using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI ScoreText;

    [SerializeField]
    private int Score;

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

        return ScoreText;
    }

    public void UpdateScoreText()
    {
        ScoreText.text = Score.ToString();
    }
}