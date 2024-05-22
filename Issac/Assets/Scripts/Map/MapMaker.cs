using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapMaker : MonoBehaviour {

    [System.Serializable]
    class RoomData {
        public IRoom.RoomType Type;
        public Room RoomPrefab;
    }

    [SerializeField] GraphLayoutData[] _graphData;
    [SerializeField] RoomData[] _roomData;

    [SerializeField] Joint _joint;

    [SerializeField] GameObject _player;

    [SerializeField] float _gridSize;

    IDictionary<IRoom.RoomType, Room> _roomDict;
    Transform _mapTr;

    private void Start()
    {
        _roomDict = new Dictionary<IRoom.RoomType, Room>();

        foreach (RoomData d in _roomData) {
            _roomDict.Add(d.Type, d.RoomPrefab);
        }

        Generate();
    }
    
    public void Generate()
    {

        GameObject mapParent = new GameObject("Map");
        _mapTr = mapParent.transform;

        MapData data = new MapData(_SelectLayout());
        IRoom rootTile = _GenerateMap(data.Root);

        Instantiate(_player, rootTile.GetPosition(), Quaternion.identity);
        
    }

    GraphLayoutData _SelectLayout()
    {
        if (_graphData.Length == 0)
        {
            return GraphLayoutData.GetDefault();
        }

        return _graphData[Random.Range(0, _graphData.Length)];

    }

    IRoom _GenerateMap(IGraph<MapData.MapDataUnit>.Entry entry) {

        IRoom tile = _GenerateTile(entry.data.type, entry.data.pos);

        foreach (IGraph<MapData.MapDataUnit>.Entry child in entry.GetChild()) {
            _Joint(tile, _GenerateMap(child));
        }

        return tile;
        
    }

    IRoom _GenerateTile (IRoom.RoomType type, Vector2 pos)
    {
        if (!_roomDict.TryGetValue(type, out Room r)) return null;

        IRoom generated = Instantiate(r, _mapTr);
        generated.Set(pos * _gridSize);

        return generated;

    }

    void _Joint(IRoom tile1, IRoom tile2)
    {
        Instantiate(_joint, _mapTr).Set(tile1, tile2);
    }

}