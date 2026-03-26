using UnityEngine;
using UnityEngine.UI; // สำหรับใช้รูปหัวใจ
using TMPro;           // สำหรับใช้ TextMeshPro คะแนน

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveForce = 500f;
    public float maxSpeed = 15f;
    public Rigidbody rb;
    public Transform cam;

    [Header("UI References")]
    public TextMeshProUGUI scoreText; // ลาก ScoreText มาใส่
    public Image[] heartImages;      // ลาก Heart1, 2, 3 มาใส่

    [Header("Game Stats")]
    public int coinsCollected = 0;   // นับจำนวนเหรียญที่เก็บได้
    public int targetCoins = 20;     // เป้าหมายคือ 20 เหรียญ
    public int health = 3;           // เลือด 3 ดวง
    public AudioClip coinSound;      // ไฟล์เสียงเหรียญ

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        // ถ้าไม่ได้ลากกล้องใส่ มันจะหากล้องหลักให้เอง
        if (cam == null && Camera.main != null) cam = Camera.main.transform;
        
        UpdateUI(); // อัปเดตหน้าจอตอนเริ่มเกม
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 moveDirection = Vector3.zero;
        if (cam != null)
        {
            Vector3 camForward = cam.forward;
            Vector3 camRight = cam.right;
            camForward.y = 0;
            camRight.y = 0;
            moveDirection = (camForward.normalized * v + camRight.normalized * h).normalized;
        }

        rb.AddForce(moveDirection * moveForce);

        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // --- 1. เก็บเหรียญ ---
        if (other.CompareTag("Coin"))
        {
            if (coinSound != null)
            {
                AudioSource.PlayClipAtPoint(coinSound, other.transform.position);
            }

            coinsCollected += 1; // เพิ่มทีละ 1 เหรียญ
            Destroy(other.gameObject);
            UpdateUI();

            if (coinsCollected >= targetCoins)
            {
                WinGame();
            }
        }

        // --- 2. โดนหิน (Trigger ทะลุผ่านได้) ---
        if (other.CompareTag("Stone"))
        {
            if (health > 0)
            {
                health -= 1; // ลดเลือดทีละ 1
                UpdateUI();
                Destroy(other.gameObject); // ให้หินหายไปทันทีที่โดนตัวเรา
                Debug.Log("โอ๊ย! โดนหินทับ หัวใจเหลือ: " + health);
            }

            if (health <= 0)
            {
                GameOver();
            }
        }
    }

    void UpdateUI()
    {
        // อัปเดตตัวเลขคะแนน
        if (scoreText != null)
        {
            scoreText.text = "Coins: " + coinsCollected + " / " + targetCoins;
        }

        // อัปเดตหัวใจ (วนลูปเช็คว่าต้องแสดงกี่ดวง)
        for (int i = 0; i < heartImages.Length; i++)
        {
            if (i < health) {
                heartImages[i].enabled = true;  // แสดงหัวใจ
            } else {
                heartImages[i].enabled = false; // ซ่อนหัวใจ
            }
        }
    }

    void WinGame()
    {
        Debug.Log("คุณชนะแล้ว! เก็บเหรียญครบ");
        // ใส่โค้ดแสดงหน้าจอ Win ตรงนี้ได้
        moveForce = 0;             // หยุดการควบคุม
        rb.velocity = Vector3.zero; // หยุดตัวละคร

        // สั่งให้ StoneSpawner หยุดเสกหิน
        StoneSpawner spawner = FindObjectOfType<StoneSpawner>();
        if (spawner != null)
        {
            spawner.StopSpawning();
        }
    }

    void GameOver()
    {
        Debug.Log("Game Over! หัวใจหมดแล้ว");
        moveForce = 0;
        rb.velocity = Vector3.zero;

        // --- เพิ่ม 2 บรรทัดนี้ลงไป ---
        Debug.Log("จะเริ่มใหม่ใน 2 วินาที...");
        Invoke("DoRestart", 2f); // รอ 2 วินาทีค่อยเรียกใช้ฟังก์ชัน DoRestart
    }

    // สร้างฟังก์ชันนี้ไว้ใต้ GameOver
    void DoRestart()
    {
        GameRestarter restarter = GetComponent<GameRestarter>();
        if (restarter != null)
        {
            restarter.RestartGame();
        }
    }
}