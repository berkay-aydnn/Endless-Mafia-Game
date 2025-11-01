using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject person;
    public GameObject obstacle;
    public Transform player;
    public float spawnDistanceX = 50f; // Player ile arasındaki mesafe
    public float spawnDistanceZ = 2f;  // Spawn aralığı (saniye değil mesafe gibi görünüyor)
    public float roadLength = 6f;
    public float destroyDistance = 20f; // Yok olma mesafesi eğer lvl dizayn olacaksa böyle aksi taktirde arkamıza birikecek adamları da siler

    private List<Vector3> spawnPositions = new List<Vector3>();
    private List<GameObject> spawnObject = new List<GameObject>();

    void Start()
    {
        InvokeRepeating("spawnObstacle", 1f, spawnDistanceZ);
        InvokeRepeating("spawnPersonGroup", 2f, spawnDistanceZ);
    }
    void Update()
    {
        foreach (GameObject obj in spawnObject)
        {
            if (obj == null) continue;
            if (obj.CompareTag("Person") && obj.transform.position.x + destroyDistance < player.position.x)
            {
                Destroy(obj);
            }
            if (obj.CompareTag("Obstacle") && obj.transform.position.x + destroyDistance < player.position.x)
            {
                Destroy(obj);
            }
        }
        spawnObject.RemoveAll(item => item == null);
    }
    //Engellerin spawn olmasını sağlayan fonksiyon
    void spawnObstacle()
    {
        Vector3 spawnPos = GetRandomPosition();
        GameObject newObj = Instantiate(obstacle, spawnPos, Quaternion.identity);
        spawnObject.Add(newObj);
    }
    // Grubun spawn olduğu konumu ve kişi sayısını belirleyen fonksiyon
    void spawnPersonGroup()
    {
        int groupSize = Random.Range(1, 5);
        Vector3 firstPos = GetRandomPosition();
        GameObject newObj = Instantiate(person, firstPos, Quaternion.identity);
        spawnPositions.Add(firstPos);
        List<GameObject> groupObj = new List<GameObject>();
        spawnObject.Add(newObj);
        for (int i = 1; i < groupSize; i++)
        {
            Vector3 spawnPos;
            int tries = 0;
            do
            {
                int xOffset = Random.Range(-1, 1);
                int zOffset = Random.Range(-1, 1);
                float spawnZ = Mathf.Clamp(firstPos.z + zOffset, -roadLength, roadLength);
                spawnPos = new Vector3(firstPos.x + xOffset, firstPos.y, spawnZ);
                tries++;
                if (tries > 10) break;
            } while (IsOverlapping(spawnPos));

            GameObject personGroup = Instantiate(person, spawnPos, Quaternion.identity);
            spawnPositions.Add(spawnPos);
            groupObj.Add(personGroup);
        }
        spawnObject.AddRange(groupObj);
    }
    // Random konum kaydeden fonksiyon
    Vector3 GetRandomPosition()
    {
        Vector3 pos;
        int tries = 0;
        do
        {
            float x = player.position.x + spawnDistanceX;
            float z = Random.Range(-roadLength, roadLength);
            pos = new Vector3(x, player.position.y, z);
            tries++;
            if (tries > 20) break;
        } while (IsOverlapping(pos));
        spawnPositions.Add(pos);
        return pos;
    }
    // Engel ve kişilerin üst üste çıkmamasını sağlayan fonksiyon
    bool IsOverlapping(Vector3 pos)
    {
        float minDistance = 1.5f;
        foreach (Vector3 p in spawnPositions)
        {
            if (Vector3.Distance(p, pos) < minDistance)
                return true;
        }
        return false;
    }
}
