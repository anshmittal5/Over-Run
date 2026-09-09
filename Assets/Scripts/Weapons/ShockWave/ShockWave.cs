using UnityEngine;

public class ShockWave : MonoBehaviour
{
    [SerializeField] int _shockWaveDamagePower;
    [SerializeField] int _knockbackPower;

    [SerializeField] float _scaleSpeed;
    [SerializeField] float _destroyTime;

    void Start()
    {
        Destroy(gameObject , _destroyTime);
    }

    // TODO (V2): Replace placeholder circle with lightning ring effect.

    // Expand the shockwave over time.
    void Update()
    {
        transform.localScale += _scaleSpeed * Time.deltaTime *  Vector3.one;
    }

    // Damage and knock back enemies hit by the shockwave.
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Enemy"))
        {
            if(other.TryGetComponent<EnemyHealth>(out var enemyHP))
            {
                enemyHP.TakeDamage(_shockWaveDamagePower);
            }
            if(other.TryGetComponent<Enemy>(out var enemy))
            {
                Vector2 dir = (other.transform.position - transform.position).normalized;
                enemy.KnockBack(dir, _knockbackPower);
            }
        }
    }
}