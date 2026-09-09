using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Transform _player;

    WorldBuilder _worldBuilder;

    [SerializeField] float _cameraZoomOut;


    int _minWorldRange;
    int _maxWorldRange;

    float _cameraHalfHeight;
    float _cameraHalfWidth;

    void Awake()
    {
        GameObject playerObj = GameObject.FindWithTag("Player");
        if(playerObj == null)
        {
            Debug.LogError("camara: player is null");
            return;
        }
        _player = playerObj.transform;

        GameObject obj = GameObject.Find("WorldBuilder");
        if(!obj.TryGetComponent(out _worldBuilder))
        {
            Debug.LogError("camera: world builder is null");
            return;
        }

        _cameraHalfHeight = Camera.main.orthographicSize;
        _cameraHalfWidth = _cameraHalfHeight * Camera.main.aspect;
    }

    void Start()
    {
        _minWorldRange = _worldBuilder.GiveMinRange();
        _maxWorldRange = _worldBuilder.GiveMaxRange();
    }

    void LateUpdate()
    {
        if(_player == null)
        {
            GameObject playerObj = GameObject.FindWithTag("Player");
            if(playerObj == null)
            return;
            _player = playerObj.transform;
        }
       
        float vic = Mathf.Clamp(_player.position.x, _minWorldRange + _cameraHalfWidth, _maxWorldRange - _cameraHalfWidth);
        float sic = Mathf.Clamp(_player.position.y, _minWorldRange + _cameraHalfHeight, _maxWorldRange - _cameraHalfHeight);
        Vector3 player = new(vic , sic , _cameraZoomOut);
        transform.position =  player;
    }
}
