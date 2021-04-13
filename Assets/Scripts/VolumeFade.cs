using UnityEngine;

public class VolumeFade : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private float FadeSpeed;

    private void Update()
    {
        audioSource.volume -= FadeSpeed * Time.unscaledDeltaTime;
    }
}