using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class UnitManager : MonoBehaviourPunCallbacks
{
    [Header("자기 자신")]
    [SerializeField] PhotonView pv;

    [SerializeField] List<UnitBlocks> waitingBlocks;
    [SerializeField] UnitBlocks startBlock;

    [HideInInspector] public List<Unit> units = new();

    // Start is called before the first frame update
    void Start()
    {
        if (pv.IsMine)
            Unit_Instantiate_Start("FireWizzard", startBlock);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool Unit_Instantiate_Waiting(string unit_name)
    {
        for (int i = 0; i < waitingBlocks.Count; i++)
        {
            if (waitingBlocks[i].CanPlace)
            {
                UnitBlocks ub = waitingBlocks[i];
                PhotonNetwork.Instantiate(unit_name, ub.transform.position + Vector3.up * 0.25f, ub.transform.rotation);
                return true;
            }
        }
        return false;
    }

    public GameObject Unit_Instantiate_Start(string unitName, UnitBlocks ub)
    {
        return PhotonNetwork.Instantiate(unitName, ub.transform.position + Vector3.up * 0.25f, ub.transform.rotation);
    }

    public void AddUnits(Unit unit)
    {
        unit.transform.SetParent(transform);
        units.Add(unit);
    }

    public void Init_IsWave(bool isWave)
    {
        foreach (var item in units)
        {
            item.Init_Mp(isWave);
        }
    }

    
}
