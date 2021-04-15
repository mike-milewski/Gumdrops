using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    [SerializeField]
    private float LifeTime;

    private void OnDisable()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        Destroy(gameObject, LifeTime);
    }
}
