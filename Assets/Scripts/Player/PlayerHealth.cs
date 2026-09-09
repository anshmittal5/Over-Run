using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private static readonly WaitForSeconds _colorChangeTime = new(0.15f);

    [SerializeField] float _health;
    readonly float _maxHealth = 100;

    [SerializeField] AudioSource _hurtAudio;
    [SerializeField] AudioSource _deathAudio;
    AudioSource _backgroundMusic;

    [SerializeField] SpriteRenderer _spriteRenderer;
    Color _playerColor;

    GameObject _canvas;
    GameObject _gameOverPanel;

    EnemySpawner _enemySpawner;
    GameTimer _gameTimer;

    void Awake()
    {
        _health = _maxHealth;
        _playerColor = _spriteRenderer.color;

        _canvas = GameObject.Find("Canvas");
        _gameOverPanel = _canvas.transform.Find("GameOver").gameObject;
        
        GameObject gameT = GameObject.Find("Timer");
        if(!gameT.TryGetComponent(out _gameTimer))
        {
            Debug.LogError("player health: game timer is null");
            return;
        }

        GameObject enemysp = GameObject.Find("EnemySpawner");
        if(!enemysp.TryGetComponent(out _enemySpawner))
        {
            Debug.LogError("player health: enemy spawner is null");
            return;
        }

        GameObject backmusic = GameObject.Find("BackGroundSound");
        if(!backmusic.TryGetComponent(out _backgroundMusic))
        {
            Debug.LogError("player health : back ground sound is null");
            return;
        }

        _enemySpawner.StartSpawning();
    }

    public void Damage(float damage)
    {
        _health -= damage;
        if(_health <= 0)
        {
            Death();
        }
        else
        {
            Hurt();
        }
    }

    // Play death feedback and trigger the game-over state.
    void Death()
    {
        _deathAudio.Play();
        GameOver();
        Destroy(gameObject, 0.3f);
    }

    void Hurt()
    {
        _hurtAudio.Play();
        StartCoroutine(PlayerFlash());
    }

    IEnumerator PlayerFlash()
    {
        _spriteRenderer.color = Color.magenta;
        yield return _colorChangeTime;
        _spriteRenderer.color = _playerColor;
    }

    // Handle game-wide actions after the player dies.
    void GameOver()
    {
        _enemySpawner.StopEnemySpawning();
        _gameTimer.StopUI();
        _gameOverPanel.SetActive(true);
        _backgroundMusic.Stop();
    }

    // Provide current health to the health bar UI.
    public float GetHealth()
    {
        return _health;
    }
}