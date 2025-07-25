using UnityEngine;

public class ViewModeSwitcher : MonoBehaviour
{
    public enum ViewMode { AR, View3D, View2D }

    [Header("AR System")]
    public GameObject arSession;

    [Header("Cameras under Camera Offset")]
    public GameObject arCameraObject;
    public GameObject camera3DObject;
    public GameObject camera2DObject;

    private Vector3 cam3DStartPos;
    private Quaternion cam3DStartRot;

    private Vector3 cam2DStartPos;
    private Quaternion cam2DStartRot;

    private Camera camera3D;
    private Camera camera2D;

    private void Start()
    {
        // Get Camera components from the GameObjects
        camera3D = camera3DObject.GetComponent<Camera>();
        camera2D = camera2DObject.GetComponent<Camera>();

        cam3DStartPos = camera3D.transform.position;
        cam3DStartRot = camera3D.transform.rotation;

        cam2DStartPos = camera2D.transform.position;
        cam2DStartRot = camera2D.transform.rotation;

        // Start in AR mode
        SwitchToAR();
    }

    public void SwitchToAR()
    {
        arSession.SetActive(true);

        arCameraObject.SetActive(true);
        camera3DObject.SetActive(false);
        camera2DObject.SetActive(false);
    }

    public void SwitchTo3D()
    {
        arSession.SetActive(false);

        arCameraObject.SetActive(false);
        camera3DObject.SetActive(true);
        camera2DObject.SetActive(false);

        camera3D.transform.position = cam3DStartPos;
        camera3D.transform.rotation = cam3DStartRot;
    }

    public void SwitchTo2D()
    {
        arSession.SetActive(false);

        arCameraObject.SetActive(false);
        camera3DObject.SetActive(false);
        camera2DObject.SetActive(true);

        camera2D.transform.position = cam2DStartPos;
        camera2D.transform.rotation = cam2DStartRot;
    }
}
