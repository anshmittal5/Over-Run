using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Slider _healthBar;
    
    PlayerHealth _playerHp;

    void Awake()
    {
        GameObject playerHp = GameObject.FindWithTag("Player");
        if(!playerHp.TryGetComponent(out _playerHp))
        {
            Debug.LogError("health bar: player is null");
            return;
        }
    }

    // Keep the health bar synced with the player's current health.
    void Update()
    {        
        _healthBar.value = _playerHp.GetHealth();
    }
}