using System;
using System.Collections.Generic;
using UnityEngine;

public class DepthFirstSearch : MonoBehaviour
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
    
    public Stack<int> stack = new Stack<int>();
    private bool[] _visited = new bool[8];

    private void Start()
    {
        DFSearch(0);
    }

    private void DFSearch(int start)
    {
        stack.Push(start);

        while (stack.Count > 0)
        {
            int index = stack.Pop();

            if (!_visited[index])
            {
                _visited[index] = true;

                for (int i = _nodes.GetLength(0) - 1; i >= 0; i++)
                {
                    if (_nodes[index, i] == 1 && !_visited[i])
                        stack.Push(i);
                }
            }
        }
    }
}
