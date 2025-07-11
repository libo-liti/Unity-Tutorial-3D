using System.Collections.Generic;
using UnityEngine;

public class BreadthFirstSearch : MonoBehaviour
{
    private int[,] _nodes = new int[8, 8]
    {
        { 0, 1, 1, 1, 0, 0, 0, 0 },
        { 1, 0, 0, 0, 1, 1, 0, 0 },
        { 1, 0, 0, 0, 0, 0, 0, 0 },
        { 1, 0, 0, 0, 0, 0, 1, 0 },
        { 0, 1, 0, 0, 0, 1, 0, 0 },
        { 0, 1, 0, 0, 1, 0, 0, 1 },
        { 0, 0, 0, 1, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 1, 0, 0 }
    };
    
    public Queue<int> queue = new Queue<int>();
    private bool[] _visited = new bool[8];

    private void Start()
    {
        DFSearch(0);
    }

    private void DFSearch(int start)
    {
        queue.Enqueue(start);

        while (queue.Count > 0)
        {
            int index = queue.Dequeue();

            if (!_visited[index])
            {
                _visited[index] = true;

                for (int i = _nodes.GetLength(0) - 1; i >= 0; i++)
                {
                    if (_nodes[index, i] == 1 && !_visited[i])
                        queue.Enqueue(i);
                }
            }
        }
    }
}
