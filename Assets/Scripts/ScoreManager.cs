using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;

    [SerializeField]
    private TextMeshProUGUI ScoreText;

    [SerializeField]
    private GameObject HighScoreFrame;

    [SerializeField]
    private Color AddScoreColor, SubtractScoreColor, BestScoreColor, DefaultBestScoreColor;

    [SerializeField]
    private int Score;

    private int NewHighScore;

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
        else
        {
            ScoreText.GetComponent<Animator>().enabled = true;
            ScoreText.GetComponent<Animator>().Play("Score", -1, 0f);
        }

        ScoreText.text = Mathf.Max(0, Score).ToString();

        CheckHighScore();

        return ScoreText;
    }

    private void CheckHighScore()
    {
        if (Score > HighScoreChecker.Instance.GetHighScore)
        {
            HighScoreFrame.SetActive(true);
        }
        else
        {
            HighScoreFrame.SetActive(false);
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

    public void ResetHighScoreFrameAnimation()
    {
        HighScoreFrame.SetActive(false);
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