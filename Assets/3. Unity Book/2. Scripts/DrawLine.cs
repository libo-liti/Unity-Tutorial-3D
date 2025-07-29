using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    private LineRenderer line;
    private int lineCount = 0;
    private int lineObjectCount = 1;

    public Color color;
    public float lineWidth = 0.05f;

    public List<GameObject> lineObjs = new List<GameObject>();

    private void Start()
    {
        color = new Color(1, 1, 1, 1);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject lineObject = new GameObject("Line Object");
            lineObjectCount++;

            line = lineObject.AddComponent<LineRenderer>();
            line.useWorldSpace = false;
            line.startWidth = lineWidth;
            line.endWidth = lineWidth;

            line.startColor = color;
            line.endColor = color;

            line.material = new Material(Shader.Find("Universal Render Pipeline/Particles/Unlit"));
            
            lineObjs.Add(line.gameObject);
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 screenPos = Input.mousePosition;
            screenPos.z = 10f;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);

            lineCount++;
            line.positionCount = lineCount;
            line.SetPosition(lineCount - 1, worldPos);
        }

        if (Input.GetMouseButtonUp(0))
        {
            lineCount = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach(var line in lineObjs)
                Destroy(line);
            lineObjs.Clear();
        }
    }
}
