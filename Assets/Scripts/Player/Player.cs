using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Player : MonoBehaviour
{
    private static WaitForSeconds _dashOnTime = new(0.3f);
    PlayerControls _controls;
    WorldBuilder _worldBuilder;
    Rigidbody2D _rb;
    Collider2D _collider;

    [SerializeField] float _dashCooldownTimer = 2f;
    [SerializeField] float _dashCooldownDuration;

    [SerializeField] float _speed;

    [SerializeField] float _dashSpeed;
    [SerializeField] AudioSource _dashSound;

    Vector2 _movement;
    Vector2 _dashDirection;

    bool _isDashOn = false;

    int _minWorldRange;
    int _maxWorldRange;
    float _halfHeight;
    float _halfWidth;

    // Set up player components and get world boundaries
    void Awake()
    {
        _controls = new PlayerControls();
        _controls.Player.Dash.performed += PlayerDash; 
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();

        GameObject obj = GameObject.Find("WorldBuilder");
        if(obj == null)
        {
            Debug.LogError("player: _world manager is null");
            return;
        }
        _worldBuilder = obj.GetComponent<WorldBuilder>();

        _halfWidth = _collider.bounds.extents.x;
        _halfHeight = _collider.bounds.extents.y;
    }

    void OnEnable()
    {
        _controls.Enable();
    }

    void OnDisable()
    {
        _controls.Disable();
    }

    void Start()
    {
        _minWorldRange = _worldBuilder.GiveMinRange();
        _maxWorldRange = _worldBuilder.GiveMaxRange();
    }

    // Read player movements inputs and update the dash cooldown
    void Update()
    {
        Movement();

        _dashCooldownTimer -= Time.deltaTime;
        _dashCooldownTimer = Mathf.Clamp(_dashCooldownTimer ,0 ,_dashCooldownDuration);
    }

    // Move player using either using player movements or on active dash
    void FixedUpdate()
    {
        if(_isDashOn == true)
        {
            Vector2 move = _rb.position + ((_speed + _dashSpeed) * Time.fixedDeltaTime * _dashDirection);
            MoveWithinBounds(move);
        }
        else
        {
            Vector2 move = _rb.position + (_speed * Time.fixedDeltaTime * _movement);
            MoveWithinBounds(move);
        }
    }

    // Keep the player in world boundaries before moving it
    void MoveWithinBounds(Vector2 move)
    {
        float vic = Mathf.Clamp(move.x, _minWorldRange + _halfWidth, _maxWorldRange - _halfWidth);
        float sic = Mathf.Clamp(move.y, _minWorldRange + _halfHeight, _maxWorldRange - _halfHeight);
        move = new Vector2(vic,sic);
        _rb.MovePosition(move);
    }

    void Movement()
    {
        _movement = _controls.Player.Movement.ReadValue<Vector2>();
    }

    void PlayerDash(InputAction.CallbackContext context)
    {
        if(_dashCooldownTimer <= 0)
        {
            if(_movement.magnitude != 0)
            {
                _dashDirection = _movement;
                _isDashOn = true;
                _dashCooldownTimer = _dashCooldownDuration;
                _dashSound.Play();
                StartCoroutine(DashRoutine());
            }
        }
    }

    private IEnumerator DashRoutine()
    {
        yield return _dashOnTime;
        _isDashOn = false;
    }

    // Provide the current dash cooldown to other scripts.
    // like the game timer script in header for UI
    public float GiveDashTimer()
    {
        return _dashCooldownTimer;
    }
}
