using System;
using UnityEngine;

public class LocalData : MonoBehaviour
{
    private int score;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            score++;
            PlayerPrefs.SetInt("score", score);

            int loadScore = PlayerPrefs.GetInt("scroe");
        }
    }
}
