using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject gameOverPanel;
    
    private bool isGameOver =false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // はじめは非表示
        gameOverPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
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


    #endregion
}
