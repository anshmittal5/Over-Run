using System.Collections;
using UnityEngine;

public class HandGun : MonoBehaviour
{
    WaitForSeconds _spawnRate;

    float _nearistEnemyDistance;
    float _rotationAngle;

    Transform _closestEnemy;
    Vector2 _directionToEnemy;

    [SerializeField] float _bulletSpawnTime;

    [SerializeField] Transform _gunFirePoint;
    [SerializeField] GameObject _bullet;
    [SerializeField] GameObject _muzzleFlash;
    [SerializeField] AudioSource _audioSource;

    // Set up the bullet firing interval.
    void Awake()
    {
        _spawnRate = new(_bulletSpawnTime);
    }
    void Start()
    {
        StartCoroutine(AutoFire());
    }

    // Find the closest enemy and aim the gun toward it.
    void Update()
    {
        _nearistEnemyDistance = float.PositiveInfinity;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if(enemies.Length != 0)
        {
            foreach(GameObject enemy in enemies)
            {
                float currentEnemyDistance = Vector2.Distance(transform.position, enemy.transform.position);

                if(currentEnemyDistance < _nearistEnemyDistance)
                {
                    _nearistEnemyDistance = currentEnemyDistance;
                    _closestEnemy = enemy.transform;
                }
            }
            _directionToEnemy = _closestEnemy.position - transform.position;
            _rotationAngle = Mathf.Atan2(_directionToEnemy.y, _directionToEnemy.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0,0, _rotationAngle);
        }
    }

    IEnumerator AutoFire()
    {
        while (true)
        {
            Instantiate(_bullet, _gunFirePoint.position, _gunFirePoint.rotation);
            GameObject flash = Instantiate(_muzzleFlash, _gunFirePoint.position, _gunFirePoint.rotation);
            Destroy(flash, 0.05f);
            _audioSource.Play();
            yield return _spawnRate;
        }
    }
}