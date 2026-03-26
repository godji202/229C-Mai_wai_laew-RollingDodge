using UnityEngine;

public class Coin : MonoBehaviour
{
    [Header("Settings")]
    public Vector3 rotationSpeed = new Vector3(0, 100, 0); // หมุนแกน Y เป็นหลัก

    void Update()
    {
        // หมุนวัตถุตามความเร็วที่ตั้งไว้ คูณด้วย Time.deltaTime เพื่อให้หมุนนุ่มนวลทุกเครื่อง
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
