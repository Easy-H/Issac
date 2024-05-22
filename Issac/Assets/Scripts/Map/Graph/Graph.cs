using System.Collections.Generic;

public class Graph<T> : IGraph<T>{

    public IGraph<T>.Entry Root { get; protected set; }

    public Graph() {
        Root = null;
    }

    public Graph(IGraph<T>.GraphData data)
    {
        T[] entryData = data.GetData();
        IGraph<T>.Entry[] _entrys = new IGraph<T>.Entry[entryData.Length];

        for (int i = 0; i < entryData.Length; i++)
        {
            _entrys[i] = new IGraph<T>.Entry(entryData[i]);
        }
        Root = _entrys[0];

        foreach (IGraph<T>.Edge edge in data.GetEdge())
        {
            if (edge.ParentIdx > _entrys.Length || edge.ChildIdx > _entrys.Length) continue;

            _entrys[edge.ParentIdx].AddChild(_entrys[edge.ChildIdx]);
        }

    }

}