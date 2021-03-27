using UnityEngine;
using UnityEngine.SceneManagement;

public class Overlay : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    private int LevelIndex;

    public int GetLevelIndex
    {
        get
        {
            return LevelIndex;
        }
        set
        {
            LevelIndex = value;
        }
    }

    public void PlayAnimator()
    {
        animator.SetBool("Fade", true);
    }

    public void ResetTrigger()
    {
        animator.SetBool("Fade", false);
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(LevelIndex);
    }
}
