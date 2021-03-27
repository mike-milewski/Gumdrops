using UnityEngine;
using UnityEngine.UI;

public enum ColorList { Red, Blue, Green, Yellow }

public class TargetColor : MonoBehaviour
{
    [SerializeField]
    private ColorList[] color;

    [SerializeField]
    private Image image;

    [SerializeField]
    private float DefaultTimeToChangeColor;

    private float TimeToChangeColor;

    private int ColorIndex;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    private void Update()
    {
        TimeToChangeColor -= Time.deltaTime;
        if(TimeToChangeColor <= 0)
        {
            ChooseColor();
            TimeToChangeColor = DefaultTimeToChangeColor;
        }
    }

    private void ChooseColor()
    {
        ColorIndex = Random.Range(0, color.Length);

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

    public void SetStartingColorTimeAndColor()
    {
        TimeToChangeColor = DefaultTimeToChangeColor;

        ChooseColor();
    }
}