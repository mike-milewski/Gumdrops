using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip clip;

    [SerializeField]
    private Slider BackgroundMusicSlider, SoundEffectSlider;

    [SerializeField]
    private AudioSource[] audioSources;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        BackgroundMusicSlider.value = PlayerPrefs.GetFloat("BackgroundMusicVolume");
        SoundEffectSlider.value = PlayerPrefs.GetFloat("SoundEffectsVolume");
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

    public void PlayNextLevelClip()
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void BgmSlider(Slider slider)
    {
        audioSource.volume = slider.value;
    }

    public void SEslider(Slider slider)
    {
        for(int i = 0; i < audioSources.Length; i++)
        {
            audioSources[i].volume = slider.value;
        }
    }
}