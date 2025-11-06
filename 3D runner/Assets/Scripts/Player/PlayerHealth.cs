using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public float stopDelay= 0.5f;
    //Engele çarptığında canı 1 azalsın
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            StartCoroutine(StopGame());
        } 
    }
    //Oyunu durdur
    private System.Collections.IEnumerator StopGame()
    {
        yield return new WaitForSeconds(stopDelay);
        Time.timeScale = 0f;
    }
}