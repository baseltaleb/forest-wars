using UnityEngine;

public class Projectile : MonoBehaviour {
    public GameObject explosion;
    public AudioClip hitSound;
    public AudioClip travelSound;
    public float speed = 15;
    public float damage;
    public float range;
    private float m_lifetime;

    private void Start()
    {
        if (travelSound) {
            AudioSource auSource = gameObject.AddComponent<AudioSource>();
            auSource.clip = travelSound;
            auSource.loop = true;
            auSource.Play();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Entity e = collision.GetComponent<Entity>();
        if (e)
        {
            e.TakeDamage(damage);
        }
        Kill();
    }

    private void Kill()
    {
        ParticleSystem trail = GetComponentInChildren<ParticleSystem>();
        if (trail)
        {
            trail.transform.SetParent(null);
            Destroy(trail.gameObject, 2f);
        }
        if (explosion)
        {
            explosion = Instantiate(explosion, transform.position, transform.rotation);
            if (hitSound)
            {
                AudioManager.instance.PlaySound(hitSound);
            }
            Destroy(explosion, 2);
        }
        Destroy(gameObject);
    }

    private void Update () {
        transform.position += transform.right * speed * Time.deltaTime;
        if (m_lifetime >= range)
            Destroy(gameObject);
        m_lifetime += Time.deltaTime;
    }
}
