using UnityEngine;
using UnityEngine.UI;

public enum Powers { DoublePoints, Slow, SameColor }

public class PowerUps : MonoBehaviour
{
    [SerializeField]
    private Image[] PowerUpImages;

    [SerializeField]
    private Powers powers;

    private void OnEnable()
    {
        switch(powers)
        {
            case (Powers.DoublePoints):
                DoublePointsPower();
                break;
            case (Powers.Slow):
                SlowPower();
                break;
            case (Powers.SameColor):
                SameColorPower();
                break;
        }
    }

    private void DoublePointsPower()
    {

    }

    private void SlowPower()
    {

    }

    private void SameColorPower()
    {

    }
}
