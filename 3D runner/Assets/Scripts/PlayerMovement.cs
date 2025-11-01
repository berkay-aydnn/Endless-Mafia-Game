using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed = 2f;
    public float horizontalSpeed = 3f;
    public float mapLimit = 6f;
    void Start()
    {
        
    }
 void Update()
{
    // Sonsuz ileri hareket
    transform.Translate(Vector3.right * playerSpeed * Time.deltaTime, Space.World);

    // Oyuncu girişi (A/D veya ok tuşları)
    float moveInput = -Input.GetAxisRaw("Horizontal");

    // Yeni Z pozisyonunu hesapla
    float newZ = transform.position.z + moveInput * horizontalSpeed * Time.deltaTime;

        // Harita sınırlarını uygula
        newZ = Mathf.Clamp(newZ, -mapLimit, mapLimit);
    
        // Konumu güncelle
        transform.position = new Vector3(transform.position.x, transform.position.y, newZ);
    if ((moveInput < 0 && transform.position.z <= -mapLimit) ||
    (moveInput > 0 && transform.position.z >= mapLimit))
{
    moveInput = 0;
}
}
}
