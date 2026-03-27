using UnityEngine;
using UnityEngine.UI; 
using TMPro;           
using UnityEngine.SceneManagement; // สั่งเพิ่มบรรทัดนี้เพื่อใช้จัดการเปลี่ยนฉาก

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveForce = 500f;
    public float maxSpeed = 15f;
    public Rigidbody rb;
    public Transform cam;

    [Header("UI References")]
    public TextMeshProUGUI scoreText; 
    public Image[] heartImages;      

    [Header("Game Stats")]
    public int coinsCollected = 0;   
    public int targetCoins = 20;     
    public int health = 3;           
    public AudioClip coinSound;      

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (cam == null && Camera.main != null) cam = Camera.main.transform;
        UpdateUI(); 
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
        if (other.CompareTag("Coin"))
        {
            if (coinSound != null)
            {
                AudioSource.PlayClipAtPoint(coinSound, other.transform.position);
            }

            coinsCollected += 1; 
            Destroy(other.gameObject);
            UpdateUI();

            if (coinsCollected >= targetCoins)
            {
                WinGame();
            }
        }

        if (other.CompareTag("Stone"))
        {
            if (health > 0)
            {
                health -= 1; 
                UpdateUI();
                Destroy(other.gameObject); 
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
        if (scoreText != null)
        {
            scoreText.text = "Coins: " + coinsCollected + " / " + targetCoins;
        }

        for (int i = 0; i < heartImages.Length; i++)
        {
            if (i < health) {
                heartImages[i].enabled = true;  
            } else {
                heartImages[i].enabled = false; 
            }
        }
    }

    // --- แก้ไขฟังก์ชัน WinGame ตรงนี้ ---
    void WinGame()
    {
        Debug.Log("คุณชนะแล้ว! เก็บเหรียญครบ กำลังไปหน้า Credits...");
        
        moveForce = 0;             
        rb.velocity = Vector3.zero; 

        StoneSpawner spawner = FindObjectOfType<StoneSpawner>();
        if (spawner != null)
        {
            spawner.StopSpawning();
        }

        // เพิ่มบรรทัดนี้: รอ 2 วินาทีแล้วไปหน้า Credits
        Invoke("GoToCredits", 2f); 
    }

    // เพิ่มฟังก์ชันสำหรับวาร์ปไปฉาก Credits
    void GoToCredits()
    {
        // ตรวจสอบให้แน่ใจว่าใน Build Settings มี Scene ชื่อ "Credits" แล้ว
        SceneManager.LoadScene("Credits");
    }

    void GameOver()
    {
        Debug.Log("Game Over! หัวใจหมดแล้ว");
        moveForce = 0;
        rb.velocity = Vector3.zero;

        Debug.Log("จะเริ่มใหม่ใน 2 วินาที...");
        Invoke("DoRestart", 2f); 
    }

    void DoRestart()
    {
        GameRestarter restarter = GetComponent<GameRestarter>();
        if (restarter != null)
        {
            restarter.RestartGame();
        }
    }
}