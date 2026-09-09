using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldBuilder : MonoBehaviour
{
    [SerializeField] int _worldSize;

    [SerializeField] Tilemap _tiles;

    [SerializeField] Tile _grassBlock;

    int _minWorldRange;
    int _maxWorldRange;

    void Awake()
    {
        _minWorldRange = -_worldSize/2;
        _maxWorldRange = _worldSize/2;
    }

    void Start()
    {
        for (int i = _minWorldRange; i < _maxWorldRange; i++)
        {
            for (int u = _minWorldRange; u < _maxWorldRange; u++)
            {
                _tiles.SetTile(new Vector3Int(i,u,0),_grassBlock);
            }
        }
    }

    public int GiveMinRange()
    {
        return _minWorldRange;
    }

    public int GiveMaxRange()
    {
        return _maxWorldRange;
    }
}
