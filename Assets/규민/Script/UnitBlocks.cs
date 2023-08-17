using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBlocks : MonoBehaviour
{
    public bool CanPlace { get; set; } = true;
    
    public bool isWating;
    [HideInInspector] public Unit unit_Placed;

    public void SetUnit(Unit placeUnit = null)
    {
        unit_Placed = placeUnit;
        CanPlace = placeUnit == null ? true : false;
    }
}
