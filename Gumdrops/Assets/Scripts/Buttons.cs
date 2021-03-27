using UnityEngine;

public class Buttons : MonoBehaviour
{
    public void LoadGameScene()
    {
        OverlayManager.Instance.ReturnLevelIndex(1);
        OverlayManager.Instance.PlayOverlayAnimator();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
