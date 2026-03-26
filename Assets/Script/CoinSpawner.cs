using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab;    // ลาก Prefab เหรียญมาใส่
    public int totalCoins = 20;      // อยากได้กี่เหรียญใส่ตรงนี้
    public float maxRadius = 45f;    // รัศมีของลานประลอง
    public float coinHeight = 2.0f;  // ความสูงเหรียญ 

    void Start()
    {
        SpawnRandomCoins();
    }

    void SpawnRandomCoins()
    {
        for (int i = 0; i < totalCoins; i++)
        {
            // 1. สุ่มทิศทางแบบวงกลม (360 องศา)
            Vector2 randomPoint = Random.insideUnitCircle * maxRadius;

            // 2. ตั้งตำแหน่ง (แกน X และ Z มาจากจุดสุ่ม, แกน Y คือความสูง)
            Vector3 spawnPos = new Vector3(randomPoint.x, coinHeight, randomPoint.y);
            
            // ย้ายจุดเสกมาไว้ที่ตำแหน่งของตัว Spawner (เพื่อให้เราเลื่อนจุดเกิดได้ใน Unity)
            spawnPos += transform.position;

            // 3. เสกเหรียญ
            Instantiate(coinPrefab, spawnPos, Quaternion.identity);
        }
        Debug.Log("เสกเหรียญกระจายทั่วลานประลองแล้ว: " + totalCoins + " อัน");
    }
}