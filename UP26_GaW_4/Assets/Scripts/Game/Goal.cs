using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class Goal : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject goalPanel;

    [Header("SE")]
    [SerializeField] private AudioClip goalSE;

    private bool isGoal;

    private AudioSource audioSource;

    private void Start()
    {
        if (goalPanel == null)
            return;

        audioSource = GetComponent<AudioSource>();

        goalPanel.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isGoal) return;

        if (other.CompareTag("Player"))
        {
            isGoal = true;

            // ÉSÅ[ÉãSEçƒê∂
            if (audioSource != null && goalSE != null)
            {
                audioSource.PlayOneShot(goalSE);
            }

            goalPanel.SetActive(true);

            // éûä‘í‚é~
            Time.timeScale = 0f;
        }
    }
}