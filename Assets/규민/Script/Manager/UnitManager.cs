using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class UnitManager : MonoBehaviour
{
    //[SerializeField] UnitBlocks ub;

    public List<Unit> units;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Init_IsWave(bool isWave)
    {
        foreach (var item in units)
        {
            item.Init_Wave(isWave);
        }
    }

    void Unit_Instantiate()
    {
        //PhotonNetwork.Instantiate("TestUnit", transform.position, transform.rotation).GetComponent<MovableObj>().block = ub;
    }
}
