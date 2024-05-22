
using System.Collections.Generic;
using UnityEngine;

public class MapData : Graph<MapData.MapDataUnit> {

    [SerializeField] Vector2[] posDir = new Vector2[4] { Vector2.up, Vector2.right, Vector2.down, Vector2.left };

    public class MapDataUnit {
        public IRoom.RoomType type;
        public Vector2 pos;

        public MapDataUnit(IRoom.RoomType type, Vector2 pos)
        {
            this.type = type;
            this.pos = pos;
        }
    }

    ICollection<Vector2> _field;
    bool _errorOccured;

    public MapData(IGraph<IRoom.RoomType>.GraphData layout)
    {

        IRoom.RoomType[] entryData = layout.GetData();
        IGraph<MapDataUnit>.Entry[] _entrys = new IGraph<MapDataUnit>.Entry[entryData.Length];

        for (int i = 0; i < entryData.Length; i++)
        {
            _entrys[i] = new IGraph<MapDataUnit>.Entry(new MapDataUnit(entryData[i], Vector2.zero));
        }
        Root = _entrys[0];

        foreach (IGraph<IRoom.RoomType>.Edge edge in layout.GetEdge())
        {
            if (edge.ParentIdx > _entrys.Length || edge.ChildIdx > _entrys.Length) continue;

            _entrys[edge.ParentIdx].AddChild(_entrys[edge.ChildIdx]);
        }

        _errorOccured = true;

        while (_errorOccured)
        {
            _field = new List<Vector2>();
            _errorOccured = false;

            _Generate(_entrys[0], Vector2.zero);

        }
    }

    void _Generate(IGraph<MapDataUnit>.Entry target, Vector2 pos)
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
        Utils.Choose choose = new Utils.Choose(4);
        retval = posDir[choose.Pick()] + pos;

        while (_field.Contains(retval))
        {
            if (choose.IsEmpty())
            {
                _errorOccured = true;
                return false;

            }
            retval = posDir[choose.Pick()] + pos;
        }
        _field.Add(retval);

        return true;
    }


}