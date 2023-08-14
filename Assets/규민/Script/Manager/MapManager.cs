using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class MapManager : MonoBehaviour
{
    public static MapManager instance;
    public MonsterManager monsterManager;
    public InGameUIManager uiManager_ingame;
    public UnitManager unitManager;
    public PhotonView pv;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
