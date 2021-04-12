using UnityEngine;

public class TitleAudio : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;

    public void PlayAudio()
    {
        audioSource.Play();
    }
}