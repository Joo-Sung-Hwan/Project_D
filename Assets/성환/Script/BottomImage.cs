using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomImage : MonoBehaviour
{
    public string unit_name;
    // Start is called before the first frame update

    public void OnCharacterImage()
    {
        Debug.Log("ĳ���� ��ȯ");
        GameManager.instance.playermanager.SetGold(1, false);
        MapManager.instance.unitManager.Unit_Instantiate_Waiting(gameObject.GetComponent<BottomImage>().unit_name);
        gameObject.SetActive(false);
    }

}
