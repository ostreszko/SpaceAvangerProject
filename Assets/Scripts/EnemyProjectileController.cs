using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Klasa odpowiada za zachowanie wrogich pocisków
public class EnemyProjectileController : MonoBehaviour
{
    private Transform projectileTransform;
    private float speed = 7f;
    public float damage = 10; 
    ObjectPooler objectPooler;

    void Start()
    {
        projectileTransform = GetComponent<Transform>();
        objectPooler = ObjectPooler.Instance;
    }

    void FixedUpdate()
    {
        projectileTransform.position += Vector3.down * speed * Time.deltaTime ;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            objectPooler.SpawnFromPool("Explosion", transform.position, Quaternion.identity).GetComponent<ParticleSystem>().Play();
            col.GetComponent<PlayerController>().Damaged += (sender,args) => {
                damage = args.Amount;
            };
            col.GetComponent<PlayerController>().DamagePlayer(damage);
            GameObject.Find("Speaker").GetComponent<AudioSource>().Play();
            gameObject.SetActive(false);
        }
    }
}
