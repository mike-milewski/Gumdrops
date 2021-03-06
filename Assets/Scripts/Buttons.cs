using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    public void LoadGameScene()
    {
        OverlayManager.Instance.ReturnLevelIndex(1);
        OverlayManager.Instance.PlayOverlayAnimator();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void PlayMenuAnimator(Animator animator)
    {
        animator.SetBool("OpenMenu", true);
    }

    public void PlayCloseMenuAnimator(Animator animator)
    {
        animator.SetBool("OpenMenu", false);
    }

    public void OpenLink(string Link)
    {
        Application.OpenURL(Link);
    }

    public void SaveBackgroundVolume(Slider slider)
    {
        PlayerPrefs.SetFloat("BackgroundVolume", slider.value);
        PlayerPrefs.Save();
    }

    public void SaveSoundEffectVolume(Slider slider)
    {
        PlayerPrefs.SetFloat("SoundEffectVolume", slider.value);
        PlayerPrefs.Save();
    }
}