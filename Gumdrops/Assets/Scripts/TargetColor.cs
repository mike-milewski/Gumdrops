using UnityEngine;
using UnityEngine.UI;

public enum StartingColor { Red, Blue, Green, Yellow }

public class TargetColor : MonoBehaviour
{
    [SerializeField]
    private StartingColor startingColor;

    [SerializeField]
    private Color color;

    [SerializeField]
    private Image image;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    private void Awake()
    {
        ChooseStartingColor();
    }

    private void ChooseStartingColor()
    {
        switch(startingColor)
        {
            case (StartingColor.Red):
                image.color = Color.red;
                break;
            case (StartingColor.Blue):
                image.color = Color.blue;
                break;
            case (StartingColor.Green):
                image.color = Color.green;
                break;
            case (StartingColor.Yellow):
                image.color = Color.yellow;
                break;
        }
    }
}
