using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class CheckPoint : MonoBehaviour
{
    [Header("SE")]
    [SerializeField] private AudioClip checkPointSE;

    private bool isActivated = false;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // 一回だけ発動
        if (isActivated)
            return;

        // Player判定
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                player.SetRespawnPoint(this.transform);

                isActivated = true;

                // チェックポイントSE再生
                if (audioSource != null && checkPointSE != null)
                {
                    audioSource.PlayOneShot(checkPointSE);
                }

                Debug.Log("チェックポイント更新");
            }
        }
    }
}