using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private float MinX, MaxX;

    private Vector2 bounds;

    public Vector2 SetBoundaries()
    {
        bounds = cam.ViewportToScreenPoint(new Vector3(Random.Range(MinX, MaxX), 1, 10));

        return bounds;
    }
}

[System.Serializable]
public class PoolController
{
    private Queue<GameObject> PooledObject = new Queue<GameObject>();

    [SerializeField]
    private GameObject ObjectToPool;

    [SerializeField]
    private int PoolAmount;

    public GameObject GetObjectToPool
    {
        get
        {
            return ObjectToPool;
        }
        set
        {
            ObjectToPool = value;
        }
    }

    public int GetPoolAmount
    {
        get
        {
            return PoolAmount;
        }
        set
        {
            PoolAmount = value;
        }
    }

    public Queue<GameObject> GetPooledObject
    {
        get
        {
            return PooledObject;
        }
        set
        {
            PooledObject = value;
        }
    }
}

public class WaveSpawner : MonoBehaviour
{
    public static WaveSpawner Instance = null;

    [SerializeField]
    private PoolController poolcontroller;

    [SerializeField]
    private Boundary boundary;

    [SerializeField]
    private Transform PoolTransform;

    [SerializeField]
    private float DefaultSpawnTimer, SpawnTimer;

    private void Awake()
    {
        #region Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        #endregion

        SpawnTimer = DefaultSpawnTimer;

        AddGumDrops(poolcontroller.GetPoolAmount);
    }

    private void Update()
    {
        SpawnTimer -= Time.deltaTime;
        if(SpawnTimer <= 0)
        {
            SpawnGumDrop();
            SpawnTimer = DefaultSpawnTimer;
        }
    }

    private void SpawnGumDrop()
    {
        var GO = GetGumDrop();
        GO.SetActive(true);
        GO.transform.position = boundary.SetBoundaries();
    }

    private void AddGumDrops(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var PO = Instantiate(poolcontroller.GetObjectToPool);
            PO.transform.SetParent(PoolTransform);
            PO.transform.position = new Vector2(PoolTransform.position.x, PoolTransform.position.y);
            poolcontroller.GetPooledObject.Enqueue(PO);

            PO.gameObject.SetActive(false);
        }
    }

    public GameObject GetGumDrop()
    {
        return poolcontroller.GetPooledObject.Dequeue();
    }

    public void ReturnGumDropToPool(GameObject pooledObject)
    {
        poolcontroller.GetPooledObject.Enqueue(pooledObject);
        pooledObject.SetActive(false);
    }
}
