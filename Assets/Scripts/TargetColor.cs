using UnityEngine;
using UnityEngine.UI;

public enum ColorList { Red, Blue, Green, Yellow }

public class TargetColor : MonoBehaviour
{
    [SerializeField]
    private ColorList[] color;

    [SerializeField]
    private Gradient gradient;

    [SerializeField]
    private Image TargetImage, NextColorImage, ColorFrame;

    [SerializeField]
    private Sprite[] GumDropColors;

    [SerializeField]
    private float DefaultTimeToChangeColor;

    private float TimeToChangeColor;

    private int ColorIndex, RandomIndex;

    private bool StaticColor, AboutToSwitchColor;

    public Image GetTargetImage
    {
        get
        {
            return TargetImage;
        }
        set
        {
            TargetImage = value;
        }
    }

    public Image GetNextColorImage
    {
        get
        {
            return NextColorImage;
        }
        set
        {
            NextColorImage = value;
        }
    }

    public Sprite[] GetGumDropColors
    {
        get
        {
            return GumDropColors;
        }
        set
        {
            GumDropColors = value;
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

    public bool GetStaticColor
    {
        get
        {
            return StaticColor;
        }
        set
        {
            StaticColor = value;
        }
    } 

    public bool GetAboutToSwitchColor
    {
        get
        {
            return AboutToSwitchColor;
        }
        set
        {
            AboutToSwitchColor = value;
        }
    }

    private void Update()
    {
        if(!StaticColor)
        {
            TimeToChangeColor -= Time.deltaTime;

            ColorFrame.fillAmount = TimeToChangeColor / DefaultTimeToChangeColor;

            ChangeColorFrame();

            if(TimeToChangeColor <= 3 && !AboutToSwitchColor)
            {
                AboutToSwitchColor = true;
                RandomColorIndex();
            }
            if (TimeToChangeColor <= 0)
            {
                PlayTargetImageAnimation();
                AboutToSwitchColor = false;
                NextColorImage.GetComponent<Animator>().SetBool("SetAnimation", false);
                TimeToChangeColor = DefaultTimeToChangeColor;
            }
        }
    }

    private void RandomColorIndex()
    {
        var gumDrops = FindObjectsOfType<Gumdrop>(false);

        int Rand = Random.Range(0, gumDrops.Length);
        
        RandomIndex = gumDrops[Rand].GetColorIndex;

        NextColorImage.sprite = GumDropColors[RandomIndex];

        NextColorImage.GetComponent<Animator>().SetBool("SetAnimation", true);
    }

    private void PlayTargetImageAnimation()
    {
        TargetImage.GetComponent<Animator>().SetBool("SetAnimation", true);
    }

    public void ChooseColor()
    {
        TargetImage.GetComponent<Animator>().SetBool("SetAnimation", false);
        TargetImage.sprite = GumDropColors[RandomIndex];
        ColorIndex = RandomIndex;
    }

    private void ChooseRandomColorFromStart()
    {
        ColorIndex = Random.Range(0, color.Length);

        TargetImage.sprite = GumDropColors[ColorIndex];
    }

    private Color ColorFromGradient(float value)  // float between 0-1
    {
        return gradient.Evaluate(value);
    }

    private void ChangeColorFrame()
    {
        ColorFrame.color = ColorFromGradient(TimeToChangeColor / DefaultTimeToChangeColor);
    }

    public void SetTimer()
    {
        TimeToChangeColor = DefaultTimeToChangeColor;
    }

    public void SetStartingTimeAndColor()
    {
        TimeToChangeColor = DefaultTimeToChangeColor;

        ChooseRandomColorFromStart();
    }
}