using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("体力")]
    [SerializeField] private int hpMax = 100;

    [Header("接近")]
    [SerializeField] private float speed = 2f;

    [Header("HPバー")]
    [SerializeField] private Image hpBarFill;

    // 現在HP
    private int hpCurrent;

    // Player
    private Transform playerTransform;


    private void Start()
    {
        // Playerを探す
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            playerTransform = player.transform;
        }

        // HP初期化
        hpCurrent = hpMax;
        // HPバー初期化
        UpdateHPBar();
    }


    private void Update()
    {
        // Playerが見つからなければ何もしない
        if (playerTransform == null)
        {
            return;
        }

        // Playerへの方向
        Vector3 direction = playerTransform.position - transform.position;

        // 上下の移動をさせない
        direction.y = 0f;

        // 距離が近すぎる場合は何もしない
        if (direction.sqrMagnitude <= 0.01f)
        {
            return;
        }

        // 正規化
        direction.Normalize();

        // Playerの方向を向く
        transform.rotation = Quaternion.LookRotation(direction);
        // Playerへ接近
        transform.position += direction * speed * Time.deltaTime;
    }


    #region HP

    /// <summary>
    /// ダメージを受ける
    /// </summary>
    public void TakeDamage(int damage)
    {
        // ダメージを受ける
        hpCurrent -= damage;
        // HPが0未満にならないようにする
        hpCurrent = Mathf.Max(hpCurrent, 0);
        // HPバー更新
        UpdateHPBar();
        Debug.Log("Enemy HP : " + hpCurrent);

        // HPが0になったら死亡
        if (hpCurrent <= 0)
        {
            Die();
        }
    }


    /// <summary>
    /// HPバー更新
    /// </summary>
    private void UpdateHPBar()
    {
        if (hpBarFill == null)
        {
            return;
        }
        hpBarFill.fillAmount = (float)hpCurrent / hpMax;
    }


    /// <summary>
    /// 敵死亡
    /// </summary>
    private void Die()
    {
        Debug.Log("Enemy Destroy!");
        Destroy(gameObject);
    }

    #endregion
}
