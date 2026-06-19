using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour
{
    [Header("移動距離")]
    [SerializeField] private Vector3 moveDistance = new Vector3(5f, 0f, 0f);

    [Header("移動速度")]
    [SerializeField] private float moveSpeed = 2f;

    [Header("待機時間")]
    [SerializeField] private float waitTime = 1f;


    private Vector3 startPos;    // 足場の初期位置
    private Vector3 targetPos;    // 移動先の位置
    private bool movingToTarget = true;    // true(行き)/false(帰り)

    private void Start()
    {
        // 初期位置を保存
        startPos = this.transform.position;

        // 移動先を計算
        targetPos = startPos + moveDistance;

        // 移動処理開始
        StartCoroutine(MoveRoutine());
    }

    private IEnumerator MoveRoutine()
    {
        // 無限ループ
        while (true)
        {
            // 現在向かう目的地
            Vector3 destination;

            // 行きなら移動先へ
            if (movingToTarget)
            {
                destination = targetPos;
            }
            // 帰りなら開始位置へ
            else
            {
                destination = startPos;
            }

            // 目的地に到着するまで移動
            while (Vector3.Distance(this.transform.position, destination) > 0.01f)
            {
                // 現在位置から目的地へ一定速度で移動
                this.transform.position = Vector3.MoveTowards(this.transform.position, destination, moveSpeed * Time.deltaTime);

                yield return null; // 次のフレームまで待機
            }

            // 誤差防止のため位置を完全に合わせる
            this.transform.position = destination;

            // 到着したら指定秒数待機
            yield return new WaitForSeconds(waitTime);

            // 行き⇔帰りを切り替え
            movingToTarget = !movingToTarget;
        }
    }
}