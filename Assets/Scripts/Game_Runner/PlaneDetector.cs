using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class PlaneDetector : MonoBehaviour
{
    public int numberOfRays = 12;
    public float rayDistance = 10f;
    public LayerMask layerMask;

    private Transform eyelevelTransform;

    [SerializeField]
    private List<Transform> raySpawnPoints;

    public float spawnTime = 3f;
    private float spawnTimer = 0;

    public List<GameObject> buttonPrefabs;

    private void Awake()
    {
        eyelevelTransform = FindObjectOfType<XROrigin>().transform.Find("Main Camera").transform;
    }

    private void Update()
    {
        spawnTimer += Time.deltaTime;

        transform.position = eyelevelTransform.position;

        List<RaycastHit> hits = new List<RaycastHit>();

        foreach (var point in raySpawnPoints)
            hits.AddRange(ShootRays(point.position));


        if (hits.Count == 0 || spawnTimer < spawnTime) return;

        RaycastHit hit = hits[Random.Range(0, hits.Count)];
        Quaternion spawnRotation = Quaternion.LookRotation(hit.normal);
        Instantiate(buttonPrefabs[Random.Range(0, buttonPrefabs.Count)], hit.point, spawnRotation);
        spawnTimer = 0;
    }

    List<RaycastHit> ShootRays(Vector3 origin)
    {
        List<RaycastHit> hits = new List<RaycastHit>();

        float angleStep = 360f / numberOfRays; // 각 광선 간의 각도
        for (int i = 0; i < numberOfRays; i++)
        {
            // 각도를 회전값으로 변환
            float angle = i * angleStep;
            Quaternion rotation = Quaternion.Euler(0, angle, 0);

            // 방향 벡터 계산
            Vector3 direction = rotation * Vector3.forward;

            // 광선 발사
            Ray ray = new Ray(origin, direction);
            if (Physics.Raycast(ray, out RaycastHit hit, rayDistance, layerMask))
            {
                // Debug.Log($"Ray {i} hit: {hit.collider.name} at distance {hit.distance}");
                hits.Add(hit);
                Debug.DrawLine(origin, hit.point, Color.red);
            }
            else
            {
                Debug.DrawLine(origin, origin + direction * rayDistance, Color.green);
            }
        }

        return hits;
    }
}
