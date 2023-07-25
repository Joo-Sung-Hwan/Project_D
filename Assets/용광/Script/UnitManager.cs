using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public List<UnitBlocks> spawns;
    public Transform spawn;
    public UnitUnion prefab;
    public List<UnitUnion> unitlist;
    public MovableObj movable;

    void Start()
    {
        
    }

    void Update()
    {
        TestSpawn();
    }

    public void FindUnit()
    {
        unitlist.Add(prefab);
    }

    public void TestSpawn()
    {
        int rand = Random.Range(0, 3);
        if (Input.GetKeyDown(KeyCode.F2))
        {
            Instantiate(prefab, spawns[rand].transform);
            FindUnit();
            movable.block = spawns[rand].gameObject.GetComponent<UnitBlocks>();
        }
    }

}
