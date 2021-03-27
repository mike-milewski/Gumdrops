using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        bounds = cam.ViewportToWorldPoint(new Vector3(Random.Range(MinX, MaxX), 1, 10));

        return bounds;
    }

    public Camera GetCamera
    {
        get
        {
            return cam;
        }
        set
        {
            cam = value;
        }
    }

    public float GetMinX
    {
        get
        {
            return MinX;
        }
        set
        {
            MinX = value;
        }
    }

    public float GetMaxX
    {
        get
        {
            return MaxX;
        }
        set
        {
            MaxX = value;
        }
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

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance = null;

    [SerializeField]
    private PoolController[] poolcontroller;

    [SerializeField]
    private Boundary boundary;

    [SerializeField]
    private Transform GumDropParent, TouchParticleParent;

    [SerializeField]
    private float DefaultSpawnTimer;

    private float SpawnTimer;

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

        AddGumDrops(poolcontroller[0].GetPoolAmount);
        AddTouchParticle(poolcontroller[1].GetPoolAmount);
    }

    private void Update()
    {
        SpawnTimer -= Time.deltaTime;
        if (SpawnTimer <= 0)
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
            var PO = Instantiate(poolcontroller[0].GetObjectToPool);
            PO.transform.SetParent(GumDropParent.transform, false);
            PO.transform.position = new Vector2(transform.position.x, transform.position.y);
            poolcontroller[0].GetPooledObject.Enqueue(PO);

            Scene scene = SceneManager.GetActiveScene();

            if(scene.buildIndex != 0)
            {
                var targetColor = FindObjectOfType<TargetColor>();
                var scoreManager = FindObjectOfType<ScoreManager>();

                PO.GetComponent<Gumdrop>().GetTargetColor = targetColor;
                PO.GetComponent<Gumdrop>().GetScoreManager = scoreManager;
                PO.GetComponent<Gumdrop>().GetObjectPooler = this;
            }

            PO.gameObject.SetActive(false);
        }
    }

    private void AddTouchParticle(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var PO = Instantiate(poolcontroller[1].GetObjectToPool);
            PO.transform.SetParent(TouchParticleParent.transform, false);
            poolcontroller[1].GetPooledObject.Enqueue(PO);

            PO.gameObject.SetActive(false);
        }
    }

    public GameObject GetGumDrop()
    {
        return poolcontroller[0].GetPooledObject.Dequeue();
    }

    public GameObject GetTouchParticle()
    {
        return poolcontroller[1].GetPooledObject.Dequeue();
    }

    public void ReturnGumDropToPool(GameObject pooledObject)
    {
        poolcontroller[0].GetPooledObject.Enqueue(pooledObject);
        pooledObject.SetActive(false);
    }

    public void ReturnTouchParticleToPool(GameObject pooledObject)
    {
        poolcontroller[1].GetPooledObject.Enqueue(pooledObject);
        pooledObject.SetActive(false);
    }
}