using UnityEngine;

[System.Serializable]
public class CatalogItem
{
    public GameObject prefab;
    public Vector3 rotation; // Set desired rotation in Inspector
}

public class ObjectCatalogManager : MonoBehaviour
{
    public CatalogItem[] catalogItems;
    private CatalogItem selectedItem = null;
    public GameObject Optionsbutton;
    public TouchCameraController touchcam;
    public TouchPanZoom2D touchcam2d;
    public void SelectObject(int index)
    {
        if (index >= 0 && index < catalogItems.Length)
        {
            selectedItem = catalogItems[index];
            touchcam.enabled = true;
            touchcam2d.enabled = true;
            Optionsbutton.SetActive(true);
            Debug.Log("Selected: " + selectedItem.prefab.name);
        }
    }

    public void ClearSelectedPrefab()
    {
        selectedItem = null;
    }

    public GameObject GetSelectedPrefab()
    {
        return selectedItem?.prefab;
    }

    public Quaternion GetSelectedRotation()
    {
        return selectedItem != null ? Quaternion.Euler(selectedItem.rotation) : Quaternion.identity;
    }
}
