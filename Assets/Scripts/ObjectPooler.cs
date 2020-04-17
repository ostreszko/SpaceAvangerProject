using System.Collections.Generic;
using UnityEngine;

//Object Pooler, stworzenie listy z obiektami, generowanie kolejek tych obiektów
public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }
    public static ObjectPooler Instance;
    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;
    public List<string> enemyList;

    private void Awake()
    {
        Instance = this;
        //Zaciągnięcie na sztywno efektu eksplozji, nie chciał się podgrać w zwykły sposób
        Pool explosionListElement = new Pool()
        {
            prefab = (GameObject)Instantiate(Resources.Load("Explosion")),
            size = 50,
            tag = "Explosion"
        };
        explosionListElement.prefab.SetActive(false);
        pools.Add(explosionListElement);

        //Stworzenie kolejek obiektów
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            poolDictionary.Add(pool.tag, objectPool);
            if (pool.tag.StartsWith("EnemyShip"))
            {
                enemyList.Add(pool.tag);
            }
        }
    }

    //Wyciągnięcie z kolejki jednego obiektu o podanym tagu
    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Brak " + tag);
            return null;
        }
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
}
