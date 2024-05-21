using System.Collections.Generic;

public class Graph<T> {

    public class Entry {

        public T data;
        private IList<Entry> child;

        public Entry(T data)
        {
            this.data = data;
            child = new List<Entry>();
        }

        public IList<Entry> GetChild() {
            return child;
        }

        public void AddChild(Entry child)
        {
            this.child.Add(child);
        }
    }

    public Entry Root { get; protected set; }

    public Graph() {
        Root = null;
    }

    public Graph(IGraph<T>.GraphData data)
    {
        T[] entryData = data.GetData();
        Entry[] _entrys = new Entry[entryData.Length];

        for (int i = 0; i < entryData.Length; i++)
        {
            _entrys[i] = new Entry(entryData[i]);
        }
        Root = _entrys[0];

        foreach (IGraph<T>.Edge edge in data.GetEdge())
        {
            if (edge.ParentIdx > _entrys.Length || edge.ChildIdx > _entrys.Length) continue;

            _entrys[edge.ParentIdx].AddChild(_entrys[edge.ChildIdx]);
        }

    }

}