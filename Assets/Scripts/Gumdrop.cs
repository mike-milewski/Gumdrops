using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Gumdrop : MonoBehaviour
{
    [SerializeField]
    private float MainMenuMoveSpeed, GameMoveSpeed, RotationSpeed, MoveSpeed, DefaultMoveSpeed, IncrementalMoveSpeed;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private Sprite[] GumDropColors;

    [SerializeField]
    private ColorList[] colors;

    [SerializeField]
    private int ScoreValue;

    private Color color;

    private int DefaultScore, ColorIndex;

    private bool StoppedGumDrop;

    private ObjectPooler objectPooler;

    private ScoreManager scoreManager;

    private TargetColor targetColor;

    public ObjectPooler GetObjectPooler
    {
        get
        {
            return objectPooler;
        }
        set
        {
            objectPooler = value;
        }
    }

    public TargetColor GetTargetColor
    {
        get
        {
            return targetColor;
        }
        set
        {
            targetColor = value;
        }
    }

    public ScoreManager GetScoreManager
    {
        get
        {
            return scoreManager;
        }
        set
        {
            scoreManager = value;
        }
    }

    public SpriteRenderer GetSpriteRenderer
    {
        get
        {
            return spriteRenderer;
        }
        set
        {
            spriteRenderer = value;
        }
    }

    public Animator GetAnimator
    {
        get
        {
            return animator;
        }
        set
        {
            animator = value;
        }
    }

    public int GetScoreValue
    {
        get
        {
            return ScoreValue;
        }
        set
        {
            ScoreValue = value;
        }
    }

    public int GetDefaultScore
    {
        get
        {
            return DefaultScore;
        }
        set
        {
            DefaultScore = value;
        }
    }

    public int GetColorIndex
    {
        get
        {
            return ColorIndex;
        }
        set
        {
            ColorIndex = value;
        }
    }

    public float GetMoveSpeed
    {
        get
        {
            return MoveSpeed;
        }
        set
        {
            MoveSpeed = value;
        }
    }

    public float GetMainMenuMoveSpeed
    {
        get
        {
            return MainMenuMoveSpeed;
        }
        set
        {
            MainMenuMoveSpeed = value;
        }
    }

    public float GetGameMoveSpeed
    {
        get
        {
            return GameMoveSpeed;
        }
        set
        {
            GameMoveSpeed = value;
        }
    }

    public float GetDefaultMoveSpeed
    {
        get
        {
            return DefaultMoveSpeed;
        }
        set
        {
            DefaultMoveSpeed = value;
        }
    }

    public float GetIncrementalMoveSpeed
    {
        get
        {
            return IncrementalMoveSpeed;
        }
        set
        {
            IncrementalMoveSpeed = value;
        }
    }

    public float GetRotationSpeed
    {
        get
        {
            return RotationSpeed;
        }
        set
        {
            RotationSpeed = value;
        }
    }

    public bool GetStoppedGumDrop
    {
        get
        {
            return StoppedGumDrop;
        }
        set
        {
            StoppedGumDrop = value;
        }
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        animator = GetComponent<Animator>();

        DefaultScore = ScoreValue;
    }

    private void OnEnable()
    {
        animator.SetBool("SetAnimation", false);

        StoppedGumDrop = false;

        if (targetColor != null)
        {
            if(targetColor.GetStaticColor)
            {
                spriteRenderer.sprite = targetColor.GetGumDropColors[targetColor.GetColorIndex];
            }
            else
            {
                ChooseColor();
            }
        }
        else
        {
            ChooseColor();
        }
    }

    private void Update()
    {
        if(!StoppedGumDrop)
        {
            transform.position += -Vector3.up * MoveSpeed * Time.deltaTime;

            transform.Rotate(0, 0, RotationSpeed * Time.deltaTime);
        }
    }

    public void ChooseColor()
    {
        ColorIndex = Random.Range(0, colors.Length);

        spriteRenderer.sprite = GumDropColors[ColorIndex];
    }

    public void CheckColor()
    {
        if(ColorIndex == targetColor.GetColorIndex)
        {
            AddGumDropScore();
        }
        else
        {
            SubtractGumDropScore();
        }
    }

    public void StopGumDrop()
    {
        MoveSpeed = 0;
        RotationSpeed = 0;
    }

    public void AddGumDropScore()
    {
        scoreManager.ScorePoints(ScoreValue);
    }

    public void SubtractGumDropScore()
    {
        scoreManager.ScorePoints(-ScoreValue);
    }

    public void ReturnGumDropBackToQueue()
    {
        objectPooler.ReturnGumDropToPool(gameObject);
    }
}