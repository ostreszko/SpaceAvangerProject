using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//Klasa odpowiada za generowanie oraz zachowanie pojedynczej kolumny przeciwników
public class EnemyWave : MonoBehaviour
{
    [SerializeField]
    public Dictionary<string, GameObject> EnemyType;
    public float EnemyColumns = 2f, EnemyRows = 2f;
    public int TotalEnemyInLine = 6;
    ObjectPooler objectPooler;
    string shipName;
    EnemyShipController shipController;
    List<bool> destroyedWaveShipList = new List<bool>();
    bool WalkRight;
     float WaveSpeed = 0f;

    //Dla każdej kolumny generowana różna prędkość poruszania się oraz kierunek startowy
    void Start()
    {
        WaveSpeed = Random.Range(0.1f, 5f);
        WalkRight = Random.Range(0f,1f) > 0.5f;
    }

    private void Awake()
    {
        objectPooler = ObjectPooler.Instance;
    }


    //Losowe generowanie przeciwników przy aktywacji rzędu
    private void OnEnable()
    {
        float posY = transform.position.y - (EnemyRows);

        for (int n = 0; n < TotalEnemyInLine; n++)
        {
            Vector2 posX = new Vector2(transform.position.x + (EnemyColumns * n), posY);
            Quaternion rot = new Quaternion(0, 0, 180, Quaternion.identity.w);
            
            GameObject GO = objectPooler.SpawnFromPool(objectPooler.enemyList[(int)Random.Range(0, objectPooler.enemyList.Count)], posX, rot);
            shipName = GO.name;
            shipController = GO.GetComponent<EnemyShipController>();
            if (shipName.StartsWith("small"))
            {
                shipController.hp = StaticDifficulty.SmallEnemyHP;
            }
            else if (shipName.StartsWith("medium"))
            {
                shipController.hp = StaticDifficulty.MediumEnemyHP;
            }
            else if (shipName.StartsWith("large"))
            {
                shipController.hp = StaticDifficulty.LargeEnemyHP;
            }
            GO.transform.SetParent(this.transform);
        }
    }

    void Update()
    {
        Vector2 direction = WalkRight ? Vector2.right : Vector2.left;
        transform.position += (Vector3)direction * WaveSpeed * Time.deltaTime;
        CheckAllChildrenDead();
    }

    //Sprawdzenie czy wszystkie statki w rzędzie zostały zniszczone
    void CheckAllChildrenDead()
    {
        destroyedWaveShipList.Clear();
        for (int i = 0; i < gameObject.transform.childCount  ; i++)
        {
            destroyedWaveShipList.Add(gameObject.transform.GetChild(i).gameObject.activeSelf);
        }
        if(destroyedWaveShipList.All(x => x == false))
        {
            gameObject.SetActive(false);
        }
    }
    
    //Po dotknięciu bariery, rząd odwraca kierunek ruchu
    public void WaveTouchBarrier()
    {
        WalkRight = !WalkRight;
    }
}
