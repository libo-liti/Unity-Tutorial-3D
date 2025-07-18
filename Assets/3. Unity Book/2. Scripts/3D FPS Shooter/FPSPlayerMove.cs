using System;
using UnityEngine;

public class FPSPlayerMove : MonoBehaviour
{
    private CharacterController cc;

    private float gravity = -20f;
    private float yVelocity = 0f;
    public float moveSpeed = 7f;
    public float jumpPower = 10f;

    public bool isJumping = false;
    private void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, 0, v);
        dir = dir.normalized;
        dir = Camera.main.transform.TransformDirection(dir);
        
        yVelocity += gravity * Time.deltaTime;
        dir.y = yVelocity;

        cc.Move(Time.deltaTime * moveSpeed * dir);

        if (cc.collisionFlags == CollisionFlags.Below)
        {
            if (isJumping)
                isJumping = false;

            yVelocity = 0f;

        }
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            isJumping = true;
            yVelocity = jumpPower;
        }
    }
}
