using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class UnitManager : MonoBehaviour
{
    [SerializeField] List<UnitBlocks> waitingBlocks;
    [SerializeField] UnitBlocks startBlock;
    Dictionary<UnitBlocks, bool> dic_canPlace = new Dictionary<UnitBlocks, bool>();
    public List<Unit> units;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var blocks in waitingBlocks)
            dic_canPlace.Add(blocks, true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Unit_Instantiate("TestUnit");
        }
    }

    public void Init_IsWave(bool isWave)
    {
        foreach (var item in units)
        {
            item.Init_Wave(isWave);
        }
    }

    public void Unit_Instantiate(string name)
    {
        for (int i = 0; i < waitingBlocks.Count; i++)
        {
            if (dic_canPlace[waitingBlocks[i]])
            {
                UnitBlocks ub = waitingBlocks[i];
                PhotonNetwork.Instantiate(name, ub.transform.position + Vector3.up * 0.25f, ub.transform.rotation).GetComponent<MovableObj>().block = ub;
                dic_canPlace[ub] = false;
                return;
            }
        }
    }

    public void Unit_Instantiate_Start(string name)
    {
        PhotonNetwork.Instantiate(name, startBlock.transform.position + Vector3.up * 0.25f, startBlock.transform.rotation).GetComponent<MovableObj>().block = startBlock;
    }
}
