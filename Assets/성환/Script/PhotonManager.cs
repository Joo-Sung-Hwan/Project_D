using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    public static PhotonManager instance = null;
    [HideInInspector] public TMP_Text userList;
    [HideInInspector] public TMP_InputField input_id;
    string user_ID;
    [HideInInspector] public bool isEnterRoom;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.instance.isConnect == false)
        {
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.ConnectUsingSettings();
            GameManager.instance.isConnect = true;
        }


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
        if(isEnterRoom == true)
        {
            // 방이 있으면 바로 입장, 없으면 생성 후 입장
            PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions { MaxPlayers = 4 }, null);
        }
    }

    public override void OnCreatedRoom()
    {

    }

    public override void OnJoinedRoom()
    {
        //UpdatePlayer();
        Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} 입장");
    }

   
    public void OnclickToLobby()
    {
        isEnterRoom = true;
        OnConnectedToMaster();
        PhotonNetwork.NickName = input_id.text;
        PhotonNetwork.LoadLevel("3.Lobby");
        input_id = null;
        DontDestroyOnLoad(this.gameObject);
    }

    public void OnClickToInGame()
    {
        PhotonNetwork.LoadLevel("4.InGameUI");
    }

    
}
