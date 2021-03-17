using UnityEngine;

public class GumdropReset : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Gumdrop>())
        {
            WaveSpawner.Instance.ReturnGumDropToPool(collision.gameObject);
        }
    }
}
