using UnityEngine;
using UnityEngine.SceneManagement; 

public class MenuController : MonoBehaviour
{
    // ฟังก์ชันสำหรับกดปุ่ม Start (เชื่อมกับปุ่ม Start ในหน้าเมนู)
    public void PlayGame()
    {
        // เช็กชื่อ Scene เกมของคุณให้ตรงนะครับ
        SceneManager.LoadScene("GamePlay"); 
    }
}