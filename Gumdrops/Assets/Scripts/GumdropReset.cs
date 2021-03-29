using UnityEngine;

public class GumdropReset : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Gumdrop>())
        {
            ObjectPooler.Instance.ReturnGumDropToPool(collision.gameObject);
        }
        if (collision.GetComponent<PowerUps>())
        {
            ObjectPooler.Instance.ReturnPowerUpToPool(collision.gameObject);
        }
    }
}
