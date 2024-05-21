using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IGraph<IRoom.RoomType>;

[CreateAssetMenu(fileName = "Graph", menuName = "Layout/Graph", order = 2)]

public class GraphLayoutData : ScriptableObject, GraphData {

    public IRoom.RoomType[] _type;
    public Edge[] _edges;

    public static GraphLayoutData GetDefault() { 
        GraphLayoutData data = new GraphLayoutData();

        data._type = new IRoom.RoomType[16];

        data._type[0] = IRoom.RoomType.Root;
        data._type[1] = IRoom.RoomType.Normal;
        data._type[2] = IRoom.RoomType.Normal;
        data._type[3] = IRoom.RoomType.Hub;
        data._type[4] = IRoom.RoomType.Normal;
        data._type[5] = IRoom.RoomType.Reward;

        data._type[6] = IRoom.RoomType.Normal;
        data._type[7] = IRoom.RoomType.Boss;
        data._type[8] = IRoom.RoomType.Boss;
        data._type[9] = IRoom.RoomType.Boss;

        data._type[10] = IRoom.RoomType.Hub;
        data._type[11] = IRoom.RoomType.Normal;
        data._type[12] = IRoom.RoomType.Normal;
        data._type[13] = IRoom.RoomType.Reward;

        data._type[14] = IRoom.RoomType.Normal;
        data._type[15] = IRoom.RoomType.Shop;

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

    public IRoom.RoomType[] GetData()
    {
        return _type;
    }

    public Edge[] GetEdge()
    {
        return _edges;
    }
}
