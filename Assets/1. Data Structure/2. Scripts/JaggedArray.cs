using System;
using UnityEngine;

public class JaggedArray : MonoBehaviour
{
    public int[] array1 = new int[3];
    public int[][] jaggedArray = new int[3][];

    private void Start()
    {
        array1[0] = 1;

        jaggedArray[0] = new int[3] { 1, 2, 3 };
        jaggedArray[1] = new int[2] { 4, 5 };
        jaggedArray[2] = new int[4] { 1, 2, 3, 4 };
    }
}
