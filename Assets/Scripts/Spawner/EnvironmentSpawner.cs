using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSpawner : MonoBehaviour
{
    [SerializeField] int _obstacleCount;
    [SerializeField] int _decorationCount;
    [SerializeField] int _objectGap;

    int _minWorldRange;
    int _maxWorldRange;

    [SerializeField] GameObject[] _decors;

    readonly List<Vector2> _decorsList = new();

    [SerializeField] GameObject[] _obstacles;

    readonly List<Vector2> _obstaclesList = new();

    WorldBuilder _worldBuilder;

    void Awake()
    {
        GameObject obj = GameObject.Find("WorldBuilder");
        if(!obj.TryGetComponent(out _worldBuilder))
        {
            Debug.LogError("environment spawner: _world manager is null");
            return;
        }
    }

    void Start()
    {
        _minWorldRange = _worldBuilder.GiveMinRange();
        _maxWorldRange = _worldBuilder.GiveMaxRange();
        ObstacleSpawner();
        DecorSpawner();
    }


    // Spawn obstacles at random positions with enough space between them.
    void ObstacleSpawner()
    {
        int items = 0;
        bool isValidPosition;
        while(items < _obstacleCount)
        {
            Vector2 newran = RandomPosition();
            
            isValidPosition = true;

            isValidPosition = ValidPosition(newran, _obstaclesList, isValidPosition);

            if (isValidPosition)
            {
                GameObject obj = Instantiate(_obstacles[Random.Range(0, _obstacles.Length)],
                newran, Quaternion.Euler(0,0, Random.Range(0, 360)),
                transform);
                float size = Random.Range(2.5f,5f);
                obj.transform.localScale = new Vector3(size, size, 1);
                _obstaclesList.Add(newran);
                items++;
            }
        }
    }

    // Spawn decorations at random positions away from other decorations and obstacles.
    void DecorSpawner()
    {
        int items = 0;
        bool isValidPosition;
        while(items < _decorationCount)
        {
            Vector2 newran = RandomPosition();
            
            isValidPosition = true;

            isValidPosition = ValidPosition(newran, _decorsList, isValidPosition);

            if (isValidPosition)
            {
                isValidPosition = ValidPosition(newran, _obstaclesList, isValidPosition);
            }

            if (isValidPosition)
            {
                GameObject obj = Instantiate(_decors[Random.Range(0, _decors.Length)],
                newran, Quaternion.Euler(0,0, Random.Range(0, 360)), transform);
                float size = Random.Range(0.3f,1f);
                obj.transform.localScale = new Vector3(size, size, 1);
                _decorsList.Add(newran);
                items++;
            }
        }
    }

    Vector2 RandomPosition()
    {
        int numx = Random.Range(_minWorldRange, _maxWorldRange);
        int numy = Random.Range(_minWorldRange, _maxWorldRange);
        Vector2 newran = new(numx,numy);
        return newran;
    }

    // Check whether a position is far enough from existing objects.
    bool ValidPosition(Vector2 NR, List<Vector2> lists, bool pos)
    {
        foreach(var item in lists)
        {
            float dis = Vector2.Distance(item, NR);
            if(dis <= _objectGap)
            {
               pos = false;
               break;
            }
        }
        return pos;
    }

    // Clear the current environment and generate a new one.
    public void ReSpawn()
    {
        foreach(Transform obj in transform)
        {
            Destroy(obj.gameObject);
        }
        _decorsList.Clear();
        _obstaclesList.Clear();
        ObstacleSpawner();
        DecorSpawner();
    }
}
