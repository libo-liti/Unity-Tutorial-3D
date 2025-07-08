using System;
using System.Collections.Generic;
using UnityEngine;

public class DynamicArray : MonoBehaviour
{
    public List<int> list1 = new List<int>() { 1, 2, 3 };

    private void Start()
    {
        for(int i = 0; i < 10; i++)
            list1.Add(i);
        
        list1.Insert(0, 100);
    }
}
