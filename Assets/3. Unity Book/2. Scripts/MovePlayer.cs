using System;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public float moveSpeed = 10f;

    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        var dir = new Vector3(h, v, 0).normalized;

        transform.position += Time.deltaTime * moveSpeed * dir;
    }
}
