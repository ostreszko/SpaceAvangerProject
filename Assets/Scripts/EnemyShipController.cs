using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Klasa odpowiadająca za statystyki wrogich statków
public class EnemyShipController : MonoBehaviour
{
    ObjectPooler objectPooler;
    GameObject player;
    
    public float fireRateMin = 3;
    public float fireRateMax = 6;
    public int scoreDrop = 50;
    public int hp = 1;
    public float damage = 10;
    private float nextFire;
    void Start()
    {
        objectPooler = ObjectPooler.Instance;
        CalculateDifficultyStats();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnEnable()
    {
        nextFire = Random.Range(fireRateMin, fireRateMax) + Time.timeSinceLevelLoad;
    }

    void Update()
    {
        //Przeciwnik strzela
        if (Time.timeSinceLevelLoad > nextFire)
        {
            nextFire = Time.timeSinceLevelLoad + nextFire;
            GameObject proj = objectPooler.SpawnFromPool("EnemyProjectile", transform.position + Vector3.down, transform.rotation);
            proj.GetComponent<EnemyProjectileController>().damage = damage;
        }
    }

    //Kolizja z pociskiem
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Projectile"))
        {
            hp--;
            if(hp <= 0)
            {
                gameObject.SetActive(false);
                player.GetComponent<PlayerController>().Scored += (sender, args) => {
                    scoreDrop = args.Amount;
                };
                player.GetComponent<PlayerController>().AddScore(scoreDrop);
                //Losowe wypadanie powerupów ze zniszczonego przeciwnika
                if (Random.Range(1,10) <= 1)
                {
                    GameObject proj = objectPooler.SpawnFromPool("HealBall", transform.position, transform.rotation);
                }else if(Random.Range(1, 15) <= 1)
                {
                    GameObject proj = objectPooler.SpawnFromPool("SpeedShoot", transform.position, transform.rotation);
                }
            }
        }
    }

    //Ustawianie statystyk statków na podstawie danych z XML
    private void CalculateDifficultyStats()
    {
        if (StaticDifficulty.IsXMLValid)
        {
            if (gameObject.name.StartsWith("small"))
            {
                fireRateMin = StaticDifficulty.SmallEnemyFireRateMin;
                fireRateMax = StaticDifficulty.SmallEnemyFireRateMax;
                scoreDrop = StaticDifficulty.SmallEnemyScoreDrop;
                hp = StaticDifficulty.SmallEnemyHP;
                damage = StaticDifficulty.SmallEnemyDamage;
            } else if (gameObject.name.StartsWith("medium"))
            {
                fireRateMin = StaticDifficulty.MediumEnemyFireRateMin;
                fireRateMax = StaticDifficulty.MediumEnemyFireRateMax;
                scoreDrop = StaticDifficulty.MediumEnemyScoreDrop;
                hp = StaticDifficulty.MediumEnemyHP;
                damage = StaticDifficulty.MediumEnemyDamage;
            }
            else if (gameObject.name.StartsWith("large"))
            {
                fireRateMin = StaticDifficulty.LargeEnemyFireRateMin;
                fireRateMax = StaticDifficulty.LargeEnemyFireRateMax;
                scoreDrop = StaticDifficulty.LargeEnemyScoreDrop;
                hp = StaticDifficulty.LargeEnemyHP;
                damage = StaticDifficulty.LargeEnemyDamage;
            }
        }
        fireRateMin = System.Math.Max(fireRateMin - StaticDifficulty.GameDifficulty, 0.5f);
        fireRateMax = System.Math.Max(fireRateMax - StaticDifficulty.GameDifficulty, 1f);
        damage = damage * StaticDifficulty.GameDifficulty;
    }
}
