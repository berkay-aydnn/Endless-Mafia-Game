using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public float coinSpin = 1f;
    public AudioSource coinSound;

    void Update()
    {
        transform.Rotate(0, 0, coinSpin * Time.deltaTime);
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
            coinSound.Play();
            Destroy(gameObject, coinSound.clip.length);
        }
    }
}
