using System.Collections.Generic;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    public Transform player;             // Karakter
    public GameObject[] roadPrefabs;     // Yol prefab ı dizesi oluşturacaksın
    public float roadLength = 50f;       // Her yolun uzunluğu
    public int startRoadCount = 4;       // Başlangıçta kaç tane yol spawn edilsin
    public float spawnOffset = 300f;      // Karakterin önünde ne kadar mesafede yeni yol oluşturulsun
    public float destroyOffset = 50f;    // Arkadaki yollar ne kadar uzaklaşınca silinsin

    private List<GameObject> activeRoads = new List<GameObject>();
    private float nextSpawnX = 0f;
    private int currentIndex = 0;        // Şu anda hangi prefab kullanılacak onu tutuyoruz

    void Start()
    {
        // Başlangıçta birkaç yol oluştur
        for (int i = 0; i < startRoadCount; i++)
        {
            SpawnRoad();
        }
    }

    void Update()
    {
        // Karakter ön taraftaki son yola yaklaşırsa yeni yol oluştur
        if (player.position.x + spawnOffset > nextSpawnX)
        {
            SpawnRoad();
        }

        // Arkadaki yolları kontrol et
        if (activeRoads.Count > 0)
        {
            GameObject firstRoad = activeRoads[0];
            if (player.position.x - firstRoad.transform.position.x > destroyOffset + roadLength)
            {
                Destroy(firstRoad);
                activeRoads.RemoveAt(0);
            }
        }
    }

    void SpawnRoad()
    {
        // 1-2-3 şeklinde sırayla prefab seç
        GameObject prefab = roadPrefabs[currentIndex];

        // Yeni yol oluştur
        Vector3 spawnPos = new Vector3(nextSpawnX, 0f, 0f);
        GameObject newRoad = Instantiate(prefab, spawnPos, Quaternion.identity);
        activeRoads.Add(newRoad);

        // Sonraki spawn noktası
        nextSpawnX += roadLength;

        // Index'i sırayla döndür (0→1→2→0→1→2...)
        currentIndex++;
        if (currentIndex >= roadPrefabs.Length)
        {
            currentIndex = 0;
        }
    }
}