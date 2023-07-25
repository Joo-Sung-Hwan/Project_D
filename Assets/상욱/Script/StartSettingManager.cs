using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class StartSettingManager : MonoBehaviour
{ 
    // 规积己 UI
    [SerializeField] private Image roomCreate;
 
    [SerializeField] private GameObject prefab;
    [SerializeField] private Transform parent;

    string roomTexttext;
    //bool isCreatecheck = false;

    void Start()
    {
        roomCreate.transform.gameObject.SetActive(false);
        //nickNameCheck.transform.gameObject.SetActive(false);
    }

    // 规积己滚瓢
    public void OnRoomCreate()
    {
       // isCreatecheck = true;
        roomCreate.transform.gameObject.SetActive(true);
    }

    // 规积己UI 啊府绰 滚瓢
    public void OnRoomCreateOff()
    {
        roomCreate.transform.gameObject.SetActive(false);
    }

    public void SetRoomText()
    {
       
    }
    /*
    public void OnClickFinalBtn()
    {
        if(isCreatecheck == true)
        {
            PhotonManager.instance.OnClickCreateRoom();
            Debug.Log("规 积己11111");
            GameObject sc = Instantiate(prefab, parent);
            Debug.Log(sc);
            Debug.Log(PhotonManager.instance.GetRoomName());
            sc.transform.GetChild(4).GetComponent<TMP_Text>().text = PhotonManager.instance.GetRoomName();
        }
        else
        {
            PhotonManager.instance.OnclickToLobby();
        }
    }
    */
}
