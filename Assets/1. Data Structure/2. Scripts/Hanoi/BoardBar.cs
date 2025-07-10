using System;
using System.Collections.Generic;
using UnityEngine;

public class BoardBar : MonoBehaviour
{
    public enum BarType { Left, Center, Right}
    public BarType barType;

    public Stack<GameObject> barStack = new Stack<GameObject>();

    private void OnMouseDown()
    {
        if (!HanoiTower.isSelected)
        {
            HanoiTower.selectedDonut = PopDonut();
            // HanoiTower.selectedDonut.transform.position = transform.position + Vector3.up * 3f;
            // HanoiTower.selectedDonut.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
            // HanoiTower.selectedDonut.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            // HanoiTower.selectedDonut.GetComponent<Rigidbody>().useGravity = false;
        }
        else
        {
            PushDonut(HanoiTower.selectedDonut);
        }
    }

    public bool CheckDonut(GameObject donut)
    {
        if (barStack.Count > 0)
        {
            var pushNumber = donut.GetComponent<Donut>().donutNumber;
            var peekDonut = barStack.Peek();
            var peekNumber = peekDonut.GetComponent<Donut>().donutNumber;

            if (pushNumber < peekNumber)
                return true;
            else
                return false;
        }

        return true;
    }
    public void PushDonut(GameObject donut)
    {
        if (!CheckDonut(donut))
            return;

        HanoiTower.moveCount++;
        HanoiTower.isSelected = false;
        HanoiTower.selectedDonut = null;
        
        donut.transform.position = transform.position + Vector3.up * 5f;
        donut.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        donut.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        donut.GetComponent<Rigidbody>().useGravity = true;

        barStack.Push(donut);
    }

    public GameObject PopDonut()
    {
        if(barStack.Count > 0)
        {
            HanoiTower.currentBar = this;
            HanoiTower.isSelected = true;
            GameObject donut = barStack.Pop();
            return donut;
        }
        return null;
    }
}
