using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    /// <summary>
    /// ƒ{ƒ^ƒ“‚©‚çŒÄ‚Ño‚·ŠÖ”
    /// </summary>
    public void OnStartButton()
    {
        SceneManager.LoadScene("Game");
    }
}