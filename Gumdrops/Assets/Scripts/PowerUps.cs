using UnityEngine;
using UnityEngine.UI;

public enum Powers { DoublePoints, Slow, SameColor }

public class PowerUps : MonoBehaviour
{
    [SerializeField]
    private Sprite DoublePointsSprite, SlowSprite, SameColorSprite;

    [SerializeField]
    private Powers[] powers;

    [SerializeField]
    private float PowerTime;

    private int PowerUpIndex;

    private PowerUpManager powerUpManager;

    public PowerUpManager GetPowerUpManager
    {
        get
        {
            return powerUpManager;
        }
        set
        {
            powerUpManager = value;
        }
    }

    public float GetPowerTime
    {
        get
        {
            return PowerTime;
        }
        set
        {
            PowerTime = value;
        }
    }

    private void OnEnable()
    {
        ChoosePower();

        FindPowerUpManager();
    }

    private void ChoosePower()
    {
        PowerUpIndex = Random.Range(0, powers.Length);

        switch (powers[PowerUpIndex])
        {
            case (Powers.DoublePoints):
                GetComponent<SpriteRenderer>().sprite = DoublePointsSprite;
                break;
            case (Powers.Slow):
                GetComponent<SpriteRenderer>().sprite = SlowSprite;
                break;
            case (Powers.SameColor):
                GetComponent<SpriteRenderer>().sprite = SameColorSprite;
                break;
        }
    }

    private void FindPowerUpManager()
    {
        if (powerUpManager == null)
        {
            var PUM = FindObjectOfType<PowerUpManager>();

            powerUpManager = PUM;
        }
        else return;
    }

    public void GetPower()
    {
        switch (powers[PowerUpIndex])
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
        var gumDrop = FindObjectsOfType<Gumdrop>(false);

        foreach(Gumdrop gd in gumDrop)
        {
            gd.GetScoreValue *= 2;
        }
    }

    private void SlowPower()
    {
        var gumDrop = FindObjectsOfType<Gumdrop>(false);

        foreach(Gumdrop gd in gumDrop)
        {
            gd.GetMoveSpeed /= 2;
        }
    }

    private void SameColorPower()
    {
        var gumDrop = FindObjectsOfType<Gumdrop>(false);
    }
}