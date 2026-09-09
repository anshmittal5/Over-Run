using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float _bulletSpeed;
    [SerializeField] int _bulletDamagePower;
    [SerializeField] float _destroyTime;
    [SerializeField] float _knockbackPower;

    // Set the bullet's lifetime.
    void Start()
    {
        Destroy(gameObject , _destroyTime);
    }

    // Move the bullet in the direction it was fired.
    void Update()
    {
        transform.Translate(_bulletSpeed * Time.deltaTime * Vector2.right);
    }

    // Damage and knock back an enemy when the bullet hits it.
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Enemy"))
        {
            if(other.TryGetComponent<EnemyHealth>(out var enemyHP))
            {
                enemyHP.TakeDamage(_bulletDamagePower);
            }
            if(other.TryGetComponent<Enemy>(out var enemy))
            {
                Vector2 dir = (other.transform.position - transform.position).normalized;
                enemy.KnockBack(dir, _knockbackPower);
            }
            Destroy(gameObject);
        }
    }
}