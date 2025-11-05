using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] private int amount = 1;

    private void OnTriggerEnter(Collider other)
    {
        GameObject hit = other.attachedRigidbody ? other.attachedRigidbody.gameObject : other.gameObject;
        if (!(hit.CompareTag("Player") || hit.transform.root.CompareTag("Player"))) return;

        if (CoinScore.instance != null)
        {
            CoinScore.instance.AddScore(amount);
        }

        // Görseli ve çarpışmayı kapat (coin sahnede kalsın). Ses ve Destroy başka script tarafından yönetilecek.
        var renderers = GetComponentsInChildren<Renderer>(true);
        foreach (var r in renderers) r.enabled = false;
        var colliders = GetComponentsInChildren<Collider>(true);
        foreach (var c in colliders) c.enabled = false;

        // Sesi ve zamanlamalı yok etmeyi CoinScript'e bırak
        var coinScript = GetComponent<CoinScript>();
        if (coinScript != null)
        {
            coinScript.PlayPickupAndDestroy();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
