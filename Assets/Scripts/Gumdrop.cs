using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Gumdrop : MonoBehaviour
{
    [SerializeField]
    private float MainMenuMoveSpeed, GameMoveSpeed, RotationSpeed, MoveSpeed, DefaultMoveSpeed, IncrementalMoveSpeed;

    [SerializeField]
    private int ScoreValue;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private ParticleSystem ParticleEffect;

    [SerializeField]
    private BoxCollider2D boxCollider2D;

    [SerializeField]
    private AudioSource source;

    [SerializeField]
    private AudioClip GumDropHitSoundEffect, WrongColorHitSoundEffect;

    private AudioClip SoundEffectToPlay;

    [SerializeField]
    private Sprite[] GumDropColors;

    [SerializeField]
    private ColorList[] colors;

    [SerializeField]
    private Color[] color;

    [SerializeField]
    private int DefaultScore, ColorIndex;

    private bool StoppedGumDrop, HitRightColor;

    private ObjectPooler objectPooler;

    private ScoreManager scoreManager;

    private TargetColor targetColor;

    public ObjectPooler GetObjectPooler
    {
        get
        {
            return objectPooler;
        }
        set
        {
            objectPooler = value;
        }
    }

    public TargetColor GetTargetColor
    {
        get
        {
            return targetColor;
        }
        set
        {
            targetColor = value;
        }
    }

    public ScoreManager GetScoreManager
    {
        get
        {
            return scoreManager;
        }
        set
        {
            scoreManager = value;
        }
    }

    public SpriteRenderer GetSpriteRenderer
    {
        get
        {
            return spriteRenderer;
        }
        set
        {
            spriteRenderer = value;
        }
    }

    public Animator GetAnimator
    {
        get
        {
            return animator;
        }
        set
        {
            animator = value;
        }
    }

    public int GetScoreValue
    {
        get
        {
            return ScoreValue;
        }
        set
        {
            ScoreValue = value;
        }
    }

    public int GetDefaultScore
    {
        get
        {
            return DefaultScore;
        }
        set
        {
            DefaultScore = value;
        }
    }

    public int GetColorIndex
    {
        get
        {
            return ColorIndex;
        }
        set
        {
            ColorIndex = value;
        }
    }

    public float GetMoveSpeed
    {
        get
        {
            return MoveSpeed;
        }
        set
        {
            MoveSpeed = value;
        }
    }

    public float GetMainMenuMoveSpeed
    {
        get
        {
            return MainMenuMoveSpeed;
        }
        set
        {
            MainMenuMoveSpeed = value;
        }
    }

    public float GetGameMoveSpeed
    {
        get
        {
            return GameMoveSpeed;
        }
        set
        {
            GameMoveSpeed = value;
        }
    }

    public float GetDefaultMoveSpeed
    {
        get
        {
            return DefaultMoveSpeed;
        }
        set
        {
            DefaultMoveSpeed = value;
        }
    }

    public float GetIncrementalMoveSpeed
    {
        get
        {
            return IncrementalMoveSpeed;
        }
        set
        {
            IncrementalMoveSpeed = value;
        }
    }

    public float GetRotationSpeed
    {
        get
        {
            return RotationSpeed;
        }
        set
        {
            RotationSpeed = value;
        }
    }

    public bool GetStoppedGumDrop
    {
        get
        {
            return StoppedGumDrop;
        }
        set
        {
            StoppedGumDrop = value;
        }
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        animator = GetComponent<Animator>();

        ParticleEffect.GetComponent<ParticleSystem>();

        boxCollider2D.GetComponent<BoxCollider2D>();

        source.GetComponent<AudioSource>();

        DefaultScore = ScoreValue;

        SoundEffectToPlay = GumDropHitSoundEffect;
    }

    private void OnEnable()
    {
        animator.SetBool("SetAnimation", false);

        spriteRenderer.enabled = true;

        ParticleEffect.gameObject.SetActive(false);

        boxCollider2D.enabled = true;

        StoppedGumDrop = false;

        if (targetColor != null)
        {
            if(targetColor.GetTimeStarted)
            {
                if (targetColor.GetStaticColor)
                {
                    spriteRenderer.sprite = targetColor.GetGumDropColors[targetColor.GetColorIndex];
                }
                else
                {
                    ChooseColor();
                }
            }
            else
            {
                ChooseColor();
            }
        }
        else
        {
            ChooseColor();
        }
    }

    private void Update()
    {
        if(!StoppedGumDrop)
        {
            transform.position += -Vector3.up * MoveSpeed * Time.deltaTime;

            transform.Rotate(0, 0, RotationSpeed * Time.deltaTime);
        }
    }

    public void ChooseColor()
    {
        if(targetColor != null)
        {
            if (Random.value * 100 <= 25)
            {
                ColorIndex = targetColor.GetColorIndex;

                spriteRenderer.sprite = GumDropColors[ColorIndex];
            }
            else
            {
                ColorIndex = Random.Range(0, colors.Length);

                spriteRenderer.sprite = GumDropColors[ColorIndex];
            }
        }
        else
        {
            ColorIndex = Random.Range(0, colors.Length);

            spriteRenderer.sprite = GumDropColors[ColorIndex];
        }
    }

    public void CheckColor()
    {
        if(ColorIndex == targetColor.GetColorIndex)
        {
            scoreManager.GetCorrectGumDropInputs++;

            AddGumDropScore();

            GetStoppedGumDrop = true;

            boxCollider2D.enabled = false;

            animator.SetBool("SetAnimation", true);

            HitRightColor = true;
        }
        else
        {
            scoreManager.GetIncorrectGumDropInputs++;

            SubtractGumDropScore();

            scoreManager.GetCorrectGumDropInputs = 0;

            animator.SetBool("SetAnimation", true);
        }
    }

    public void StopGumDrop()
    {
        MoveSpeed = 0;
        RotationSpeed = 0;
    }

    public void AddGumDropScore()
    {
        scoreManager.ScorePoints(ScoreValue);

        scoreManager.GetScoreText.color = scoreManager.GetAddScoreColor;

        scoreManager.CheckBestScoreText();
    }

    public void SubtractGumDropScore()
    {
        scoreManager.ScorePoints(-ScoreValue * scoreManager.GetIncorrectGumDropInputs);

        if(scoreManager.GetScore > 0)
        {
            scoreManager.GetScoreText.color = scoreManager.GetSubtractScoreColor;
        }
    }

    public void PlayParticle()
    {
        if(HitRightColor)
        {
            ParticleEffect.gameObject.SetActive(true);

            var ps = ParticleEffect.main;

            ps.startColor = color[ColorIndex];

            spriteRenderer.enabled = false;

            SoundEffectToPlay = GumDropHitSoundEffect;

            source.clip = SoundEffectToPlay;

            source.Play();

            StartCoroutine("WaitToReturnToQueue");
        }
        else
        {
            SoundEffectToPlay = WrongColorHitSoundEffect;

            source.clip = SoundEffectToPlay;

            source.Play();

            animator.SetBool("SetAnimation", false);
        }
    }

    private IEnumerator WaitToReturnToQueue()
    {
        yield return new WaitForSeconds(1);
        HitRightColor = false;
        objectPooler.ReturnGumDropToPool(gameObject);
    }
}