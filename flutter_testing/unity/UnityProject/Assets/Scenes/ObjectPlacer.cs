using UnityEngine;
using UnityEngine.EventSystems;
using Lean.Touch;
using Lean.Common;

public class ObjectPlacer : MonoBehaviour
{
    public Camera ArMainCamera; // Assign your main camera
    public Camera ThreeDMaincamera;
    public Camera TwoDMaincamera;
    public ObjectCatalogManager catalogManager;
    public LayerMask floorLayer; // Set this to "Floor" layer in inspector

    private Camera mainCamera;

    public void Arcamswitch()
    {
        mainCamera = ArMainCamera;
    }

    public void ThreedainCamera()
    {
        mainCamera = ThreeDMaincamera;
    }


    public void TwoDcamswitch()
    {
        mainCamera = TwoDMaincamera;
    }

    private void Start()
    {
        Arcamswitch();
    }
    void Update()
    {
        // MOBILE
#if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IOS)
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Touch touch = Input.GetTouch(0);
            if (!EventSystem.current.IsPointerOverGameObject(touch.fingerId))
            {
                Ray ray = mainCamera.ScreenPointToRay(touch.position);
                TryPlace(ray);
            }
        }
#else
        // MOUSE (Editor/Desktop)
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            TryPlace(ray);
        }
#endif
    }
    void TryPlace(Ray ray)
    {
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, floorLayer))
        {
            GameObject prefab = catalogManager.GetSelectedPrefab();
            Quaternion rotation = catalogManager.GetSelectedRotation();

            if (prefab != null)
            {
                GameObject obj = Instantiate(prefab, hit.point, rotation);
                Debug.Log("Spawned prefab at: " + hit.point + " with rotation: " + rotation.eulerAngles);

                // Add collider if not present
                if (!obj.GetComponent<Collider>())
                {
                    obj.AddComponent<BoxCollider>();
                }

                // Add LeanSelectableByFinger (enables selection)
                obj.AddComponent<LeanSelectableByFinger>();


                // Add LeanTwistRotateAxis (rotate on selection)
                var rot = obj.AddComponent<LeanTwistRotateAxis>();
                

                // Add object selection handler to enable/disable drag/rotation
                obj.AddComponent<LeanObjectSelectionHandler>();

                // Clear current prefab selection from UI
                catalogManager.ClearSelectedPrefab();
            }
        }
    }



}
