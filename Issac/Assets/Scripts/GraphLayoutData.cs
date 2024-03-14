using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Graph<MapTile.TileType>;

[CreateAssetMenu(fileName = "Graph", menuName = "Layout/Graph", order = 2)]
public class GraphLayoutData : ScriptableObject, GraphData {

    public MapTile.TileType[] _type;
    public Edge[] _edges;

    public static GraphLayoutData GetDefault() { 
        GraphLayoutData data = new GraphLayoutData();

        data._type = new MapTile.TileType[16];

        data._type[0] = MapTile.TileType.Root;
        data._type[1] = MapTile.TileType.Normal;
        data._type[2] = MapTile.TileType.Normal;
        data._type[3] = MapTile.TileType.Hub;
        data._type[4] = MapTile.TileType.Normal;
        data._type[5] = MapTile.TileType.Reward;

        data._type[6] = MapTile.TileType.Normal;
        data._type[7] = MapTile.TileType.Boss;
        data._type[8] = MapTile.TileType.Boss;
        data._type[9] = MapTile.TileType.Boss;

        data._type[10] = MapTile.TileType.Hub;
        data._type[11] = MapTile.TileType.Normal;
        data._type[12] = MapTile.TileType.Normal;
        data._type[13] = MapTile.TileType.Reward;

        data._type[14] = MapTile.TileType.Normal;
        data._type[15] = MapTile.TileType.Shop;

        data._edges = new Edge[15];

        data._edges[0] = new Edge(0, 1);
        data._edges[1] = new Edge(1, 2);
        data._edges[2] = new Edge(2, 3);
        data._edges[3] = new Edge(3, 4);
        data._edges[4] = new Edge(4, 5);
        data._edges[5] = new Edge(3, 6);
        data._edges[6] = new Edge(6, 7);
        data._edges[7] = new Edge(7, 8);
        data._edges[8] = new Edge(8, 9);
        data._edges[9] = new Edge(6, 10);
        data._edges[10] = new Edge(10, 11);
        data._edges[11] = new Edge(11, 12);
        data._edges[12] = new Edge(12, 13);
        data._edges[13] = new Edge(10, 14);
        data._edges[14] = new Edge(14, 15);

        return data;
    }

    public MapTile.TileType[] GetData()
    {
        return _type;
    }

    public Edge[] GetEdge()
    {
        return _edges;
    }
}
