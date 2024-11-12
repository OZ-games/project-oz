using UnityEngine;
using UnityEngine.Events;

public class Projectile : MonoBehaviour
{
    public GameObject projectile;
    public Transform firePoint;
    public float projectileSpeed;
    public float projectileLifetime;

    private Camera cam;
    private Vector3 destination;
    private GameObject projectileObj;

    public void ReadyProjectile()
    {
        InstantiateProjectile();
    }

    public void ShootProjectile()
    {
        if (cam == null)
            cam = Camera.main;

        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        destination = Physics.Raycast(ray, out hit) ? hit.point : ray.GetPoint(1000);
        MoveProjectile();
    }

    private void InstantiateProjectile()
    {
        projectileObj = Instantiate(projectile, firePoint.position, Quaternion.identity, firePoint);
    }

    private void MoveProjectile()
    {
        if (projectileObj != null)
        {
            projectileObj.GetComponent<Rigidbody>().velocity = (destination - firePoint.position).normalized * projectileSpeed;
            Destroy(projectileObj, projectileLifetime);
        }
        projectileObj.transform.parent = null;
        projectileObj = null;
    }
}
