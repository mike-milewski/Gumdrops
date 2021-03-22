using UnityEngine;

public class Gumdrop : MonoBehaviour
{
    [SerializeField]
    private float MoveSpeed, RotationSpeed;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private Color[] color;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        spriteRenderer.color = color[Random.Range(0, color.Length)];
    }

    private void Update()
    {
        transform.position += -Vector3.up * MoveSpeed * Time.deltaTime;

        transform.Rotate(0, 0, RotationSpeed * Time.deltaTime);
    }
}
