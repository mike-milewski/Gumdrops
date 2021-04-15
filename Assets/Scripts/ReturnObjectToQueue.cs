using UnityEngine;

public enum QueuedObject { SameColorPower }

public class ReturnObjectToQueue : MonoBehaviour
{
    [SerializeField]
    private QueuedObject queuedObject;

    [SerializeField]
    private float LifeTime;

    private float DefaultLifeTime;

    private void OnEnable()
    {
        DefaultLifeTime = LifeTime;
    }

    private void OnDisable()
    {
        //CheckObject();
    }

    private void Update()
    {
        DefaultLifeTime -= Time.deltaTime;
        if(DefaultLifeTime <= 0)
        {
            CheckObject();
        }
    }

    private void CheckObject()
    {
        switch(queuedObject)
        {
            case (QueuedObject.SameColorPower):
                ObjectPooler.Instance.ReturnSameColorPowerEffectToPool(gameObject);
                break;
        }
    }
}
