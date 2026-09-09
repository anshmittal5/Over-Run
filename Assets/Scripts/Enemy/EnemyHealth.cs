using System.Collections;
using TMPro;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private static readonly WaitForSeconds _changeColor = new(0.15f);

    [SerializeField] int _enemyHealth;
    readonly int _enemyMaxHealth = 100;

    [SerializeField] int _enemyPoints;

    [SerializeField] GameObject _damageNumPrefab;
    [SerializeField] GameObject _bloodParticles;
    [SerializeField] GameObject _deathParticles;

    [SerializeField] AudioSource _hurtAudioSource;
    [SerializeField] AudioSource _deathAudioSource;

    [SerializeField] SpriteRenderer _spriteRenderer;

    GameObject _damageNumCanvas;

    Score _score;

    Color _enemyColor;

    void Awake()
    {
        _enemyHealth = _enemyMaxHealth;
        
        GameObject score = GameObject.Find("Score");
        if(!score.TryGetComponent(out _score))
        {
            Debug.LogError("enemy health : score is null");
            return;
        }

        _damageNumCanvas = GameObject.Find("DamageCanvas");
        if(_damageNumCanvas == null)
        {
            Debug.LogError("enemy health : damage canvas is null");
            return;
        }

        _enemyColor = _spriteRenderer.color;
    }

    public void TakeDamage(int damage)
    {
        _enemyHealth -= damage;

        SpawnDamageNumber(damage);

        if(_enemyHealth <= 0)
        {
            EnemyDeath();
        }
        else
        {
            EnemyHurt();
        }
    }

    void EnemyHurt()
    {
        _hurtAudioSource.Play();
        StartCoroutine(EnemyFlash());
        Instantiate(_bloodParticles, transform.position, Quaternion.identity);
    }

    // Award points, play death feedback, and remove the enemy.
    void EnemyDeath()
    {
        _score.AddScore(_enemyPoints);
        _deathAudioSource.Play();
        Instantiate(_deathParticles, transform.position, Quaternion.identity);
        _spriteRenderer.enabled = false;
        Destroy(gameObject, 0.3f);
    }

    // Spawn the floating damage number above the enemy.
    void SpawnDamageNumber(int damage)
    {
        GameObject damNum = Instantiate(_damageNumPrefab, transform.position + (Vector3.up * 1f),
        Quaternion.identity, _damageNumCanvas.transform);

        TMP_Text damageText = damNum.GetComponent<TMP_Text>();
        damageText.text = "-" + damage;

        Destroy(damNum, 0.25f);
    }

    IEnumerator EnemyFlash()
    {
        _spriteRenderer.color = Color.white;
        yield return _changeColor;
        _spriteRenderer.color = _enemyColor;
    }

}
