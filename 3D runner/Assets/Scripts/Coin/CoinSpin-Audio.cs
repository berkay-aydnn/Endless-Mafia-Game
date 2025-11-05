using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public float coinSpin = 1f;
    public AudioSource coinSound;

    void Update()
    {
        transform.Rotate(0, 0, coinSpin * Time.deltaTime);
    }
    public void PlayPickupAndDestroy()
    {
        if (coinSound != null)
        {
            coinSound.Play();
            float delay = coinSound.clip != null ? coinSound.clip.length : 0.1f;
            Destroy(gameObject, delay);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
