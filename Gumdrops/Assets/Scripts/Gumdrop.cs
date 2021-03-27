using UnityEngine;
using UnityEngine.UI;

public class Gumdrop : MonoBehaviour
{
    [SerializeField]
    private float MoveSpeed, RotationSpeed;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    private Color color;

    [SerializeField]
    private ColorList[] colors;

    [SerializeField]
    private int ScoreValue;

    private int ColorIndex;

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

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        ChooseColor();
    }

    private void Update()
    {
        transform.position += -Vector3.up * MoveSpeed * Time.deltaTime;

        transform.Rotate(0, 0, RotationSpeed * Time.deltaTime);
    }

    private Color ChooseColor()
    {
        ColorIndex = Random.Range(0, colors.Length);

        switch(colors[ColorIndex])
        {
            case (ColorList.Red):
                color = Color.red;
                break;
            case (ColorList.Blue):
                color = Color.blue;
                break;
            case (ColorList.Green):
                color = Color.green;
                break;
            case (ColorList.Yellow):
                color = Color.yellow;
                break;
        }
        spriteRenderer.color = color;

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
