using UnityEngine;

public class StoneSound : MonoBehaviour
{
    [Header("Audio Settings")]
    public AudioClip hitStoneSound; // ลากไฟล์เสียงหินมาใส่ช่องนี้

    private void OnTriggerEnter(Collider other)
    {
        // เช็กว่าสิ่งที่ชนคือ Tag "Stone" หรือเปล่า
        if (other.CompareTag("Stone"))
        {
            if (hitStoneSound != null)
            {
                // เล่นเสียงตรงตำแหน่งที่ตัวละครอยู่
                AudioSource.PlayClipAtPoint(hitStoneSound, transform.position);
                Debug.Log("ได้ยินเสียงหินแล้ว!");
            }
        }
    }
}