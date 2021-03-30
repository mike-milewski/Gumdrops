using UnityEngine;
using UnityEngine.UI;

public class PowerUpSymbol : MonoBehaviour
{
    private PowerUps powerUp;

    private float PowerUpTime;

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

    private void Update()
    {
        PowerUpTime -= Time.deltaTime;
        if(PowerUpTime <= 0)
        {
            EndPowerUp();
        }
    }

    private void EndPowerUp()
    {
        powerUp.LosePower(powerUp.GetPowerUpIndex);
        Destroy(gameObject);
    }
}
