using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    public TMP_InputField input_id;
    string user_ID;

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.instance.isConnect == false)
        {
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.ConnectUsingSettings();
            GameManager.instance.isConnect = true;
        }

        Screen.SetResolution(1920, 1080, true);

        if (input_id == null)
        {
            return;
        }
        input_id = GameObject.Find("Canvas/!_BG_NameSetting(Image)/!_BG/NameSetting_IF").GetComponent<TMP_InputField>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(input_id.text);
    }

    public override void OnConnectedToMaster()
    {
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 4;

        // 방이 있으면 바로 입장, 없으면 생성 후 입장
        PhotonNetwork.JoinOrCreateRoom("Room1", options, null);
    }

    public override void OnCreatedRoom()
    {

    }

    public override void OnJoinedRoom()
    {
        Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} 입장");
    }

    public override void OnJoinedLobby()
    {

    }
    public void OnclickToLobby()
    {
        PhotonNetwork.NickName = input_id.text;
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("3.Lobby");
        }
        PhotonNetwork.JoinLobby();
        input_id = null;
    }
}
