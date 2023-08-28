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
                    Debug.Log("�ʵ忡�� ��⼮����");
                    InGameUI.instance.SetSynergy(placeUnit, false);
                    return;
                }
                else
                {
                    Debug.Log("�ʵ忡�� �ʵ��");
                    return;
                }
            }
            else
            {
                if (isWating)
                {
                    Debug.Log("��⼮���� ��⼮����");
                    return;
                }
                else
                {
                    Debug.Log("��⼮���� �ʵ��");
                    InGameUI.instance.SetSynergy(placeUnit, true);
                }
            }
        }
    }
}
