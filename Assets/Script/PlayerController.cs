using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveForce = 20f;   // แรงที่ใช้กลิ้ง
    public float maxSpeed = 10f;    // จำกัดความเร็ว
    public Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(h, 0, v);

        // ใส่แรงให้ลูกบอล
        rb.AddForce(move * moveForce);

        // จำกัดความเร็ว
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }
}