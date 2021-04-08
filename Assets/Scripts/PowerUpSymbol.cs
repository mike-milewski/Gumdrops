using UnityEngine;
using UnityEngine.UI;

public class PowerUpSymbol : MonoBehaviour
{
    private PowerUps powerUp;

    [SerializeField]
    private Image PowerUpImage, PowerUpImageFill;

    [SerializeField]
    private float PowerUpTime;

    private float DefaultPowerUpTime;

    private int PowerUpIndex;

    public PowerUps GetPowerUps
    {
        get
        {
            return powerUp;
        }
        set
        {
            powerUp = value;
        }
    }

    public Image GetPowerUpImage
    {
        get
        {
            return PowerUpImage;
        }
        set
        {
            PowerUpImage = value;
        }
    }

    public float GetPowerUpTime
    {
        get
        {
            return PowerUpTime;
        }
        set
        {
            PowerUpTime = value;
        }
    }

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

    private void OnEnable()
    {
        FindPowerUpManager();

        DefaultPowerUpTime = PowerUpTime;
    }

    private void FindPowerUpManager()
    {
        var pum = FindObjectOfType<PowerUpManager>();

        pum.GetTempPowerUpIndex = PowerUpIndex;
    }

    private void Update()
    {
        PowerUpTime -= Time.deltaTime;
        PowerUpImageFill.fillAmount = PowerUpTime / DefaultPowerUpTime;
        if(PowerUpTime <= 0)
        {
            EndPowerUp();
        }
    }

    private void EndPowerUp()
    {
        powerUp.LosePower(PowerUpIndex);
        Destroy(gameObject);
    }
}