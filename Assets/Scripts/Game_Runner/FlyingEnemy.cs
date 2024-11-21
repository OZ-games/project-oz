using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 1f;
    [SerializeField]
    private float attackRate = 1f;
    [SerializeField]
    private float attackRange = 1f;
    [SerializeField]
    private Transform target;
    [SerializeField]
    private AudioClip[] spawnSounds;
    [SerializeField]
    private AudioClip[] attackSounds;
    [SerializeField]
    private AudioClip[] deathSounds;
    [SerializeField]
    private GameObject deathEffect;

    private Animator animator;
    private Rigidbody rigid;
    private AudioSource audioSource;

    private bool isDead = false;
    private float curAttackRate = 0f;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        audioSource.PlayOneShot(spawnSounds[Random.Range(0, spawnSounds.Length)]);

        target = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            transform.LookAt(target);

            if (curAttackRate < attackRate)
            {
                curAttackRate += Time.deltaTime;
            }

            if ((Vector3.Distance(target.position, transform.position) < attackRange))
            {
                if (curAttackRate >= attackRate)
                {
                    curAttackRate = 0f;
                    Attack();
                }
            }
            else
            {
                rigid.AddForce((target.position - transform.position).normalized * moveSpeed * Time.deltaTime);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet") && !isDead)
        {
            Die();
        }
    }

    private void Attack()
    {
        animator.SetInteger("animation", 3);
        audioSource.PlayOneShot(attackSounds[Random.Range(0, attackSounds.Length)]);
        GameManager.instance.UpdateHp(-5);
    }

    private void Die()
    {
        EnemyCreator.instance.EnemyRemove(this);
        GameManager.instance.UpdateScore(100);

        isDead = true;
        animator.SetInteger("animation", 5);
        audioSource.PlayOneShot(deathSounds[Random.Range(0, deathSounds.Length)]);

        if (deathEffect)
        {
            deathEffect.SetActive(true);
        }

        StartCoroutine(Resize(transform.localScale, Vector3.zero, 3f));
    }

    private IEnumerator Resize(Vector3 start, Vector3 end, float time)
    {
        float current = 0;
        float percent = 0;

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / time;

            transform.localScale = Vector3.Lerp(start, end, percent);

            yield return null;
        }

        Destroy(gameObject);
        //gameObject.SetActive(false);
    }
}
