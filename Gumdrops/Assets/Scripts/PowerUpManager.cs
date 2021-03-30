using UnityEngine;
using UnityEngine.UI;

public class PowerUpManager : MonoBehaviour
{
    private PowerUps powerUp;

    [SerializeField]
    private PowerUpSymbol powerUpSymbol;

    [SerializeField]
    private Transform PowerUpSymbolParent;

    [SerializeField]
    private float PowerUpSpawnTime, PowerUpSpawnChance;

    [SerializeField]
    private float DefaultPowerUpTime;

    public Transform GetPowerUpSymbolParent
    {
        get
        {
            return PowerUpSymbolParent;
        }
        set
        {
            PowerUpSymbolParent = value;
        }
    }

    private void OnEnable()
    {
        ResetPowerUpTime();
    }

    private void Start()
    {
        FindPowerUp();
    }

    private void Update()
    {
        DefaultPowerUpTime -= Time.deltaTime;
        if(DefaultPowerUpTime <= 0)
        {
            DefaultPowerUpTime = 0;
            return;
        }
    }

    public bool PowerUpSpawn()
    {
        bool Spawned = false;

        if(Random.value * 100 <= PowerUpSpawnChance && !powerUp.gameObject.activeInHierarchy && DefaultPowerUpTime <= 0)
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
    }

    public void CreatePowerUpSymbol()
    {
        var pus = Instantiate(powerUpSymbol);

        pus.GetPowerUps = powerUp;
        pus.GetPowerUpTime = powerUp.GetPowerTime;
        pus.GetPowerUpIndex = powerUp.GetPowerUpIndex;

        pus.transform.SetParent(PowerUpSymbolParent);

        pus.GetComponent<Image>().sprite = powerUp.GetComponent<SpriteRenderer>().sprite;
    }

    public void ResetPowerUpTime()
    {
        DefaultPowerUpTime = PowerUpSpawnTime;
    }
}