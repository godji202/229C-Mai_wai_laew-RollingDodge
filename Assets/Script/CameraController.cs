using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player; 
    public float mouseSensitivity = 200f;
    public float distanceFromPlayer = 7f; // ระยะห่างจากลูกบอล
    public float heightOffset = 2f;      // ความสูงของกล้องเหนือลูกบอล

    private float rotationX = 20f;
    private float rotationY = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; 
    }

    void LateUpdate()
    {
        // 1. รับค่าเมาส์
        rotationY += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        rotationX -= Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        rotationX = Mathf.Clamp(rotationX, 5f, 60f); // ล็อคไม่ให้กล้องมุดดิน

        // 2. คำนวณการหมุนและตำแหน่ง
        Quaternion rotation = Quaternion.Euler(rotationX, rotationY, 0);
        
        // ตำแหน่งเป้าหมาย (ตัวลูกบอล + ความสูงนิดหน่อย)
        Vector3 targetPosition = player.position + Vector3.up * heightOffset;
        
        // เลื่อนกล้องถอยหลังจากเป้าหมายตามทิศทางที่หมุน
        transform.position = targetPosition - (rotation * Vector3.forward * distanceFromPlayer);
        
        // ให้กล้องมองไปที่จุดเป้าหมาย
        transform.LookAt(targetPosition);
    }
}