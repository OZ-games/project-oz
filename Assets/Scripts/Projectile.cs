using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject impactVFX;
    public float speed;
    public float lifetime;
    public AudioClip chargingSound;
    public AudioClip castingSound;
    public AudioClip impactSound;

    private bool collided;
    private Rigidbody rb;
    private AudioSource audioSource;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        PlaySFX(chargingSound);
    }

    public void MoveProjectile(Vector3 direction)
    {
        PlaySFX(castingSound);

        rb.velocity = direction * speed;
        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter(Collision co)
    {
        if (!co.gameObject.CompareTag("Bullet") && !co.gameObject.CompareTag("Player") && !collided)
        {
            collided = true;

            GameObject impact = Instantiate(impactVFX, co.contacts[0].point, Quaternion.identity);
            impact.GetComponent<AudioSource>().PlayOneShot(impactSound);

            Destroy(impact, 2f);
            Destroy(gameObject);
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
