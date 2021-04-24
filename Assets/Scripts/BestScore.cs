using UnityEngine;

public class BestScore : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;

    [SerializeField]
    private ScoreManager scoreManager;

    [SerializeField]
    private Animator BestScoreAnimator;

    public void PlayAnimator()
    {
        if (scoreManager.GetNewHighScore > HighScoreChecker.Instance.GetHighScore)
        {
            BestScoreAnimator.gameObject.SetActive(true);

            BestScoreAnimator.SetBool("SetAnimation", true);
            BestScoreAnimator.Play("NewBestScore", -1, 0f);

            gameManager.CheckHighScore();
        }
    }

    public void CheckAnimator()
    {
        if(BestScoreAnimator.gameObject.activeInHierarchy)
        {
            BestScoreAnimator.gameObject.SetActive(false);
        }
    }
}