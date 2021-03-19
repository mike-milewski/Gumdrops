using UnityEngine;

public class TouchInput : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    private Vector2 touchPosition;

    private void Start()
    {
        cam.GetComponent<Camera>();
    }

    void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            touchPosition = cam.ScreenToWorldPoint(touch.position);

            if (touch.phase == TouchPhase.Began)
            {
                Collider2D touchedCollider = Physics2D.OverlapPoint(touchPosition);
                if(touchedCollider == null)
                {
                    return;
                }
                else
                {
                    Debug.Log("Test");
                    //touchedCollider.gameObject.SetActive(false);
                }
            }
        }
    }
}
