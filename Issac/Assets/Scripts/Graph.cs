
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;

public class Graph<T> {

    public class Entry {

        public T data;
        private List<Entry> child;

        public Entry(T data)
        {
            this.data = data;
            child = new List<Entry>();
        }

        public List<Entry> GetChild() {
            return child;
        }

        public void AddChild(Entry child)
        {
            this.child.Add(child);
        }
    }

    [System.Serializable]
    public class Edge {
        public int ParentIdx;
        public int ChildIdx;

        public Edge(int parent, int child)
        {
            ParentIdx = parent;
            ChildIdx = child;
        }
    }

    public interface GraphData {
        public T[] GetData();
        public Edge[] GetEdge();
    }
    public Entry Root { get; protected set; }

    public Graph() {
        Root = null;
    }

    public Graph(GraphData data)
    {
        T[] entryData = data.GetData();
        Entry[] _entrys = new Entry[entryData.Length];

        for (int i = 0; i < entryData.Length; i++)
        {
            _entrys[i] = new Entry(entryData[i]);
        }
        Root = _entrys[0];

        foreach (Edge edge in data.GetEdge())
        {
            if (edge.ParentIdx > _entrys.Length || edge.ChildIdx > _entrys.Length) continue;

            _entrys[edge.ParentIdx].AddChild(_entrys[edge.ChildIdx]);
        }

    }

}