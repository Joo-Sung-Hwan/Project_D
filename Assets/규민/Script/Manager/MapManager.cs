using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager instance;
    [SerializeField] public MonsterManager monsterManager;
    [SerializeField] public InGameUIManager uiManager_ingame;
    [SerializeField] public UnitManager unitManager;

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
