using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TouchInput : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private LayerMask layermask;

    private Vector2 touchPosition;

    private Scene scene;

    private void Start()
    {
        cam.GetComponent<Camera>();

        scene = SceneManager.GetActiveScene();
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

                if(hit && hit.collider.GetComponent<Gumdrop>() && !EventSystem.current.IsPointerOverGameObject(0) && scene.buildIndex != 0 && Time.timeScale > 0)
                {
                    hit.collider.GetComponent<Gumdrop>().CheckColor();

                    hit.collider.GetComponent<Gumdrop>().GetStoppedGumDrop = true;

                    hit.collider.GetComponent<BoxCollider2D>().enabled = false;

                    hit.collider.GetComponent<Gumdrop>().GetAnimator.SetBool("SetAnimation", true);
                }
                else if(hit && hit.collider.GetComponent<PowerUps>() && !EventSystem.current.IsPointerOverGameObject(0) && Time.timeScale > 0)
                {
                    hit.collider.GetComponent<PowerUps>().GetPower();

                    hit.collider.GetComponent<PowerUps>().GetCircleCollider2D.enabled = false;

                    hit.collider.GetComponent<SpriteRenderer>().enabled = false;

                    hit.collider.GetComponent<PowerUps>().GetAudioSource.Play();

                    hit.collider.GetComponent<PowerUps>().GetPowerParticle.SetActive(false);

                    StartCoroutine(hit.collider.GetComponent<PowerUps>().WaitToReturnToQueue());
                }
                else
                {
                    SpawnTouchParticle();
                }
            }
        }
    }

    private void SpawnTouchParticle()
    {
        var GO = ObjectPooler.Instance.GetTouchParticle();
        GO.SetActive(true);
        GO.transform.position = touchPosition;
    }
}