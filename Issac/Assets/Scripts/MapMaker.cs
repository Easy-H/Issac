using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapMaker : MonoBehaviour {

    [SerializeField] GraphLayoutData[] _graphData;
    [SerializeField] MapTile _mapTilePrefab;
    [SerializeField] Joint _joint;
    [SerializeField] Player _player;
    [SerializeField] float _gridSize;
    Transform _mapTr;

    private void Start()
    {
        Generate();
    }
    
    public void Generate()
    {
        GameObject mapParent = new GameObject();
        _mapTr = mapParent.transform;

        GraphLayoutData selectedData;

        if (_graphData.Length == 0) selectedData = GraphLayoutData.GetDefault();
        else selectedData = _graphData[Random.Range(0, _graphData.Length)];

        MapData data = new MapData(selectedData);
        MapTile rootTile = _GenerateMap(data.Root);

        Instantiate(_player, rootTile.transform.position, Quaternion.identity);
        
    }

    MapTile _GenerateMap(MapData.Entry entry) {

        MapTile tile = _GenerateTile(entry.data.type, entry.data.pos);

        foreach (MapData.Entry child in entry.GetChild()) {
            _Joint(tile, _GenerateMap(child));
        }

        return tile;
        
    }

    MapTile _GenerateTile (MapTile.TileType type, Vector2 pos)
    {

        MapTile generated = Instantiate(_mapTilePrefab, _mapTr);
        generated.Set(pos * _gridSize, type);

        return generated;

    }

    void _Joint(MapTile tile1, MapTile tile2)
    {
        Joint j = Instantiate(_joint, _mapTr);
        j.Set(tile1, tile2);
    }

}

public class MapData : Graph<MapData.MapDataUnit> {

    [SerializeField] Vector2[] posDir = new Vector2[4] { Vector2.up, Vector2.right, Vector2.down, Vector2.left };

    public class MapDataUnit {
        public MapTile.TileType type;
        public Vector2 pos;

        public MapDataUnit(MapTile.TileType type, Vector2 pos)
        {
            this.type = type;
            this.pos = pos;
        }
    }

    List<Vector2> field;
    bool _errorOccured;

    Entry[] _entrys;

    public MapData(Graph<MapTile.TileType>.GraphData layout) {

        MapTile.TileType[] entryData = layout.GetData();
        _entrys = new Entry[entryData.Length];

        for (int i = 0; i < entryData.Length; i++)
        {
            _entrys[i] = new Entry(new MapDataUnit(entryData[i], Vector2.zero));
        }
        Root = _entrys[0];

        foreach (Graph<MapTile.TileType>.Edge edge in layout.GetEdge())
        {
            if (edge.ParentIdx > _entrys.Length || edge.ChildIdx > _entrys.Length) continue;

            _entrys[edge.ParentIdx].AddChild(_entrys[edge.ChildIdx]);
        }

        _errorOccured = true;

        while (_errorOccured) {
            field = new List<Vector2>();
            _errorOccured = false;

            _Generate(_entrys[0], Vector2.zero);

        }
    }

    void _Generate(Entry target, Vector2 pos)
    {
        if (_errorOccured) return;

        if (!_GetNearbyEmptyPos(pos, out Vector2 generatePos))
        {
            _errorOccured = true;
            return;
        }

        target.data.pos = generatePos;

        for (int i = 0; i < target.GetChild().Count; i++)
        {
            _Generate(target.GetChild()[i], generatePos);

            if (_errorOccured) return;

        }

    }

    bool _GetNearbyEmptyPos(Vector2 pos, out Vector2 retval)
    {
        Util.Choose choose = new Util.Choose(4);
        retval = posDir[choose.Pick()] + pos;

        while (field.Contains(retval))
        {
            if (choose.IsEmpty())
            {
                _errorOccured = true;
                return false;

            }
            retval = posDir[choose.Pick()] + pos;
        }
        field.Add(retval);
        return true;
    }


}