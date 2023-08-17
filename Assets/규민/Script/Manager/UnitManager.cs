using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class UnitManager : MonoBehaviourPunCallbacks
{
    [SerializeField] List<UnitBlocks> waitingBlocks;
    [SerializeField] UnitBlocks startBlock;
    [SerializeField] PhotonView pv;
    public List<Unit> units;


    // Start is called before the first frame update
    void Start()
    {
        if (pv.IsMine)
            Unit_Instantiate("FireWizzard", startBlock);
    }

    // Update is called once per frame
    void Update()
    {
        if (pv.IsMine && Input.GetKeyDown(KeyCode.Q))
        {
            Unit_Instantiate_Waiting("FireWizzard");
        }
    }

    public void Init_IsWave(bool isWave)
    {
        foreach (var item in units)
        {
            item.Init_Wave(isWave);
        }
    }

    public void Unit_Instantiate_Waiting(string unit_name)
    {
        for (int i = 0; i < waitingBlocks.Count; i++)
        {
            if (waitingBlocks[i].CanPlace)
            {
                UnitBlocks ub = waitingBlocks[i];
                PhotonNetwork.Instantiate(unit_name, ub.transform.position + Vector3.up * 0.25f, ub.transform.rotation).GetComponent<MovableObj>();
                return;
            }
        }
    }

    public GameObject Unit_Instantiate(string unitName, UnitBlocks ub)
    {
        return PhotonNetwork.Instantiate(unitName, ub.transform.position + Vector3.up * 0.25f, ub.transform.rotation);
    }
}
