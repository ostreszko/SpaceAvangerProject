using UnityEngine;

//Klasa odpowiadająca za zachowanie pocisków gracza
public class ProjectileController : MonoBehaviour
{
    private Transform projectileTransform;
    private float speed = 15f;
    ObjectPooler objectPooler;

    void Start()
    {
        projectileTransform = GetComponent<Transform>();
        objectPooler = ObjectPooler.Instance;
    }

    void FixedUpdate()
    {
        projectileTransform.position += Vector3.up * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag != "Projectile" && col.gameObject.tag != "ProjectileEnemy")
        {
            if(col.gameObject.tag == "EnemyShip")
            {
                objectPooler.SpawnFromPool("Explosion", transform.position, Quaternion.identity).GetComponent<ParticleSystem>().Play();
                GameObject.Find("Speaker").GetComponent<AudioSource>().Play();
                gameObject.SetActive(false);
            }

        }
    }
}


