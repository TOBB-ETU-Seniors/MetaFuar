using UnityEngine;

public class RayCastScript : MonoBehaviour
{
    public Camera cam;
    
    void Start()
    {
        if (cam == null)
            cam = Camera.main;
    }

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f;
        mousePos = cam.ScreenToWorldPoint(mousePos);
        Debug.DrawRay(transform.position, mousePos - transform.position, Color.blue);
        
        if (Input.GetMouseButtonDown(0))
        {
            cam.ScreenPointToRay(Input.mousePosition);
        }
    }
}
