using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    /// <summary>
    /// ボタンから呼び出す関数
    /// </summary>
    public void OnStartButton()
    {
        SceneManager.LoadScene("Game1");
    }

    /// <summary>
    /// ゲーム終了
    /// </summary>
    public void OnQuitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }
}