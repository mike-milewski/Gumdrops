using UnityEngine;

public class ScaleGumDropTrigger : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private Canvas canvas;

    [SerializeField]
    private BoxCollider2D boxCollider2D;

    private float ResolutionX, ResolutionY;

    private void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();

        ResolutionX = Screen.width;
        ResolutionY = Screen.height;

        boxCollider2D.size = new Vector2(Screen.width * 2, boxCollider2D.size.y);

        SetPosition();
    }

    private void Update()
    {
        if(Screen.width != ResolutionX && Screen.height != ResolutionY)
        {
            boxCollider2D.size = new Vector2(Screen.width * 2, boxCollider2D.size.y);

            ResolutionX = Screen.width;
            ResolutionY = Screen.height;

            SetPosition();
        }
    }

    private void SetPosition()
    {
        transform.position = new Vector2(0, transform.position.y);
    }
}
