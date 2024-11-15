using UnityEngine;

public class MagicInputController : MonoBehaviour
{
    public Projectile projectile;
    public Transform firePoint;
    public bool isLeftHand;

    private Camera cam;
    private Vector3 destination;
    private Projectile projectileObj;

    public void ReadyProjectile()
    {
        InstantiateProjectile();
    }

    public void ShootProjectile()
    {
        if (cam == null)
            cam = Camera.main;

        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        //Ray ray = new Ray();
        //ray.origin = transform.position;
        //ray.direction = transform.forward;
        RaycastHit hit;
        int layerMask = 1 << LayerMask.NameToLayer("Object");

        destination = Physics.Raycast(ray, out hit, layerMask) ? hit.point : ray.GetPoint(1000);
        //destination = ray.GetPoint(10);
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
            projectileObj.MoveProjectile((destination - firePoint.position).normalized);
        }
        projectileObj.transform.parent = null;
        projectileObj = null;
    }
}
