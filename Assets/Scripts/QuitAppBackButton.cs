using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuitAppBackButton : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager = null;

    [SerializeField]
    private GameObject QuitMenu;

    [SerializeField]
    private Button YesBtn, NoBtn, MenuBtn = null;

    [SerializeField]
    private Button[] buttons;

    [SerializeField]
    private AudioSource audioSource;

    private Scene scene;

    [SerializeField]
    private bool CanQuit;

    private bool IsMenuOpened;

    private void OnEnable()
    {
        scene = SceneManager.GetActiveScene();
    }

    private void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if(CanQuit)
                {
                    if (!IsMenuOpened)
                    {
                        QuitMenu.GetComponent<Animator>().SetBool("OpenMenu", true);
                        DisableButtons();
                        EnableYesAndNoButtons();
                        if(MenuBtn != null)
                        {
                            MenuBtn.interactable = false;
                        }
                        audioSource.Play();
                        IsMenuOpened = true;

                        if (CheckMainGameScene())
                        {
                            Time.timeScale = 0;
                        }
                    }
                    else return;
                }
                else return;
            }
        }
    }

    private bool CheckMainGameScene()
    {
        bool Correct = false;

        if(scene.buildIndex == 1)
        {
            Correct = true;
        }
        else
        {
            Correct = false;
        }

        return Correct;
    }

    public void DisableButtons()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }
    }

    public void DisableYesAndNoButtons()
    {
        YesBtn.interactable = false;
        NoBtn.interactable = false;
    }

    private void EnableYesAndNoButtons()
    {
        YesBtn.interactable = true;
        NoBtn.interactable = true;
    }

    public void EnableButtons()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = true;
        }
    }

    public void CheckTimeScale()
    {
        var Gamemanager = FindObjectOfType<GameManager>();

        if (!Gamemanager.GetTimeScaleIsZero)
        {
            Time.timeScale = 1;
        }
        else return;
    }

    public void CheckIfMenuIsOpened()
    {
        if (!gameManager.GetMenuOpened)
        {
            MenuBtn.interactable = true;
        }
    }

    public void ResetIsMenuOpened()
    {
        IsMenuOpened = false;
    }

    public void DisableCanQuit()
    {
        CanQuit = false;
    }
}