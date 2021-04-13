using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ColorList { Red, Blue, Green, Yellow }

public class TargetColor : MonoBehaviour
{
    [SerializeField]
    private ColorList[] color;

    [SerializeField]
    private Gradient gradient;

    [SerializeField]
    private Image TargetImage, NextColorImage, ColorFrame;

    [SerializeField]
    private Sprite[] GumDropColors;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private float DefaultTimeToChangeColor, MinimumTimeToChangeColor;

    [SerializeField]
    private bool StaticColor;

    [SerializeField]
    private List<Gumdrop> gumDrops;

    private float TimeToChangeColor, CurrentTimeToChangeColor;

    private int ColorIndex, RandomIndex;

    private bool AboutToSwitchColor;

    public Image GetTargetImage
    {
        get
        {
            return TargetImage;
        }
        set
        {
            TargetImage = value;
        }
    }

    public Image GetNextColorImage
    {
        get
        {
            return NextColorImage;
        }
        set
        {
            NextColorImage = value;
        }
    }

    public Sprite[] GetGumDropColors
    {
        get
        {
            return GumDropColors;
        }
        set
        {
            GumDropColors = value;
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

    public bool GetStaticColor
    {
        get
        {
            return StaticColor;
        }
        set
        {
            StaticColor = value;
        }
    } 

    public bool GetAboutToSwitchColor
    {
        get
        {
            return AboutToSwitchColor;
        }
        set
        {
            AboutToSwitchColor = value;
        }
    }

    private void OnEnable()
    {
        StartCoroutine("WaitToStartGame");
    }

    private void Update()
    {
        if(!StaticColor)
        {
            TimeToChangeColor -= Time.deltaTime;

            ColorFrame.fillAmount = TimeToChangeColor / CurrentTimeToChangeColor;

            ChangeColorFrame();

            if(TimeToChangeColor <= 3 && !AboutToSwitchColor)
            {
                AboutToSwitchColor = true;
                RandomColorIndex();
            }
            if (TimeToChangeColor <= 0)
            {
                PlayTargetImageAnimation();
                AboutToSwitchColor = false;
                NextColorImage.GetComponent<Animator>().SetBool("SetAnimation", false);
                TimeToChangeColor = CurrentTimeToChangeColor;
            }
        }
    }

    private void RandomColorIndex()
    {
        var gd = FindObjectsOfType<Gumdrop>(false);

        foreach(Gumdrop gumdrop in gd)
        {
            gumDrops.Add(gumdrop);
        }

        for(int i = 0; i < gd.Length; i++)
        {
            if(gd[i].GetColorIndex == ColorIndex)
            {
                gumDrops.Remove(gd[i]);
            }
        }

        int Rand = Random.Range(0, gumDrops.Count);

        RandomIndex = gumDrops[Rand].GetColorIndex;

        NextColorImage.sprite = GumDropColors[RandomIndex];

        NextColorImage.GetComponent<Animator>().SetBool("SetAnimation", true);

        gumDrops.Clear();
    }

    private void PlayTargetImageAnimation()
    {
        TargetImage.GetComponent<Animator>().SetBool("SetAnimation", true);
    }

    public void ChooseColor()
    {
        TargetImage.GetComponent<Animator>().SetBool("SetAnimation", false);
        TargetImage.sprite = GumDropColors[RandomIndex];
        ColorIndex = RandomIndex;
    }

    private void ChooseRandomColorFromStart()
    {
        ColorIndex = Random.Range(0, color.Length);

        TargetImage.sprite = GumDropColors[ColorIndex];
    }

    private Color ColorFromGradient(float value)
    {
        return gradient.Evaluate(value);
    }

    private void ChangeColorFrame()
    {
        ColorFrame.color = ColorFromGradient(TimeToChangeColor / CurrentTimeToChangeColor);
    }

    public void SetStartingTimeAndColor()
    {
        TimeToChangeColor = DefaultTimeToChangeColor;
        CurrentTimeToChangeColor = DefaultTimeToChangeColor;

        ChooseRandomColorFromStart();
    }

    public void ReduceTimer()
    {
        if(CurrentTimeToChangeColor > MinimumTimeToChangeColor)
        {
            CurrentTimeToChangeColor -= 1;
        }
        TimeToChangeColor = CurrentTimeToChangeColor;
    }

    private IEnumerator WaitToStartGame()
    {
        yield return new WaitForSeconds(1f);
        TargetImage.GetComponent<Animator>().SetTrigger("SetFirstAnimation");
        SetStartingTimeAndColor();
        StaticColor = false;
    }

    public void PlayAudioSource()
    {
        audioSource.Play();
    }
}