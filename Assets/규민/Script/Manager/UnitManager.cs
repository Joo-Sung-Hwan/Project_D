using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
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
}
