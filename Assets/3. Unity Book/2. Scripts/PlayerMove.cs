using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5;

    private void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 dir = new Vector3(h, v);

        transform.position += Time.deltaTime * speed * dir;
    }
}
