using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomImage : MonoBehaviour
{
    public string unit_name;
    [SerializeField] private bool is_instantiate;

    public void OnCharacterImage()
    {
        if (MapManager.instance.unitManager.Unit_Instantiate_Waiting(gameObject.GetComponent<BottomImage>().unit_name))
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
