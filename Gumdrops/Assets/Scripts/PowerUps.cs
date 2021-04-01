using UnityEngine;
using UnityEngine.UI;

public enum Powers { DoublePoints, Slow, SameColor }

public class PowerUps : MonoBehaviour
{
    private PowerUpManager powerUpManager;

    private ObjectPooler objectPooler;

    [SerializeField]
    private Sprite DoublePointsSprite, SlowSprite, SameColorSprite;

    [SerializeField]
    private Powers[] powers;

    [SerializeField]
    private float PowerTime;

    private int PowerUpIndex;

    public int GetPowerUpIndex
    {
        get
        {
            return PowerUpIndex;
        }
        set
        {
            PowerUpIndex = value;
        }
    }

    public Powers[] GetPowers
    {
        get
        {
            return powers;
        }
        set
        {
            powers = value;
        }
    }

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
    }

    private void Awake()
    {
        FindPowerUpManager();

        FindObjectPooler();
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

    private void FindObjectPooler()
    {
        var OP = FindObjectOfType<ObjectPooler>();

        objectPooler = OP;
    }

    public void GetPower()
    {
        if (!powerUpManager.ResetPowerUpSymbolTime())
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
        powerUpManager.CreatePowerUpSymbol();
    }

    public void LosePower(int index)
    {
        switch (powers[index])
        {
            case (Powers.DoublePoints):
                LoseDoublePointsPower();
                break;
            case (Powers.Slow):
                LoseSlowPower();
                break;
            case (Powers.SameColor):
                LoseSameColorPower();
                break;
        }
    }

    private void DoublePointsPower()
    {
        var gumDrop = FindObjectsOfType<Gumdrop>(true);

        foreach(Gumdrop gd in gumDrop)
        {
            gd.GetScoreValue *= 2;
        }
    }

    private void SlowPower()
    {
        var gumDrop = FindObjectsOfType<Gumdrop>(true);

        foreach(Gumdrop gd in gumDrop)
        {
            gd.GetMoveSpeed /= 2;
            gd.GetRotationSpeed /= 2;
        }
        objectPooler.GetDefaultSpawnTimer = 2;
    }

    private void SameColorPower()
    {
        var gumDrop = FindObjectsOfType<Gumdrop>(true);
        var targetColor = FindObjectOfType<TargetColor>();

        targetColor.GetStaticColor = true;

        foreach (Gumdrop gd in gumDrop)
        {
            gd.GetSpriteRenderer.color = targetColor.GetImage.color;
        }
    }

    private void LoseDoublePointsPower()
    {
        var gumDrop = FindObjectsOfType<Gumdrop>(true);

        foreach (Gumdrop gd in gumDrop)
        {
            gd.GetScoreValue = gd.GetDefaultScore;
        }
    }

    private void LoseSlowPower()
    {
        var gumDrop = FindObjectsOfType<Gumdrop>(true);

        foreach (Gumdrop gd in gumDrop)
        {
            gd.GetMoveSpeed = gd.GetIncrementalMoveSpeed;
            gd.GetRotationSpeed *= 2;
        }
        objectPooler.GetDefaultSpawnTimer = 1;
    }

    private void LoseSameColorPower()
    {
        var gumDrop = FindObjectsOfType<Gumdrop>(true);
        var targetColor = FindObjectOfType<TargetColor>();

        targetColor.GetStaticColor = false;
        targetColor.SetTimer();

        foreach (Gumdrop gd in gumDrop)
        {
            gd.ChooseColor();
        }
    }
}