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
        if(placeUnit == null)
        {
            return;
        }
        placeUnit.Init();
        /*
        if(!placeUnit.ud.isBuy)
        {
            return;
        }
        */
        if (isWating)
        {
            InGameUI.instance.SetSynergy(placeUnit.ud.element_type, false);
        }
        else
        {
            InGameUI.instance.SetSynergy(placeUnit.ud.element_type, true);
        }
    }
}
