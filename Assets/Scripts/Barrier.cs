using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    //Odpowiada za kolizje przeciwników z barierkami flankującymi planszę z boków
     void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyShip"))
        {
             collision.GetComponentInParent<EnemyWave>().WaveTouchBarrier();
        }
    }

}
