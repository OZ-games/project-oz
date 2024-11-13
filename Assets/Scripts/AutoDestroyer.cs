using UnityEngine;

public class AutoDestroyer : MonoBehaviour
{
    public float lifetime = 1;
    public bool destroyOnAwake;

    private void Start()
    {
        if (destroyOnAwake)
            Destroy(gameObject, lifetime);
    }

    public void Destroy()
    {

    }
}
