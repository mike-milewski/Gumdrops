using UnityEngine;

public class Gumdrop : MonoBehaviour
{
    [SerializeField]
    private float MoveSpeed, RotationSpeed;

    private void Update()
    {
        transform.position += -Vector3.up * MoveSpeed * Time.deltaTime;

        transform.Rotate(0, 0, RotationSpeed * Time.deltaTime);
    }
}
