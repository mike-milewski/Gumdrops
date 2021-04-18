using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI ScoreText;

    [SerializeField]
    private GameObject HighScoreFrame;

    [SerializeField]
    private Color AddScoreColor, SubtractScoreColor;

    [SerializeField]
    private int Score;

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
        var Highscorechecker = FindObjectOfType<HighScoreChecker>();

        if (Score > Highscorechecker.GetHighScore)
        {
            HighScoreFrame.SetActive(true);
        }
        else
        {
            HighScoreFrame.SetActive(false);
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
}