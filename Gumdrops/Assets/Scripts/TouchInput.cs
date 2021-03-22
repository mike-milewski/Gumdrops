using UnityEngine;
using UnityEngine.EventSystems;

public class TouchInput : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private LayerMask layermask;

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
                RaycastHit2D hit = Physics2D.Raycast(touchPosition, touch.position, layermask);

                if(hit && hit.collider.GetComponent<Gumdrop>() && !EventSystem.current.IsPointerOverGameObject(0))
                {
                    Debug.Log(hit.collider.name);
                }
                else
                {
                    return;
                    //touchedCollider.gameObject.SetActive(false);
                }
            }
        }
    }
}
