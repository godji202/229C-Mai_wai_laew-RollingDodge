using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveForce = 500f;  // ปรับเพิ่มเป็น 500 เพื่อให้เหมาะกับแมพใหญ่
    public float maxSpeed = 15f;    // ความเร็วสูงสุด
    public Rigidbody rb;
    public Transform cam;           // อย่าลืมลาก Main Camera มาใส่ในช่องนี้ที่หน้า Inspector

    [Header("Score Settings")]
    public int score = 0;           // ตัวแปรเก็บคะแนน

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        // ถ้าลืมลากกล้องใส่ใน Inspector โค้ดจะพยายามหา Main Camera ให้เอง
        if (cam == null && Camera.main != null)
        {
            cam = Camera.main.transform;
        }
    }

    void FixedUpdate()
    {
        // 1. รับค่าการกดปุ่ม WASD
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // 2. คำนวณทิศทางตามมุมกล้อง
        Vector3 moveDirection = Vector3.zero;
        if (cam != null)
        {
            Vector3 camForward = cam.forward;
            Vector3 camRight = cam.right;
            camForward.y = 0;
            camRight.y = 0;
            moveDirection = (camForward.normalized * v + camRight.normalized * h).normalized;
        }

        // 3. ใส่แรงเดิน
        rb.AddForce(moveDirection * moveForce);

        // 4. จำกัดความเร็วไม่ให้กลิ้งเร็วเกินไปจนคุมไม่ได้
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }

    // --- ระบบเก็บเหรียญ ---
    private void OnTriggerEnter(Collider other)
    {
        // ต้องตั้ง Tag ที่เหรียญว่า "Coin" และติ๊ก Is Trigger ที่ Collider ของเหรียญด้วยนะ
        if (other.CompareTag("Coin"))
        {
            score += 5;
            Debug.Log("เก็บเหรียญได้แล้ว! คะแนนตอนนี้: " + score);
            Destroy(other.gameObject); // ลบเหรียญทิ้ง
        }
    }
}