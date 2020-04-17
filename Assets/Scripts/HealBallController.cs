using UnityEngine;

//Klasa odpowiada za zachowanie powerupa leczącego
public class HealBallController : MonoBehaviour
{
    private Transform projectileTransform;
    private float speed = 4f;
    public float heal = 20;
    public AudioClip clip;
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

    //Po kolizji z graczem uruchamia metodę dodającą zdrowie i aktualizującą GUI
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            objectPooler.SpawnFromPool("Explosion", transform.position, Quaternion.identity).GetComponent<ParticleSystem>().Play();
            col.GetComponent<PlayerController>().Healed += (sender, args) => {
                heal = args.Amount;
            };
            col.GetComponent<PlayerController>().HealPlayer(heal);
            col.GetComponent<AudioSource>().Play();
            gameObject.SetActive(false);
        }
    }
}
