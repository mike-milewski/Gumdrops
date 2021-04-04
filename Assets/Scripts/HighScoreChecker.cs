using UnityEngine;

public class HighScoreChecker : MonoBehaviour
{
    public static HighScoreChecker Instance = null;

    [SerializeField]
    private int Highscore;

    public int GetHighScore
    {
        get
        {
            return Highscore;
        }
        set
        {
            Highscore = value;
        }
    }

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
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("HighScore"))
        {
            Highscore = PlayerPrefs.GetInt("HighScore");
        }
    }
}
