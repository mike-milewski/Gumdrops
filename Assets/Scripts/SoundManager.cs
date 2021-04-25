using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource, BackgroundMusicSource;

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
        if(!PlayerPrefs.HasKey("BackgroundVolume"))
        {
            PlayerPrefs.SetFloat("BackgroundVolume", 1);
            PlayerPrefs.Save();

            BackgroundMusicSlider.value = PlayerPrefs.GetFloat("BackgroundVolume");
        }
        else
        {
            BackgroundMusicSlider.value = PlayerPrefs.GetFloat("BackgroundVolume");
        }

        if (!PlayerPrefs.HasKey("SoundEffectVolume"))
        {
            PlayerPrefs.SetFloat("SoundEffectVolume", 1);
            PlayerPrefs.Save();

            SoundEffectSlider.value = PlayerPrefs.GetFloat("SoundEffectVolume");
        }
        else
        {
            SoundEffectSlider.value = PlayerPrefs.GetFloat("SoundEffectVolume");
        }
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
        BackgroundMusicSource.volume = slider.value;
    }

    public void SEslider(Slider slider)
    {
        for(int i = 0; i < audioSources.Length; i++)
        {
            audioSources[i].volume = slider.value;
        }
    }
}