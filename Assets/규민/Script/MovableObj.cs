using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class MovableObj : MonoBehaviour
{
    RaycastHit rayHit;
    Vector3 dis;
    Vector3 prePos;
    [SerializeField] PhotonView pv;

    [SerializeField] public UnitBlocks block;

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
        if (pv.IsMine)
        {
            pv.RPC("RPCOnMouseDrag" , RpcTarget.All);
        }
    }

    [PunRPC]
    void RPCOnMouseDrag()
    {
        if (!block.isWaiting && MapManager.instance.monsterManager.isWave)
        {
            transform.position = prePos;
            return;
        }

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
        if (pv.IsMine)
        {
            pv.RPC("RPCOnMouseDown" , RpcTarget.All);
        }
    }

    [PunRPC]
    void RPCOnMouseDown()
    {
        prePos = transform.position;
    }

    private void OnMouseUp()
    {
        if (pv.IsMine)
        {
            pv.RPC("RPCOnMouseUp", RpcTarget.All);
        }
    }

    [PunRPC]
    void RPCOnMouseUp()
    {
        if (!block.isWaiting && MapManager.instance.monsterManager.isWave)
            return;

        dis = Vector3.zero;
        int layer_Ground = 1 << LayerMask.NameToLayer("Ground");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out rayHit, Mathf.Infinity, layer_Ground))
        {
            if (!rayHit.collider.GetComponent<UnitBlocks>().isWaiting && MapManager.instance.monsterManager.isWave)
                transform.position = prePos;
            else
            {
                transform.position = rayHit.collider.transform.position + Vector3.up * 0.25f;
                block = rayHit.collider.GetComponent<UnitBlocks>();
            }
        }
        else
        {
            transform.position = prePos;
        }
    }
}
