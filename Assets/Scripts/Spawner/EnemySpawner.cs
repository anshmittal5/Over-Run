using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    WaitForSeconds _waitForSeconds;

    [SerializeField] float _spawnTime;
    [SerializeField] GameObject _enemyPrefab;
    [SerializeField] GameObject _enemyCon;


    WorldBuilder _worldBuilder;

    Transform _camera;

    Score _score;

    GameTimer _gameTimer;

    int _minWorldRange;
    int _maxWorldRange;
    float _cameraHalfHeight;
    float _cameraHalfWidth;

    bool _isSpawning = true;

    Coroutine _storeEnemySpawning;

    void Awake()
    {
        _waitForSeconds = new(_spawnTime);

        GameObject obj = GameObject.Find("WorldBuilder");
        if(!obj.TryGetComponent(out _worldBuilder))
        {
            Debug.LogError("enemy spawner: world Builder is null");
            return;
        }

        GameObject camobj = GameObject.FindWithTag("MainCamera");
        if(camobj == null)
        {
            Debug.LogError("enemy spawner: main camera is null");
            return;
        }
        _camera = camobj.transform;
        _cameraHalfHeight = Camera.main.orthographicSize;
        _cameraHalfWidth = _cameraHalfHeight * Camera.main.aspect;

        GameObject sco = GameObject.Find("Score");
        if(!sco.TryGetComponent(out _score))
        {
            Debug.LogError("enemy spawner : score is null");
            return;
        }

        GameObject time = GameObject.Find("Timer");
        if(!time.TryGetComponent(out _gameTimer))
        {
            Debug.LogError("enemy spawner : game timer is null");
            return;
        }
    }

    void Start() 
    {
        _minWorldRange = _worldBuilder.GiveMinRange();
        _maxWorldRange = _worldBuilder.GiveMaxRange();
        _storeEnemySpawning = StartCoroutine(EnemySpawning());
    }

    // spawn enemy every 0.8 seconds on a new random position
    // Spawn enemies at random positions outside the camera's view.
    IEnumerator EnemySpawning()
    {
        while(_isSpawning)
        {
            Vector2 posToSpawn = new(Random.Range(_minWorldRange, _maxWorldRange),
            Random.Range(_minWorldRange, _maxWorldRange));
            Vector2 campos = _camera.position;
            float bottomleftX = campos.x - _cameraHalfWidth;
            float bottomleftY = campos.y - _cameraHalfHeight;
            Rect cameraRect = new(bottomleftX,bottomleftY ,_cameraHalfWidth*2 ,_cameraHalfHeight*2);
            if(cameraRect.Contains(posToSpawn))
            {
                continue;
            }
        
            Instantiate(_enemyPrefab,posToSpawn,Quaternion.identity,_enemyCon.transform);

            yield return _waitForSeconds;
        }
    }

    // when player is dead this function is called from player script 
    // stop spawning new enemies
    // destroy all the enemies still around
    public void StopEnemySpawning()
    {
        _isSpawning = false;
        foreach(Transform enemy in _enemyCon.transform)
        {
            Destroy(enemy.gameObject);
        }
        if(_storeEnemySpawning != null)
        {
            StopCoroutine(_storeEnemySpawning);
            _storeEnemySpawning = null;
        }
    }

    public void StartSpawning()
    {
        if (!_isSpawning)
        {
            _isSpawning = true;
            _storeEnemySpawning = StartCoroutine(EnemySpawning());
            _score.ReSetScore();
            _gameTimer.ReSetTimer();
        }
    }
}