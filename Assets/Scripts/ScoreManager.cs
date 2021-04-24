using UnityEngine;
using TMPro;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;

    [SerializeField]
    private TextMeshProUGUI ScoreText, BonusScoreText;

    [SerializeField]
    private Animator BonusScoreFrameAnimator;

    [SerializeField]
    private GameObject ScoreFrame;

    [SerializeField]
    private Color AddScoreColor, SubtractScoreColor, BestScoreColor, DefaultBestScoreColor;

    [SerializeField]
    private int Score, ScoreBonus, IncorrectGumDropInputs, CorrectGumDropInputs, MaxGumDropInputs;

    private int NewHighScore;

    private bool GoalReached;

    public TextMeshProUGUI GetScoreText
    {
        get
        {
            return ScoreText;
        }
        set
        {
            ScoreText = value;
        }
    }

    public int GetScore
    {
        get
        {
            return Score;
        }
        set
        {
            Score = value;
        }
    }

    public int GetNewHighScore
    {
        get
        {
            return NewHighScore;
        }
        set
        {
            NewHighScore = value;
        }
    }

    public int GetCorrectGumDropInputs
    {
        get
        {
            return CorrectGumDropInputs;
        }
        set
        {
            CorrectGumDropInputs = value;
        }
    }

    public int GetIncorrectGumDropInputs
    {
        get
        {
            return IncorrectGumDropInputs;
        }
        set
        {
            IncorrectGumDropInputs = value;
        }
    }

    public Color GetAddScoreColor
    {
        get
        {
            return AddScoreColor;
        }
        set
        {
            AddScoreColor = value;
        }
    }

    public Color GetSubtractScoreColor
    {
        get
        {
            return SubtractScoreColor;
        }
        set
        {
            SubtractScoreColor = value;
        }
    }

    private void OnEnable()
    {
        UpdateScoreText();
    }

    public TextMeshProUGUI ScorePoints(int value)
    {
        Score += value;
        if(Score < 0)
        {
            Score = 0;
        }

        ScoreText.GetComponent<Animator>().enabled = true;
        ScoreText.GetComponent<Animator>().Play("Score", -1, 0f);

        ScoreText.text = Mathf.Max(0, Score).ToString();

        CheckGumDropHits();

        CheckTargetScore();

        return ScoreText;
    }

    public void CheckTargetScore()
    {
        if (Score >= gameManager.GetTargetScore)
        {
            if (!GoalReached)
            {
                ScoreFrame.SetActive(true);
                ScoreFrame.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("SoundEffectVolume");
                ScoreFrame.GetComponent<AudioSource>().Play();

                GoalReached = true;
            }
        }
        else
        {
            ScoreFrame.SetActive(false);

            GoalReached = false;
        }
    }

    public void CheckBestScoreText()
    {
        if(PlayerPrefs.HasKey("HighScore"))
        {
            if (Score > HighScoreChecker.Instance.GetHighScore)
            {
                NewHighScore = Score;
                gameManager.GetBestScoreNumberText.text = NewHighScore.ToString();
                gameManager.GetBestScoreNumberText.color = BestScoreColor;
            }
        }
        else
        {
            if (Score > HighScoreChecker.Instance.GetHighScore)
            {
                NewHighScore = Score;
            }
        }
    }

    private void CheckGumDropHits()
    {
        if(CorrectGumDropInputs >= MaxGumDropInputs)
        {
            int BonusScore = ScoreBonus * gameManager.GetScoreModifier;

            Score += BonusScore;

            BonusScoreFrameAnimator.SetBool("BonusScore", true);

            BonusScoreText.text = "BONUS \n" + BonusScore;

            BonusScoreFrameAnimator.gameObject.GetComponent<AudioSource>().Play();

            StartCoroutine("ResetBonusAnimator");

            UpdateScoreText();

            CorrectGumDropInputs = 0;
        }
    }

    private IEnumerator ResetBonusAnimator()
    {
        yield return new WaitForSeconds(1.5f);
        BonusScoreFrameAnimator.SetBool("BonusScore", false);
    }

    public void ResetGumDropHits()
    {
        CorrectGumDropInputs = 0;
        IncorrectGumDropInputs = 0;
    }

    public void ResetHighScoreFrameAnimation()
    {
        ScoreFrame.SetActive(false);
    }

    public void UpdateScoreText()
    {
        ScoreText.text = Score.ToString();
    }

    public void ResetBestColorTextColor()
    {
        gameManager.GetBestScoreNumberText.color = DefaultBestScoreColor;
    }
}