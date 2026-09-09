using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Enemy : MonoBehaviour
{
    private static readonly WaitForSeconds _knockbackOnTime = new (0.05f);

    Transform _player;
    WorldBuilder _worldBuilder;

    Rigidbody2D _rigidbody;
    Collider2D _collider;

    int _minWorldRange;
    int _maxWorldRange;
    float _halfHeight;
    float _halfWidth;


    float _distance;
    [SerializeField] float _enemySpeed;
    [SerializeField] float _radius;

    bool _isKnockBackOn = false;

    void Awake()
    {
        GameObject playerObj = GameObject.FindWithTag("Player");
        if(!playerObj.TryGetComponent(out _player))
        {
            Debug.LogError("enemy: _player is null");
            return;
        }

        GameObject obj = GameObject.Find("WorldBuilder");
        if(!obj.TryGetComponent(out _worldBuilder))
        {
            Debug.LogError("enemy: world builder is null");
            return;
        }
        _minWorldRange = _worldBuilder.GiveMinRange();
        _maxWorldRange = _worldBuilder.GiveMaxRange();

        _rigidbody = GetComponent<Rigidbody2D>();

        _collider = GetComponent<Collider2D>();
        _halfWidth = _collider.bounds.extents.x;
        _halfHeight = _collider.bounds.extents.y;
    }

    // Move toward the player while staying inside the world.
    void FixedUpdate()
    {
        if (!_isKnockBackOn)
        {
            if(_player == null)
            {
                GameObject playerObj = GameObject.FindWithTag("Player");
                if(playerObj == null)
                {
                    return;
                }
                playerObj.TryGetComponent(out _player);
            }
            
            _distance = Vector2.Distance(transform.position, _player.position);
            if(_distance <= _radius)
            {
                Vector2 enemymove = transform.position;
                enemymove = Vector2.MoveTowards(enemymove,
                _player.position, _enemySpeed * Time.fixedDeltaTime);

                float vic = Mathf.Clamp(enemymove.x, _minWorldRange + _halfWidth, _maxWorldRange - _halfWidth);
                float sic = Mathf.Clamp(enemymove.y, _minWorldRange + _halfHeight, _maxWorldRange - _halfHeight);
                enemymove = new(vic,sic);

                _rigidbody.MovePosition(enemymove);
            }
        }
    }

    // Apply knockback and temporarily prevent normal movement.
    public void KnockBack(Vector2 direction, float force)
    {
        _rigidbody.AddForce(direction * force, ForceMode2D.Impulse);
        _isKnockBackOn = true;
        StartCoroutine(TurnKnockbackOff());
    }

    IEnumerator TurnKnockbackOff()
    {
        yield return _knockbackOnTime;
        _isKnockBackOn = false;
    }
}