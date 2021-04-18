using UnityEngine;
using TMPro;

public class ChangeScoreColor : MonoBehaviour
{
    [SerializeField]
    private Color DefaultScoreColor;

    [SerializeField]
    private TextMeshProUGUI ScoreText;

    public void ReturnScoreColorToDefault()
    {
        ScoreText.color = DefaultScoreColor;
    }
}