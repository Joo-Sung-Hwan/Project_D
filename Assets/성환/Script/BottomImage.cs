using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomImage : MonoBehaviour
{
    public string unit_name;
    // Start is called before the first frame update

    public void OnCharacterImage()
    {
        Debug.Log("캐릭터 소환");
        MapManager.instance.unitManager.Unit_Instantiate(gameObject.GetComponent<BottomImage>().unit_name);
        gameObject.SetActive(false);
    }

}
