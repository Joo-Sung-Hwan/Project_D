using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SynergyIcon : MonoBehaviour
{
    

    private void OnEnable()
    {
        MapManager.instance.unitManager.ActiveSynergy(transform.parent.GetComponent<Synergy>().s_type, true);
    }

    private void OnDisable()
    {
        MapManager.instance.unitManager.ActiveSynergy(transform.parent.GetComponent<Synergy>().s_type, false);
    }
}
