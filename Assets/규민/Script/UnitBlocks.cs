using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBlocks : MonoBehaviour
{
    public bool CanPlace { get; set; } = true;
    
    public bool isWating;
    [HideInInspector] public Unit unit_Placed;

    public void SetUnit(bool ischeck, Unit placeUnit = null)
    {
        
        unit_Placed = placeUnit;
        CanPlace = placeUnit == null ? true : false;
        if(placeUnit == null)
        {
            return;
        }
        if(placeUnit.isBuy == false)
        {
            placeUnit.isBuy = true;
            return;
        }
        else
        {
            if (ischeck)
            {
                if (isWating)
                {
                    Debug.Log("필드에서 대기석으로");
                    InGameUI.instance.SetSynergy(placeUnit, false);
                    return;
                }
                else
                {
                    Debug.Log("필드에서 필드로");
                    return;
                }
            }
            else
            {
                if (isWating)
                {
                    Debug.Log("대기석에서 대기석으로");
                    return;
                }
                else
                {
                    Debug.Log("대기석에서 필드로");
                    InGameUI.instance.SetSynergy(placeUnit, true);
                }
            }
        }
    }
}
