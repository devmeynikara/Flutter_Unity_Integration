using Lean.Touch;
using UnityEngine;

[RequireComponent(typeof(LeanDragTranslate))]
public class FloorDragOnly : MonoBehaviour
{
    private Transform target;
    public float floorY = 0f;
    public float setfloat;
    public bool sofa;
    void Start()
    {
        target = transform;
    }

    void Update()
    {
        // Clamp the Y position to floorY
        if (target != null)
        {
            Vector3 pos = target.position;
            if(sofa == true)
            {
                floorY = setfloat;
            }
            else
            {
                floorY = 0f;
            }
           
            target.position = new Vector3(pos.x, floorY, pos.z);
        }
    }
}
