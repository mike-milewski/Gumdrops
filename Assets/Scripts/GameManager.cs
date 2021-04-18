using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private ScoreManager scoreManager;

    [SerializeField]
    private TargetColor targetColor;

    [SerializeField]
    private SoundManager soundManager;

    [SerializeField]
    private TextMeshProUGUI TimerText, TargetScoreText, LevelText, ScoreModifierText;

    [SerializeField]
    private Animator ScoreModifierAnimator;

    [SerializeField]
    private GameObject GameOverMenu, FrozenOverlay;

    [SerializeField]
    private Transform PowerUpSymbolParent;

    [SerializeField]
    private Button menuButton;

    private Color TimerTextColor;

    [SerializeField]
    private int DefaultTargetScore, ScoreIncrement, GumDropMoveSpeedIncrement, MaxSpeed, ScoreModifier;

    [SerializeField]
    private float StartTimer;

    private int Level, LevelIndex;

    private float TargetScore;

    private float Timer;

    private bool StartedGame;

    [SerializeField]
    private bool GameOverMenuOpened;

    public int GetScoreModifier
    {
        get
        {
            return ScoreModifier;
        }
        set
        {
            ScoreModifier = value;
        }
    }

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

    public Animator GetScoreModifierAnimator
    {
        get
        {
            return ScoreModifierAnimator;
        }
        set
        {
            ScoreModifierAnimator = value;
        }
    }

    public GameObject GetFrozenOverlay
    {
        get
        {
            return FrozenOverlay;
        }
        set
        {
            FrozenOverlay = value;
        }
    }

    public TextMeshProUGUI GetScoreModifierText
    {
        get
        {
            return ScoreModifierText;
        }
        set
        {
            ScoreModifierText = value;
        }
    }

    private void OnEnable()
    {
        PlayerPrefs.DeleteKey("HighScore");

        TimerTextColor = TimerText.color;

        StartGame();

        StartCoroutine("WaitToStartTimer");

        UpdateScoreModifier();
    }

    private void Update()
    {
        if(StartedGame)
        {
            Timer -= Time.deltaTime;
        }
        TimerText.text = Mathf.Clamp(Timer, 0, Timer).ToString("F0");
        CheckTimer();
        if(Timer <= 0)
        {
            if(scoreManager.GetScore >= TargetScore)
            {
                NextLevel();
            }
            else
            {
                GameOverMenu.GetComponent<Animator>().SetBool("OpenMenu", true);

                if (!GameOverMenuOpened)
                {
                    ToggleMenuButton(menuButton);
                    GetComponent<AudioSource>().Play();
                    CheckHighScore();
                    GameOverMenuOpened = true;
                }

                Time.timeScale = 0;
            }
        }
    }

    private void CheckTimer()
    {
        if(Timer < 10)
        {
            TimerText.color = Color.red;
            TimerText.GetComponent<Animator>().SetBool("Timer", true);
        }
    }

    public void ResetGameOverMenuOpened()
    {
        GameOverMenuOpened = false;
    }

    private void NextLevel()
    {
        Timer = StartTimer;
        TargetScore += ScoreIncrement;

        TargetScoreText.text = TargetScore.ToString();

        UpdateLevel();

        TargetScoreText.GetComponent<Animator>().enabled = true;
        TargetScoreText.GetComponent<Animator>().Play("HighScore", -1, 0f);

        TimerText.GetComponent<Animator>().SetBool("Timer", false);

        TimerText.color = TimerTextColor;

        targetColor.ReduceTimer();

        if(ObjectPooler.Instance.GetCurrentSpawnTimer > ObjectPooler.Instance.GetMinimumSpawnTimer)
        {
            ObjectPooler.Instance.GetCurrentSpawnTimer = ObjectPooler.Instance.GetSpawnTimerPerLevel[LevelIndex];
            LevelIndex++;
        }

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

        TimerText.GetComponent<Animator>().SetBool("Timer", false);

        TimerText.color = TimerTextColor;

        TargetScore = DefaultTargetScore;

        scoreManager.GetScore = 0;

        scoreManager.UpdateScoreText();

        TargetScoreText.text = TargetScore.ToString();

        SetLevelText();

        GameOverMenuOpened = false;
        menuButton.interactable = true;
    }

    public void ResetGumDrops()
    {
        var gumDrops = FindObjectsOfType<Gumdrop>(true);

        foreach(Gumdrop gd in gumDrops)
        {
            ObjectPooler.Instance.ReturnGumDropToPool(gd.gameObject);
        }
    }

    public void ResetPowerUp()
    {
        var powerUpMovement = FindObjectsOfType<PowerUpMovement>(true);

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
                pum.GetPowerUps.LosePower(pum.GetPowerUpIndex);
                Destroy(pum.gameObject);
            }
        }
    }

    private void IncreaseGumDropAndPowerUpSpeed()
    {
        var gumDrops = FindObjectsOfType<Gumdrop>(true);

        foreach (Gumdrop gd in gumDrops)
        {
            if(gd.GetMoveSpeed < MaxSpeed)
            {
                gd.GetMoveSpeed += GumDropMoveSpeedIncrement;
                gd.GetIncrementalMoveSpeed += GumDropMoveSpeedIncrement;

                gd.GetMoveSpeed = Mathf.Clamp(gd.GetMoveSpeed, 0, MaxSpeed);
                gd.GetIncrementalMoveSpeed = Mathf.Clamp(gd.GetIncrementalMoveSpeed, 0, MaxSpeed);
            }
        }
    }

    public void ResetGumDropAndPowerUpSpeed()
    {
        var gumDrops = FindObjectsOfType<Gumdrop>(true);

        foreach (Gumdrop gd in gumDrops)
        {
            gd.GetMoveSpeed = gd.GetDefaultMoveSpeed;
            gd.GetIncrementalMoveSpeed = gd.GetMoveSpeed;
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

    private void SetLevelText()
    {
        Level = 1;

        LevelText.text = "<u>Level</u> \n" + Level;
    }

    private void UpdateLevel()
    {
        Level++;

        LevelText.text = "<u>Level</u> \n" + Level;

        soundManager.PlayNextLevelClip();
    }

    public void UpdateScoreModifier()
    {
        ScoreModifierText.text = ScoreModifier.ToString();
    }

    public void ToggleMenuButton(Button MenuButton)
    {
        MenuButton.interactable = false;
    }

    public void OpenMenu(Animator animator)
    {
        animator.SetBool("OpenMenu", true);
        Time.timeScale = 0;
    }

    public void CloseMenu(Animator animator)
    {
        Time.timeScale = 1;
        animator.SetBool("OpenMenu", false);
    }

    public void ResetNextTargetImageAnimation()
    {
        targetColor.GetAboutToSwitchColor = false;
        targetColor.GetNextColorImage.GetComponent<Animator>().SetBool("SetAnimation", false);
    }

    public void ResetObjectSpawnTimer()
    {
        ObjectPooler.Instance.ResetCurrentTimer();
    }

    public void ResetScoreModifier()
    {
        ScoreModifier = 1;
        UpdateScoreModifier();
    }

    public void ResetLevelIndex()
    {
        LevelIndex = 0;
    }

    private IEnumerator WaitToStartTimer()
    {
        yield return new WaitForSeconds(1);
        StartedGame = true;
    }
}