using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObj : MonoBehaviour
{
    RaycastHit rayHit;
    Vector3 dis;

    // Start is called before the first frame update
    void Start()
    {
        dis = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDrag()
    {
        int layer_Movable = 1 << LayerMask.NameToLayer("Movable");
        int layer_Ground = 1 << LayerMask.NameToLayer("Ground");

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out rayHit, Mathf.Infinity, layer_Movable))
        {
            float y = transform.position.y;
            if (dis == Vector3.zero)
            {
                dis = new Vector3(transform.position.x - rayHit.point.x, 0, transform.position.z - rayHit.point.z);
            }
            
            transform.position = new Vector3(rayHit.point.x, y, rayHit.point.z) + dis;
        }
        Debug.Log(rayHit.point);
    }

    private void OnMouseUp()
    {
        dis = Vector3.zero;
    }
}
