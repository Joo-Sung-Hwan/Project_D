using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObj : MonoBehaviour
{
    RaycastHit rayHit;
    Vector3 dis;
    Vector3 prePos;

    bool isWave = true;

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
        int layer_DragBox = 1 << LayerMask.NameToLayer("Movable");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out rayHit, Mathf.Infinity, layer_DragBox))
        {
            float y = transform.position.y;

            //dis = 클릭한 마우스 위치와 클릭된 오브젝트 위치의 차이
            if (dis == Vector3.zero)
                dis = new Vector3(transform.position.x - rayHit.point.x, 0, transform.position.z - rayHit.point.z);
            
            transform.position = new Vector3(rayHit.point.x, y, rayHit.point.z) + dis;
        }
    }

    private void OnMouseDown()
    {
        prePos = transform.position;
    }

    private void OnMouseUp()
    {
        dis = Vector3.zero;
        int layer_Ground = 1 << LayerMask.NameToLayer("Ground");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        transform.position = !Physics.Raycast(ray, out rayHit, Mathf.Infinity, layer_Ground) ? 
            prePos: rayHit.collider.transform.position + Vector3.up * 0.25f;
    }

}
