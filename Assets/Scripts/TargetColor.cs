using UnityEngine;
using UnityEngine.UI;

public enum ColorList { Red, Blue, Green, Yellow }

public class TargetColor : MonoBehaviour
{
    [SerializeField]
    private ColorList[] color;

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

    private void Update()
    {
        if(!StaticColor)
        {
            TimeToChangeColor -= Time.deltaTime;

            ColorFrame.fillAmount = TimeToChangeColor / DefaultTimeToChangeColor;

            if(TimeToChangeColor <= 3 && !AboutToSwitchColor)
            {
                AboutToSwitchColor = true;
                RandomColorIndex();
            }
            if (TimeToChangeColor <= 0)
            {
                ChooseColor();
                AboutToSwitchColor = false;
                NextColorImage.sprite = null;
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
    }

    private void ChooseColor()
    {
        TargetImage.sprite = GumDropColors[RandomIndex];
        ColorIndex = RandomIndex;
    }

    private void ChooseRandomColorFromStart()
    {
        ColorIndex = Random.Range(0, color.Length);

        TargetImage.sprite = GumDropColors[ColorIndex];
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