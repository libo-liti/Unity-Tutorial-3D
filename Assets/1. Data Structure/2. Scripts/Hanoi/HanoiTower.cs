using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HanoiTower : MonoBehaviour
{
    public enum HanoiLevel {Lv1 = 3, Lv2, Lv3}
    public HanoiLevel hanoiLevel = HanoiLevel.Lv1;

    public GameObject[] donutPrefabs;
    public BoardBar[] bars;

    public static GameObject selectedDonut;
    public static bool isSelected;
    public static BoardBar currentBar;
    public static int moveCount;
    public TextMeshProUGUI countTextUI;

    public Button answerButton;

    private void Awake()
    {
        answerButton.onClick.AddListener(HanoiAnswer);
    }

    public void HanoiAnswer()
    {
        HanoiRoutine((int)hanoiLevel, 0, 1, 2);
    }

    public void HanoiRoutine(int n, int from, int temp, int to)
    {
        if (n == 0)
            return;

        if (n == 1)
            Debug.Log($"{n}번 도넛을 {from}에서 {to}로 이동");
        else
        {
            HanoiRoutine(n - 1, from, to, temp);
            Debug.Log($"{n}번 도넛을 {from}에서 {to}로 이동");
            
            HanoiRoutine(n - 1, temp, from, to);
        }
    }

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
