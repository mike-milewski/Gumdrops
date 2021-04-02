using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Gumdrop : MonoBehaviour
{
    [SerializeField]
    private float MainMenuMoveSpeed, GameMoveSpeed, RotationSpeed, MoveSpeed, DefaultMoveSpeed, IncrementalMoveSpeed;

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

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        DefaultScore = ScoreValue;
    }

    private void OnEnable()
    {
        if(targetColor != null)
        {
            if(targetColor.GetStaticColor)
            {
                spriteRenderer.color = targetColor.GetImage.color;
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
        transform.position += -Vector3.up * MoveSpeed * Time.deltaTime;

        transform.Rotate(0, 0, RotationSpeed * Time.deltaTime);
    }

    public Color ChooseColor()
    {
        ColorIndex = Random.Range(0, colors.Length);

        switch(colors[ColorIndex])
        {
            case (ColorList.Red):
                spriteRenderer.sprite = GumDropColors[ColorIndex];
                break;
            case (ColorList.Blue):
                spriteRenderer.sprite = GumDropColors[ColorIndex];
                break;
            case (ColorList.Green):
                spriteRenderer.sprite = GumDropColors[ColorIndex];
                break;
            case (ColorList.Yellow):
                spriteRenderer.sprite = GumDropColors[ColorIndex];
                break;
        }
        return color;
    }

    public void CheckColor()
    {
        if(spriteRenderer.color == targetColor.GetComponent<Image>().color)
        {
            AddGumDropScore();
        }
        else
        {
            SubtractGumDropScore();
        }
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