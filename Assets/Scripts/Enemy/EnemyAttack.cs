using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private static readonly WaitForSeconds _attackInterval = new(0.2f);

    [SerializeField] float _enemyattack;

    bool _isEnemyAttacking = false;
    bool _isDamageCoroutineOn = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerHealth playerHP = other.GetComponent<PlayerHealth>();

            _isEnemyAttacking = true;
            if(!_isDamageCoroutineOn)
            {
                StartCoroutine(DamageRoutine(playerHP));
                _isDamageCoroutineOn = true;
            }   
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            _isEnemyAttacking = false;
        }
    }

    // Repeatedly damage the player while the enemy remains in contact.
    IEnumerator DamageRoutine(PlayerHealth playerHP)
    {
        while(_isEnemyAttacking && playerHP != null)
        {
            playerHP.Damage(_enemyattack);
            yield return _attackInterval;
        }
        _isDamageCoroutineOn = false;
    }
}