using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class HanoiTower : MonoBehaviour
{
    public enum HanoiLevel {Lv1 = 3, Lv2, Lv3}
    public HanoiLevel hanoiLevel;

    public GameObject[] donutPrefabs;
    public BoardBar[] bars;

    public static GameObject selectedDonut;
    public static bool isSelected;
    public static BoardBar currentBar;
    public static int moveCount;
    public TextMeshProUGUI countTextUI;
    IEnumerator Start()
    {
        countTextUI.text = moveCount.ToString();
        for (int i = (int)hanoiLevel - 1; i >= 0 ; i--)
        {
            var donut = Instantiate(donutPrefabs[i]);
            donut.transform.position = new Vector3(-5f, 5f, 0);
            bars[0].PushDonut(donut);
            
            yield return new WaitForSeconds(1f);
        }

        moveCount = 0;
        countTextUI.text = moveCount.ToString();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            currentBar.barStack.Push(selectedDonut);
            
            isSelected = false;
            selectedDonut = null;
        }

        countTextUI.text = moveCount.ToString();
    }
}
