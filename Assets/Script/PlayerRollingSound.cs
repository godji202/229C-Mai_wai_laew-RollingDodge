using UnityEngine;

public class PlayerRollingSound : MonoBehaviour
{
    public AudioClip rollSound;     // ลากไฟล์เสียงกลิ้งมาใส่
    public Rigidbody rb;            // ลาก Rigidbody ของตัว Player มาใส่
    public float minSpeed = 0.1f;    // ความเร็วขั้นต่ำที่เสียงจะเริ่มดัง
    
    private AudioSource audioSource;

    void Start()
    {
        // สร้าง AudioSource ขึ้นมาในตัวเพื่อคุมเสียงวนซ้ำ
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = rollSound;
        audioSource.loop = true;      // สั่งให้เล่นวนซ้ำ
        audioSource.playOnAwake = false;
        audioSource.volume = 0f;      // เริ่มต้นให้เงียบก่อน
        audioSource.Play();
    }

    void Update()
    {
        if (rb == null) return;

        // เช็กความเร็วของลูกบอล (Magnitude)
        float speed = rb.velocity.magnitude;

        if (speed > minSpeed)
        {
            // ถ้าวิ่งอยู่ ให้ค่อยๆ เพิ่มเสียงตามความเร็ว (ยิ่งเร็วยิ่งดัง)
            // ปรับค่า 0.5f หรือ 15f ตามความเหมาะสมของเกมคุณ
            audioSource.volume = Mathf.Lerp(audioSource.volume, Mathf.Clamp(speed / 15f, 0f, 0.5f), Time.deltaTime * 5f);
            
            // ปรับความแหลมของเสียงตามความเร็ว (ยิ่งเร็วเสียงยิ่งสูง)
            audioSource.pitch = Mathf.Lerp(audioSource.pitch, 0.8f + (speed / 20f), Time.deltaTime * 5f);
        }
        else
        {
            // ถ้าหยุดนิ่ง ให้ค่อยๆ เบาเสียงลงจนเงียบ
            audioSource.volume = Mathf.Lerp(audioSource.volume, 0f, Time.deltaTime * 5f);
        }
    }
}