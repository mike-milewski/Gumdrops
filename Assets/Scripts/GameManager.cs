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
    private AudioSource BGM;

    [SerializeField]
    private TextMeshProUGUI TimerText, TargetScoreText, LevelText, ScoreModifierText, BestScoreNumberText, BestScoreText;

    [SerializeField]
    private Animator ScoreModifierAnimator, IceFrameAnimator, MinusTimerAnimator;

    [SerializeField]
    private GameObject GameOverMenu, FrozenOverlay;

    [SerializeField]
    private Transform PowerUpSymbolParent;

    [SerializeField]
    private Button menuButton;

    private Color TimerTextColor;

    [SerializeField]
    private int GumDropMoveSpeedIncrement, MaxSpeed, ScoreModifier;

    [SerializeField]
    private int[] ScoreIncrement;

    [SerializeField]
    private float StartTimer, MinimumTimer;

    private int Level, LevelIndex, TargetScore, ScoreIncrementIndex;

    private float Timer, CurrentTimer;

    private bool StartTimerCount, TimeScaleIsZero, MenuOpened;

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

    public int GetTargetScore
    {
        get
        {
            return TargetScore;
        }
        set
        {
            TargetScore = value;
        }
    }

    public bool GetTimeScaleIsZero
    {
        get
        {
            return TimeScaleIsZero;
        }
        set
        {
            TimeScaleIsZero = value;
        }
    }

    public bool GetStartTimerCount
    {
        get
        {
            return StartTimerCount;
        }
        set
        {
            StartTimerCount = value;
        }
    }

    public bool GetMenuOpened
    {
        get
        {
            return MenuOpened;
        }
        set
        {
            MenuOpened = value;
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

    public Animator GetIceFrameAnimator
    {
        get
        {
            return IceFrameAnimator;
        }
        set
        {
            IceFrameAnimator = value;
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

    public TextMeshProUGUI GetBestScoreNumberText
    {
        get
        {
            return BestScoreNumberText;
        }
        set
        {
            BestScoreNumberText = value;
        }
    }

    public TextMeshProUGUI GetTimerText
    {
        get
        {
            return TimerText;
        }
        set
        {
            TimerText = value;
        }
    }

    private void OnEnable()
    {
        TimerTextColor = TimerText.color;

        StartGame();

        StartCoroutine("WaitToStartTimer");

        UpdateScoreModifier();

        CheckHighScoreText();
    }

    private void Update()
    {
        if(StartTimerCount)
        {
            Timer -= Time.deltaTime;
        }
        TimerText.text = Mathf.Clamp(Timer, 0, Timer).ToString("F0");
        CheckTimer();
        if (Timer <= 0)
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
                    GameOverMenuOpened = true;
                }

                Time.timeScale = 0;
            }
        }
    }

    public void CheckHighScoreText()
    {
        if(PlayerPrefs.HasKey("HighScore"))
        {
            BestScoreText.gameObject.SetActive(true);
            BestScoreNumberText.text = HighScoreChecker.Instance.GetHighScore.ToString();
        }
        else
        {
            BestScoreText.gameObject.SetActive(false);
            BestScoreNumberText.text = "";
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

    private void CheckScoreIncrement()
    {
        ScoreIncrementIndex++;
        if(ScoreIncrementIndex < ScoreIncrement.Length)
        {
            TargetScore = ScoreIncrement[ScoreIncrementIndex];
        }
        else
        {
            TargetScore += 1000;
        }
    }

    private void NextLevel()
    {
        if(CurrentTimer > MinimumTimer)
        {
            MinusTimerAnimator.enabled = true;
            MinusTimerAnimator.Play("MinusTimer", -1, 0f);
            CurrentTimer--;
        }
        Timer = CurrentTimer;

        CheckScoreIncrement();

        TargetScoreText.text = TargetScore.ToString();

        UpdateLevel();

        TargetScoreText.GetComponent<Animator>().enabled = true;
        TargetScoreText.GetComponent<Animator>().Play("HighScore", -1, 0f);

        TimerText.GetComponent<Animator>().SetBool("Timer", false);

        TimerText.color = TimerTextColor;

        targetColor.ReduceTimer();

        if(ObjectPooler.Instance.GetCurrentSpawnTimer > ObjectPooler.Instance.GetMinimumSpawnTimer)
        {
            LevelIndex++;
            ObjectPooler.Instance.GetCurrentSpawnTimer = ObjectPooler.Instance.GetSpawnTimerPerLevel[LevelIndex];
        }

        scoreManager.CheckTargetScore();

        scoreManager.GetIncorrectGumDropInputs = 0;

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
        CurrentTimer = StartTimer;

        TimerText.GetComponent<Animator>().SetBool("Timer", false);
        FrozenOverlay.GetComponent<Animator>().SetBool("SetAnimation", false);
        IceFrameAnimator.SetBool("SetAnimation", false);

        TimerText.color = TimerTextColor;

        TargetScore = ScoreIncrement[0];

        scoreManager.GetScore = 0;

        scoreManager.UpdateScoreText();

        TargetScoreText.text = TargetScore.ToString();

        SetLevelText();

        GameOverMenuOpened = false;
        menuButton.interactable = true;
    }

    public void ResetMusic()
    {
        BGM.Stop();
        BGM.Play();
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
        if(scoreManager.GetNewHighScore > HighScoreChecker.Instance.GetHighScore)
        {
            HighScoreChecker.Instance.GetHighScore = scoreManager.GetNewHighScore;

            PlayerPrefs.SetInt("HighScore", HighScoreChecker.Instance.GetHighScore);
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

    public void SetMenuOpenedToTrue()
    {
        MenuOpened = true;
    }
    
    public void SetMenuOpenedToFalse()
    {
        MenuOpened = false;
    }

    public void OpenMenu(Animator animator)
    {
        animator.SetBool("OpenMenu", true);
        TimeScaleIsZero = true;
        Time.timeScale = 0;
    }

    public void CloseMenu(Animator animator)
    {
        Time.timeScale = 1;
        TimeScaleIsZero = false;
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
        StartTimerCount = true;
    }
}