using UnityEngine;
using UnityEngine.UI;

public enum ColorList { Red, Blue, Green, Yellow }

public class TargetColor : MonoBehaviour
{
    [SerializeField]
    private ColorList[] color;

    [SerializeField]
    private Image image, NextColorImage;

    [SerializeField]
    private float DefaultTimeToChangeColor;

    private float TimeToChangeColor;

    private int ColorIndex;

    private bool StaticColor, AboutToSwitchColor;

    public Image GetImage
    {
        get
        {
            return image;
        }
        set
        {
            image = value;
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

    private void Start()
    {
        image = GetComponent<Image>();
    }

    private void Update()
    {
        if(!StaticColor)
        {
            TimeToChangeColor -= Time.deltaTime;
            if(TimeToChangeColor <= 3 && !AboutToSwitchColor)
            {
                AboutToSwitchColor = true;
                RandomColorIndex();
            }
            if (TimeToChangeColor <= 0)
            {
                ChooseColor();
                AboutToSwitchColor = false;
                NextColorImage.color = Color.white;
                TimeToChangeColor = DefaultTimeToChangeColor;
            }
        }
    }

    private void RandomColorIndex()
    {
        ColorIndex = Random.Range(0, color.Length);

        switch (color[ColorIndex])
        {
            case (ColorList.Red):
                NextColorImage.color = Color.red;
                break;
            case (ColorList.Blue):
                NextColorImage.color = Color.blue;
                break;
            case (ColorList.Green):
                NextColorImage.color = Color.green;
                break;
            case (ColorList.Yellow):
                NextColorImage.color = Color.yellow;
                break;
        }
    }

    private void ChooseColor()
    {
        switch(color[ColorIndex])
        {
            case (ColorList.Red):
                image.color = Color.red;
                break;
            case (ColorList.Blue):
                image.color = Color.blue;
                break;
            case (ColorList.Green):
                image.color = Color.green;
                break;
            case (ColorList.Yellow):
                image.color = Color.yellow;
                break;
        }
    }

    private void ChooseRandomColorFromStart()
    {
        ColorIndex = Random.Range(0, color.Length);

        switch (color[ColorIndex])
        {
            case (ColorList.Red):
                image.color = Color.red;
                break;
            case (ColorList.Blue):
                image.color = Color.blue;
                break;
            case (ColorList.Green):
                image.color = Color.green;
                break;
            case (ColorList.Yellow):
                image.color = Color.yellow;
                break;
        }
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