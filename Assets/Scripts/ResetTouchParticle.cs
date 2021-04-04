using UnityEngine;

public class ResetTouchParticle : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem particle;

    private void Start()
    {
        particle = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if(!particle.IsAlive(false))
        {
            ObjectPooler.Instance.ReturnTouchParticleToPool(gameObject);
        }
    }
}
