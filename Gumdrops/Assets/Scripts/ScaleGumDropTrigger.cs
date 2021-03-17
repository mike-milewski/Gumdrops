using UnityEngine;

public class ScaleGumDropTrigger : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private BoxCollider2D boxCollider2D;

    private int ResolutionX, ResolutionY;

    private void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();

        ResolutionX = Screen.width;
        ResolutionY = Screen.height;

        SetPosition();
    }

    private void Update()
    {
        boxCollider2D.size = new Vector2(Screen.width, boxCollider2D.size.y);

        if(Screen.width != ResolutionX && Screen.height != ResolutionY)
        {
            SetPosition();

            ResolutionX = Screen.width;
            ResolutionY = Screen.height;
        }
    }

    private void SetPosition()
    {
        transform.position = new Vector2(Screen.width / 2, transform.position.y);
    }
}
