using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class GroundDetector : MonoBehaviour
{
    [Header("AR Plane Manager")]
    [SerializeField]
    private ARPlaneManager arPlaneManager;
    [SerializeField]
    private Transform head;

    [Header("@Debug")]
    [SerializeField]
    private List<ARPlane> planes;
    [SerializeField]
    private ARPlane groundPlane;
    [SerializeField]
    private ARPlane ceilingPlane;


    [Header("Prefabs")]
    [SerializeField]
    private GameObject axe;

    [Header("Materials")]
    [SerializeField]
    private Material defaultMaterial;
    [SerializeField]
    private Material horizontalUpMaterial;
    [SerializeField]
    private Material horizontalDownMaterial;
    [SerializeField]
    private Material verticalMaterial;

    private void Start()
    {
        arPlaneManager.planesChanged += (args) =>
        {
            planes.Clear();
            planes.AddRange(args.added);
            planes.AddRange(args.updated);

            FindLargestPlane();
        };
    }

    private void OnAddNewPlane(List<ARPlane> addedPlanes)
    {
        addedPlanes = addedPlanes.Where(plane => plane.alignment == PlaneAlignment.Vertical).ToList();
        this.planes.AddRange(addedPlanes);
    }

    private void OnRemoveNewPlane(List<ARPlane> removedPlanes)
    {
        this.planes.RemoveAll(plane => removedPlanes.Contains(plane));
    }

    private void FindLargestPlane()
    {
        foreach (var plane in planes)
            plane.GetComponent<MeshRenderer>().material = defaultMaterial;

        // All planes to HorizontalUp(real) planes.
        List<ARPlane> groundPlanes = planes
                                        .Where(plane => plane.alignment == PlaneAlignment.HorizontalUp && plane.center.y < head.position.y)
                                        .ToList();

        groundPlanes.Sort((a, b) => (b.size.x * b.size.y).CompareTo(a.size.x * a.size.y));

        if (groundPlanes.Count != 0)
        {
            groundPlane = groundPlanes[0];
            groundPlane.GetComponent<MeshRenderer>().material = horizontalUpMaterial;
        }


        List<ARPlane> ceilingPlanes = planes
                                        .Where(plane => plane.alignment == PlaneAlignment.HorizontalUp && plane.center.y > head.position.y)
                                        .ToList();

        ceilingPlanes.Sort((a, b) => (b.size.x * b.size.y).CompareTo(a.size.x * a.size.y));

        if (ceilingPlanes.Count != 0)
        {
            ceilingPlane = ceilingPlanes[0];
            ceilingPlane.GetComponent<MeshRenderer>().material = horizontalDownMaterial;
        }
    }

    private bool hasSpawnedAxe = false;
    private bool init = false;
    private void Update()
    {
        if (!init) return;
        if (ceilingPlane == null || hasSpawnedAxe) return;

        Instantiate(axe, ceilingPlane.center, Quaternion.identity);
        hasSpawnedAxe = true;
    }

    public void Init()
    {
        init = true;
    }
}
