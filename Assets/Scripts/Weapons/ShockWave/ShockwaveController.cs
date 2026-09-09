using UnityEngine;
using UnityEngine.InputSystem;

public class ShockwaveController : MonoBehaviour
{
    PlayerControls _controls;

    [SerializeField] float _cooldownDuration;
    [SerializeField] float _cooldownTimer;

    [SerializeField] GameObject _shockWave;

    [SerializeField] AudioSource _shockwaveSound;

    void Awake()
    {
        _controls = new PlayerControls();
        _controls.Weapon.ShockWave.performed += ShockWave;
    }

    void OnEnable()
    {
        _controls.Enable();
    }

    void OnDisable()
    {
        _controls.Disable();
    }

    // Update the remaining shockwave cooldown.
    void Update()
    {
        _cooldownTimer -= Time.deltaTime;
        _cooldownTimer = Mathf.Clamp(_cooldownTimer ,0 ,_cooldownDuration);
    }

    // Use the shockwave when its cooldown is ready.
    void ShockWave(InputAction.CallbackContext context)
    {
        if(_cooldownTimer <= 0)
        {
            _cooldownTimer = _cooldownDuration;
            _shockwaveSound.Play();
            Instantiate(_shockWave, transform.position, Quaternion.identity, transform);
        }
    }

    // Provide the remaining cooldown to the UI.
    public float GetCooldownTime()
    {
        return _cooldownTimer;
    }
}