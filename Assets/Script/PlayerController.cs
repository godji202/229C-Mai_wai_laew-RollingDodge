using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveForce = 15f;    // แรงที่ใช้กลิ้ง
    public float maxSpeed = 10f;    // จำกัดความเร็ว
    public Rigidbody rb;
    public Transform cam;           // ช่องสำหรับลาก Main Camera มาใส่

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // 1. รับค่าจากคีย์บอร์ด (WASD / ลูกศร)
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // 2. คำนวณทิศทางโดยอิงจากมุมกล้อง
        Vector3 camForward = cam.forward;
        Vector3 camRight = cam.right;

        // ล้างค่าแนวตั้ง (Y) ทิ้ง เพื่อไม่ให้ลูกบอลพุ่งลงดินหรือลอยขึ้นฟ้าตามมุมกล้อง
        camForward.y = 0;
        camRight.y = 0;
        camForward.Normalize();
        camRight.Normalize();

        // ทิศทางที่จะเคลื่อนที่จริง
        Vector3 moveDirection = (camForward * v + camRight * h).normalized;

        // 3. ใส่แรงให้ลูกบอลตามทิศทางที่คำนวณได้
        rb.AddForce(moveDirection * moveForce);

        // 4. จำกัดความเร็วสูงสุด
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }
}