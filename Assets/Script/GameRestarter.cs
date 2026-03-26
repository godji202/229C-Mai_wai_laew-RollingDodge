using UnityEngine;
using UnityEngine.SceneManagement; // ต้องมีบรรทัดนี้เพื่อใช้คำสั่งเปลี่ยนฉาก

public class GameRestarter : MonoBehaviour
{
    // ฟังก์ชันสำหรับเริ่มเกมใหม่
    public void RestartGame()
    {
        // สั่งให้โหลดฉาก (Scene) ปัจจุบันใหม่อีกครั้ง
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}