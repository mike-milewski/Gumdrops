using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private ScoreManager scoreManager;

    [SerializeField]
    private TargetColor targetColor;

    [SerializeField]
    private TextMeshProUGUI TimerText, TargetScoreText;

    [SerializeField]
    private GameObject GameOverMenu;

    [SerializeField]
    private Transform PowerUpSymbolParent;

    [SerializeField]
    private Button menuButton;

    [SerializeField]
    private int DefaultTargetScore, TargetScore, ScoreIncrement, GumDropMoveSpeedIncrement;

    [SerializeField]
    private float StartTimer;

    private float Timer;

    private void OnEnable()
    {
        PlayerPrefs.DeleteKey("HighScore");

        StartGame();
    }

    private void Update()
    {
        Timer -= Time.deltaTime;
        TimerText.text = Mathf.Clamp(Timer, 0, Timer).ToString("F0");
        if(Timer <= 0)
        {
            if(scoreManager.GetScore >= TargetScore)
            {
                NextLevel();
            }
            else
            {
                GameOverMenu.SetActive(true);
                ToggleMenuButton(menuButton);
                Time.timeScale = 0;
            }
        }
    }

    private void NextLevel()
    {
        Timer = StartTimer;
        TargetScore += ScoreIncrement;

        TargetScoreText.text = TargetScore.ToString();

        IncreaseGumDropAndPowerUpSpeed();
    }

    public void PlayOverlayAnimator()
    {
        OverlayManager.Instance.ReturnLevelIndex(0);
        OverlayManager.Instance.PlayOverlayAnimator();
    }

    public void StartGame()
    {
        Timer = StartTimer;

        TargetScore = DefaultTargetScore;

        scoreManager.GetScore = 0;

        scoreManager.UpdateScoreText();

        targetColor.SetStartingTimeAndColor();

        TargetScoreText.text = TargetScore.ToString();
    }

    public void ResetGumDrops()
    {
        var gumDrops = FindObjectsOfType<Gumdrop>(false);

        foreach(Gumdrop gd in gumDrops)
        {
            ObjectPooler.Instance.ReturnGumDropToPool(gd.gameObject);
        }
    }

    public void ResetPowerUp()
    {
        var powerUpMovement = FindObjectsOfType<PowerUpMovement>(false);

        foreach (PowerUpMovement pum in powerUpMovement)
        {
            ObjectPooler.Instance.ReturnPowerUpToPool(pum.gameObject);
        }
    }

    public void DestroyPowerUpSymbols()
    {
        foreach(PowerUpSymbol pum in PowerUpSymbolParent.GetComponentsInChildren<PowerUpSymbol>())
        {
            if(pum != null)
            {
                Destroy(pum);
            }
        }
    }

    private void IncreaseGumDropAndPowerUpSpeed()
    {
        var gumDrops = FindObjectsOfType<Gumdrop>(false);
        var powerUpMovement = FindObjectsOfType<PowerUpMovement>(false);

        foreach (Gumdrop gd in gumDrops)
        {
            gd.GetMoveSpeed += GumDropMoveSpeedIncrement;
        }
        foreach (PowerUpMovement pum in powerUpMovement)
        {
            pum.GetMoveSpeed += GumDropMoveSpeedIncrement;
        }
    }

    public void RestGumDropAndPowerUpSpeed()
    {
        var gumDrops = FindObjectsOfType<Gumdrop>(false);
        var powerUpMovement = FindObjectsOfType<PowerUpMovement>(false);

        foreach (Gumdrop gd in gumDrops)
        {
            gd.GetMoveSpeed = gd.GetDefaultMoveSpeed;
        }
        foreach (PowerUpMovement pum in powerUpMovement)
        {
            pum.GetMoveSpeed = pum.GetDefaultMoveSpeed;
        }
    }

    public void CheckHighScore()
    {
        var Highscorechecker = FindObjectOfType<HighScoreChecker>();

        if(scoreManager.GetScore > Highscorechecker.GetHighScore)
        {
            Highscorechecker.GetHighScore = scoreManager.GetScore;

            PlayerPrefs.SetInt("HighScore", Highscorechecker.GetHighScore);
            PlayerPrefs.Save();
        }
    }

    public void ToggleMenuButton(Button MenuButton)
    {
        MenuButton.interactable = false;
    }

    public void OpenMenu()
    {
        Time.timeScale = 0;
    }

    public void CloseMenu()
    {
        Time.timeScale = 1;
    }
}