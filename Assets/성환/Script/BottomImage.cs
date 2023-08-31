using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class BottomImage : MonoBehaviour
{
    public string unit_name;
    [SerializeField] private bool is_instantiate;

    public void OnCharacterImage()
    {
        UnitManager um = MapsManager.instance.map_list.transform.GetChild(0).transform.GetChild(1).GetComponent<UnitManager>();
        if(GameManager.instance.playermanager.Gold <= 0)
        {
            return;
        }
        if (um.Unit_Instantiate_Waiting(gameObject.GetComponent<BottomImage>().unit_name))
        {
            Debug.Log("캐릭터 소환");
            GameManager.instance.playermanager.SetGold(1, false);
            gameObject.SetActive(false);
        }
        else
        {
            return;
        }
    }

}
