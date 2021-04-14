using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum Powers { DoublePoints, Slow, SameColor }

public class PowerUps : MonoBehaviour
{
    private PowerUpManager powerUpManager;

    private ObjectPooler objectPooler;

    [SerializeField]
    private GameObject SlowPowerParticle, DoublePointsParticle, SameColorParticle;

    private GameObject PowerParticle;

    [SerializeField]
    private Sprite DoublePointsSprite, SlowSprite, SameColorSprite;

    [SerializeField]
    private AudioClip DoublePowerClip, SlowPowerClip, SameColorClip;

    private AudioClip SoundToPlay;

    [SerializeField]
    private Powers[] powers;

    private AudioSource audioSource;

    private SpriteRenderer spriteRenderer;

    private CircleCollider2D circleCollider2D;

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

    public CircleCollider2D GetCircleCollider2D
    {
        get
        {
            return circleCollider2D;
        }
        set
        {
            circleCollider2D = value;
        }
    }

    public AudioSource GetAudioSource
    {
        get
        {
            return audioSource;
        }
        set
        {
            audioSource = value;
        }
    }

    public AudioClip GetSoundToPlay
    {
        get
        {
            return SoundToPlay;
        }
        set
        {
            SoundToPlay = value;
        }
    }

    public GameObject GetPowerParticle
    {
        get
        {
            return PowerParticle;
        }
        set
        {
            PowerParticle = value;
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

        spriteRenderer.enabled = true;
        circleCollider2D.enabled = true;
    }

    private void Awake()
    {
        FindPowerUpManager();

        FindObjectPooler();

        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        audioSource = GetComponent<AudioSource>();
    }

    private void ChoosePower()
    {
        PowerUpIndex = Random.Range(0, powers.Length);

        switch (powers[PowerUpIndex])
        {
            case (Powers.DoublePoints):
                GetComponent<SpriteRenderer>().sprite = DoublePointsSprite;
                DoublePointsParticle.SetActive(true);
                SlowPowerParticle.SetActive(false);
                SameColorParticle.SetActive(false);
                SoundToPlay = DoublePowerClip;
                PowerParticle = DoublePointsParticle;
                break;
            case (Powers.Slow):
                GetComponent<SpriteRenderer>().sprite = SlowSprite;
                DoublePointsParticle.SetActive(false);
                SlowPowerParticle.SetActive(true);
                SameColorParticle.SetActive(false);
                SoundToPlay = SlowPowerClip;
                PowerParticle = SlowPowerParticle;
                break;
            case (Powers.SameColor):
                GetComponent<SpriteRenderer>().sprite = SameColorSprite;
                DoublePointsParticle.SetActive(false);
                SlowPowerParticle.SetActive(false);
                SameColorParticle.SetActive(true);
                SoundToPlay = SameColorClip;
                PowerParticle = SameColorParticle;
                break;
        }
        audioSource.clip = SoundToPlay;
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
            gd.GetSpriteRenderer.sprite = targetColor.GetTargetImage.sprite;
            gd.GetColorIndex = targetColor.GetColorIndex;
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

        foreach (Gumdrop gd in gumDrop)
        {
            gd.ChooseColor();
        }
    }

    public IEnumerator WaitToReturnToQueue()
    {
        yield return new WaitForSeconds(1f);
        objectPooler.ReturnPowerUpToPool(gameObject);
    }
}