using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class Coin : MonoBehaviour
{
    [Header("SCORE")]
    [SerializeField] private int score = 1;

    [Header("SE")]
    [SerializeField] private AudioClip coinSE;

    private AudioSource audioSource;
    private GameManager gameManager;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // シーン内のGameManagerを取得
        gameManager = FindFirstObjectByType<GameManager>();
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        // スコア加算
        if (gameManager != null)
        {
            gameManager.AddScore(score);
        }

        // SE再生
        if (audioSource != null && coinSE != null)
        {
            audioSource.PlayOneShot(coinSE);
        }

        // 非表示
        GetComponent<Collider>().enabled = false;
        GetComponent<Renderer>().enabled = false;

        // 死
        float delay = 0f;
        if (coinSE != null)
        {
            delay = coinSE.length;
        }

        Destroy(gameObject, delay);
    }
}
