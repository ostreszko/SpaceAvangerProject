using UnityEngine;

//Klasa odpowiedzialna za powerup zwiększający szybkość strzału
public class SpeedShootController : MonoBehaviour
{
    private Transform projectileTransform;
    private float speed = 4f;
    ObjectPooler objectPooler;

    void Start()
    {
        projectileTransform = GetComponent<Transform>();
        objectPooler = ObjectPooler.Instance;
    }

    void FixedUpdate()
    {
        projectileTransform.position += Vector3.down * speed * Time.deltaTime;
        
    }

    //Przy kolizji z graczem zwiększa szybkość strzału o 10%
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            objectPooler.SpawnFromPool("Explosion", transform.position, Quaternion.identity).GetComponent<ParticleSystem>().Play();
            col.GetComponent<PlayerController>().fireRate *= 0.9f;
            col.GetComponent<AudioSource>().Play();
            gameObject.SetActive(false);
        }
    }
}
