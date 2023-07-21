using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public List<Transform> spawns;
    public Transform spawn;
    public GameObject prefab;
    public List<UnitUnion> unitlist;

    void Start()
    {
        
    }

    void Update()
    {
        TestSpawn();
    }

    public void FindUnit()
    {
        unitlist.Add(prefab.GetComponent<UnitUnion>());
    }

    public void TestSpawn()
    {
        int rand = Random.Range(0, 3);
        if (Input.GetKeyDown(KeyCode.F2))
        {
            Instantiate(prefab, spawns[rand]);
            FindUnit();
        }
    }

    public void Union()
    {

    }
}
