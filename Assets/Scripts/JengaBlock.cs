using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JengaBlock : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.relativeVelocity.magnitude > 1)
            {
                GameManager.instance.UpdateScore((int)transform.position.y);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            GameManager.instance.UpdateHp(-10);

            Destroy(gameObject, 3f);
        }
    }
}
