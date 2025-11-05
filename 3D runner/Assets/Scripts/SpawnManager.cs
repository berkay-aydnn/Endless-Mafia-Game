using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject coin;
    public GameObject obstacle;
    public Transform player;
    public float spawnDistanceX = 50f; // Player ile arasındaki mesafe
    public float spawnDistanceZ = 2f;  // Spawn aralığı (saniye değil mesafe gibi görünüyor)
    public float roadLength = 6f;
    public float destroyDistance = 20f; // Yok olma mesafesi eğer lvl dizayn olacaksa böyle aksi taktirde arkamıza birikecek adamları da siler
    public float spawnZMaxOffsetFromPlayer = 3f; // Z ekseninde oyuncudan maksimum uzaklık
    public float coinSpacingX = 1.5f; // Coinlerin x ekseninde arka arkaya mesafesi
    public int coinGroupMin = 2;
    public int coinGroupMax = 3;
    // Farklı spawn periyotları (Inspector’dan ayarlanabilir)
    public float obstacleSpawnInterval = 1.5f;
    public float coinSpawnInterval = 0.8f;

    private List<Vector3> obstaclePositions = new List<Vector3>();
    private List<GameObject> spawnObject = new List<GameObject>();

    void Start()
    {
        InvokeRepeating("spawnObstacle", 1f, obstacleSpawnInterval);
        InvokeRepeating("spawnCoinGroup", 1.2f, coinSpawnInterval);
    }
    void Update()
    {
        foreach (GameObject obj in spawnObject)
        {
            if (obj == null) continue;
            if (obj.CompareTag("Coin") && obj.transform.position.x + destroyDistance < player.position.x)
            {
                Destroy(obj);
            }
            if (obj.CompareTag("Obstacle") && obj.transform.position.x + destroyDistance < player.position.x)
            {
                // Silmeden önce bu engelin konumunu listeden de temizle
                Vector3 pos = obj.transform.position;
                obstaclePositions.RemoveAll(p => Vector3.Distance(p, pos) < 0.1f);
                Destroy(obj);
            }
        }
        spawnObject.RemoveAll(item => item == null);
    }
    //Engellerin spawn olmasını sağlayan fonksiyon
    void spawnObstacle()
    {
        Vector3 spawnPos = GetRandomPosition();
        GameObject newObj = Instantiate(obstacle, spawnPos, obstacle.transform.rotation);
        spawnObject.Add(newObj);
        obstaclePositions.Add(spawnPos);
    }
    // Grubun spawn olduğu konumu ve kişi sayısını belirleyen fonksiyon
    void spawnCoinGroup()
    {
        int groupSize = Random.Range(coinGroupMin, coinGroupMax + 1);
        // Coinler oyuncudan bağımsız, yol genişliğinde rastgele Z'de olsun
        float baseX = player.position.x + spawnDistanceX;
        float baseZ = Random.Range(-roadLength, roadLength);
        Vector3 firstPos = new Vector3(baseX, player.position.y, baseZ);

        // Her coin için, sadece OBSTACLE ile çakışmayı kontrol et
        for (int i = 0; i < groupSize; i++)
        {
            Vector3 coinPos = new Vector3(baseX + i * coinSpacingX, firstPos.y, firstPos.z);
            int tries = 0;
            while (IsOverlappingObstacle(coinPos) && tries < 10)
            {
                // Obstacle ile çakışıyorsa, tam yol aralığından yeni bir Z seç
                float newZ = Random.Range(-roadLength, roadLength);
                coinPos = new Vector3(coinPos.x, coinPos.y, newZ);
                tries++;
            }
            GameObject newCoin = Instantiate(coin, coinPos, coin.transform.rotation);
            spawnObject.Add(newCoin);
        }
    }
    // Random konum kaydeden fonksiyon
    Vector3 GetRandomPosition()
    {
        Vector3 pos;
        int tries = 0;
        do
        {
            float x = player.position.x + spawnDistanceX;
            float z = Mathf.Clamp(player.position.z + Random.Range(-spawnZMaxOffsetFromPlayer, spawnZMaxOffsetFromPlayer), -roadLength, roadLength);
            pos = new Vector3(x, player.position.y, z);
            tries++;
            if (tries > 20) break;
        } while (IsOverlappingObstacle(pos));
        return pos;
    }
    // Engel ve kişilerin üst üste çıkmamasını sağlayan fonksiyon
    bool IsOverlappingObstacle(Vector3 pos)
    {
        float minDistance = 1.5f;
        foreach (Vector3 p in obstaclePositions)
        {
            if (Vector3.Distance(p, pos) < minDistance)
                return true;
        }
        return false;
    }
}
