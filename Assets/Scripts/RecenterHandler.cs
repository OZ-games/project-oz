using UnityEngine;
using Unity.XR.CoreUtils;

public class RecenterHandler : MonoBehaviour
{
    [SerializeField]
    private bool recenterOnStart = true;

    private XROrigin xrOrigin;

    private void Awake()
    {
        xrOrigin = FindObjectOfType<XROrigin>();
    }

    private void Start()
    {
        if (recenterOnStart)
            Recenter();
    }

    public void Recenter()
    {
        if (xrOrigin == null) return;
        xrOrigin.MoveCameraToWorldLocation(new Vector3(0, xrOrigin.CameraYOffset, 0));
        xrOrigin.RotateAroundCameraUsingOriginUp(-Camera.main.transform.rotation.eulerAngles.y);
    }
}
