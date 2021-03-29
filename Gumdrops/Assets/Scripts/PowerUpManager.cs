using UnityEngine;
using UnityEngine.UI;

public class PowerUpManager : MonoBehaviour
{
    private PowerUps powerUp;

    [SerializeField]
    private float PowerUpSpawnTime, PowerUpSpawnChance;

    private float PowerUpTime;

    private void Start()
    {
        FindPowerUp();
    }

    public bool PowerUpSpawn()
    {
        bool Spawned = false;

        if(Random.value * 100 <= PowerUpSpawnChance && !powerUp.gameObject.activeInHierarchy)
        {
            SpawnPowerUp();
            Spawned = true;
        }
        else
        {
            ObjectPooler.Instance.SpawnGumDrop();
            Spawned = false;
        }

        return Spawned;
    }

    public void SpawnPowerUp()
    {
        var power = ObjectPooler.Instance.GetPowerUp();
        power.SetActive(true);
        power.transform.position = ObjectPooler.Instance.GetBoundary.SetBoundaries();
    }

    private void FindPowerUp()
    {
        var power = FindObjectOfType<PowerUps>(true);

        powerUp = power;

        PowerUpTime = powerUp.GetPowerTime;
    }
}