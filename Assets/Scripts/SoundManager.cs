using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PauseMainGameTheme(AudioSource audioSource)
    {
        audioSource.Pause();
    }

    public void ResumeMainGameTheme(AudioSource audioSource)
    {
        audioSource.UnPause();
    }

    public void PlayMenuSE(AudioClip audioClip)
    {
        audioSource.clip = audioClip;

        audioSource.Play();
    }
}