using UnityEngine;

public class OverlayManager : MonoBehaviour
{
    public static OverlayManager Instance = null;

    [SerializeField]
    private Overlay overlay;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        if(Time.timeScale < 1)
        {
            Time.timeScale = 1;
        }
    }

    public void PlayOverlayAnimator()
    {
        overlay.PlayAnimator();
    }

    public void LoadLevel()
    {
        overlay.LoadLevel();
    }

    public int ReturnLevelIndex(int LevelIndex)
    {
        overlay.GetLevelIndex = LevelIndex;

        return overlay.GetLevelIndex;
    }
}
