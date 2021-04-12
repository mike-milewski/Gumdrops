using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    public void PlaySound()
    {
        gameObject.GetComponent<AudioSource>().Play();
    }
}
