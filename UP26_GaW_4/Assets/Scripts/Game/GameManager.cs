using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("TIME")]
    [SerializeField] private float timeLimit = 60f;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject gameOverPanel;

    [Header("Next Scene")]
    [SerializeField] private TextMeshProUGUI nextButtonText;
    [SerializeField] private string nextSceneName;


    private int score = 0;


    private bool isGameOver =false;

    // 外部公開
    public bool IsGameOver => isGameOver;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nextButtonText.text = nextSceneName;

        // はじめは非表示
        gameOverPanel.SetActive(false);
    }

    void Update()
    {
        CountDownTimeLimit();
    }

    /// <summary>
    /// 制限時間
    /// </summary>
    void CountDownTimeLimit()
    {
        // ゲームオーバー後は停止
        if (isGameOver)
            return;

        // 時間減少
        timeLimit -= Time.deltaTime;

        // 0未満防止
        if (timeLimit <= 0)
        {
            timeLimit = 0;
            // 時間切れ！
            GameOver();
        }

        // UI更新
        timeText.text = "TIME : " + timeLimit.ToString("F1");

        scoreText.text = "SCORE : " + score.ToString();
    }


    // スコア加算
    public void AddScore(int value)
    {
        score += value;
    }



    /// <summary>
    /// ゲームオーバー
    /// </summary>
    public void GameOver()
    {
        // すでにゲームオーバーなら何もしない
        if (isGameOver)
            return;

        isGameOver = true;

        // ゲームオーバー表示
        gameOverPanel.SetActive(true);

        // ゲーム停止
        Time.timeScale = 0f;
    }

    #region ボタンから呼び出す関数
    // リトライ
    public void OnRetryButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    // タイトル
    public void OnTitleButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Title");
    }

    // 次のステージ
    public void OnNextButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(nextSceneName);
    }


    #endregion
}
