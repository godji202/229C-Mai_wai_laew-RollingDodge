using UnityEngine;

public class StoneSpawner : MonoBehaviour
{
    public GameObject stonePrefab;    // ลาก Prefab หินมาใส่ที่นี่
    public float spawnRate = 2f;      // ทุกๆ กี่วินาทีที่หินจะตก (ปรับให้เร็วขึ้นเพื่อความยาก)
    public Vector2 spawnRange = new Vector2(40f, 40f); // ระยะความกว้างของแมพ (X, Z)
    public float spawnHeight = 50f;   // ความสูงที่หินจะเริ่มตกลงมา

    void Start()
    {
        // เริ่มสั่งให้เสกหินวนซ้ำไปเรื่อยๆ
        InvokeRepeating("SpawnStone", 1f, spawnRate);
    }

    void SpawnStone()
    {
        // สุ่มตำแหน่ง X และ Z ภายในระยะที่กำหนด
        float randomX = Random.Range(-spawnRange.x, spawnRange.x);
        float randomZ = Random.Range(-spawnRange.y, spawnRange.y);

        Vector3 spawnPos = new Vector3(randomX, spawnHeight, randomZ);

        // สร้างหินขึ้นมา
        GameObject newStone = Instantiate(stonePrefab, spawnPos, Quaternion.identity);

        // (แถม) ทำให้อายุหินสั้นลง จะได้ไม่รกแมพ
        Destroy(newStone, 5f); 
    }
    public void StopSpawning()
    {
        CancelInvoke("SpawnStone");
        Debug.Log("หยุดเสกหินแล้ว!");
    }
}