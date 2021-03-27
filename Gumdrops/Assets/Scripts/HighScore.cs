using UnityEngine;
using TMPro;

public class HighScore : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI HighScoreText;

    private void Start()
    {
        if(PlayerPrefs.HasKey("HighScore"))
        {
            HighScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
        }
        else
        {
            return;
        }
    }
}
