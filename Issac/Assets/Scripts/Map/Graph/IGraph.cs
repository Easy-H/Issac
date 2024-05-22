using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGraph<T> {
    public class Entry {

        public T data;
        private IList<Entry> _child;

        public Entry(T data)
        {
            this.data = data;
            _child = new List<Entry>();
        }

        public IList<Entry> GetChild()
        {
            return _child;
        }

        public void AddChild(Entry child)
        {
            _child.Add(child);
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

    public Entry Root { get; }
}
