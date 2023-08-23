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
    [Header("직접 넣어줄 것(자기 자신)")]
    [SerializeField] PhotonView pv;
    [SerializeField] Unit unit;

    [HideInInspector]public UnitBlocks block;
    bool preBlock_IsWating;
    float clickedTime = 0f;


    void Start()
    {
        dis = Vector3.zero;
        InitBlock();
    }

    public void InitBlock()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hitdata;
        int layerNum = LayerMask.NameToLayer("Ground");
        if (Physics.Raycast(ray, out hitdata, 1.5f, 1 << layerNum))
        {
            if(block)
            {
                block.SetUnit();
                preBlock_IsWating = block.isWating;
            }
            block = hitdata.collider.GetComponent<UnitBlocks>();
            block.SetUnit(unit);
        }

        /*
        //시너지 활성/비활성화
        if (!block.isWating && preBlock_IsWating)
            InGameUI.instance.SetSynergy(unit.ud.element_type, true);
        else
            InGameUI.instance.SetSynergy(unit.ud.element_type, false);
        */
    }

    void Update()
    {
        
    }

    #region 유닛 옮기기
    #region 마우스 드래그
    private void OnMouseDrag()
    {
        if (pv.IsMine)
        {
            pv.RPC("RPCOnMouseDrag" , RpcTarget.All);
        }
        
        clickedTime += Time.deltaTime;
    }

    [PunRPC]
    void RPCOnMouseDrag()
    {
        if (!block.isWating && MapManager.instance.monsterManager.isWave)
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
    #endregion

    #region 마우스 다운
    private void OnMouseDown()
    {
        if (pv.IsMine)
            pv.RPC("RPCOnMouseDown" , RpcTarget.All);
        
        clickedTime = 0f;
    }

    [PunRPC]
    void RPCOnMouseDown()
    {
        prePos = transform.position;
    }
    #endregion

    #region 마우스 업 (합치기 포함)
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
        if (!block.isWating && MapManager.instance.monsterManager.isWave)
            return;
        
        dis = Vector3.zero;
        int layer_Ground = 1 << LayerMask.NameToLayer("Ground");
        Ray ray = new Ray(transform.position, Vector3.down);

        if (Physics.Raycast(ray, out rayHit, Mathf.Infinity, layer_Ground))
        {
            UnitBlocks ub = rayHit.collider.GetComponent<UnitBlocks>();
            if (!ub.isWating && MapManager.instance.monsterManager.isWave || ub == block)
            {
                transform.position = prePos;
            }
            else if (!ub.CanPlace)
            {
                if (ub.unit_Placed && ub.unit_Placed.level == unit.level && ub.unit_Placed.ud.element_type == unit.ud.element_type)
                {
                    ub.unit_Placed.LevelUp_Test();
                    unit.DestroyUnit();
                    block.SetUnit();
                }
                else
                    transform.position = prePos;
            }
            else
            {
                transform.position = rayHit.collider.transform.position + Vector3.up * 0.25f;
                InitBlock();
            }
        }
        else
        {
            transform.position = prePos;
        }
    }
    #endregion
    #endregion
}
