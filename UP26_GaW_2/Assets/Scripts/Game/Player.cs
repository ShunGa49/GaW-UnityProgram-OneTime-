using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("体力")]
    [SerializeField] private int hpMax = 100;

    [Header("HP UI")]
    [SerializeField] private TMP_Text hpText;

    private int hpCurrent;


    private void Start()
    {
        // HP初期化
        hpCurrent = hpMax;
        // HP表示
        UpdateHPUI();
    }


    /// <summary>
    /// ダメージを受ける
    /// </summary>
    public void TakeDamage(int damage)
    {
        // ダメージ
        hpCurrent -= damage;
        // HPが0未満にならないようにする
        hpCurrent = Mathf.Max(hpCurrent, 0);
        // HP表示更新
        UpdateHPUI();

        Debug.Log("Player HP : " + hpCurrent);

        // HPが0になったら死亡
        if (hpCurrent <= 0)
        {
            Die();
        }
    }


    /// <summary>
    /// HP UI更新
    /// </summary>
    private void UpdateHPUI()
    {
        if (hpText == null)
        {
            return;
        }

        hpText.text = "HP : " + hpCurrent + " / " + hpMax;
    }


    /// <summary>
    /// プレイヤー死亡
    /// </summary>
    private void Die()
    {
        Debug.Log("Player Dead!");
    }
}
