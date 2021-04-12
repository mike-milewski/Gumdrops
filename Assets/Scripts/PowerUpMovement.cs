using UnityEngine;

public class PowerUpMovement : MonoBehaviour
{
    [SerializeField]
    private float DefaultMoveSpeed;

    private float MoveSpeed;

    public float GetDefaultMoveSpeed
    {
        get
        {
            return DefaultMoveSpeed;
        }
        set
        {
            DefaultMoveSpeed = value;
        }
    }

    public float GetMoveSpeed
    {
        get
        {
            return MoveSpeed;
        }
        set
        {
            MoveSpeed = value;
        }
    }

    private void OnEnable()
    {
        MoveSpeed = DefaultMoveSpeed;
    }

    private void Update()
    {
        transform.position += -Vector3.up * MoveSpeed * Time.deltaTime;
    }
}