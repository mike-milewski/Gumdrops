using UnityEngine;

public class TouchInput : MonoBehaviour
{
    private RaycastHit hit;

    void Update()
    {
        for (int i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                // Construct a ray from the current touch coordinates
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);

                // Create a particle if hit
                if (Physics.Raycast(ray))
                {
                    Debug.Log(Input.GetTouch(i).position);
                }
                if(Physics.Raycast(ray, out hit))
                {
                    if(hit.collider.GetComponent<Gumdrop>())
                    {
                        Debug.Log("Gumdrop");
                    }
                }
            }
        }
    }
}
