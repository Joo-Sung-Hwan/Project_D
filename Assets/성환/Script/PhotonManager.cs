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
    [SerializeField] private TMP_InputField input_id;
    [SerializeField] private TMP_InputField create_room_name;
    [HideInInspector] public bool isEnterRoom;
    [HideInInspector] public bool isCreate = true;
    string join_room_name;

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
    }

    // Update is called once per frame
    void Update()
    {
        
        
        //Debug.Log(input_id.text);
    }

    public override void OnConnectedToMaster()
    {
        if(isEnterRoom == true && isCreate == true)
        {
            PhotonNetwork.CreateRoom(create_room_name.text, new RoomOptions { MaxPlayers = 4 }, null);
        }
        else if(isEnterRoom == true && isCreate == false)
        {
            PhotonNetwork.JoinRoom(GetRoomName(), null);
        }
        else
        {
            return;
        }
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("방 인원이 최대입니다. 다른 방을 선택해주세요.");
    }
    public override void OnCreatedRoom()
    {
        Debug.Log("방생성");
    }

    public override void OnJoinedRoom()
    {
        //UpdatePlayer();
        Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} 입장");
    }

    public void OnClickCreateRoom()
    {
        isEnterRoom = true;
        isCreate = true;
        OnConnectedToMaster();
        PhotonNetwork.NickName = input_id.text;
        PhotonNetwork.LoadLevel("3.Lobby");
        DontDestroyOnLoad(this.gameObject);
    }
   
    public void OnclickToLobby()
    {
        isEnterRoom = true;
        isCreate = false;
        OnConnectedToMaster();
        PhotonNetwork.NickName = input_id.text;
        PhotonNetwork.LoadLevel("3.Lobby");
        DontDestroyOnLoad(this.gameObject);
    }

    public string GetRoomName()
    {
        join_room_name = gameObject.GetComponent<TMP_Text>().text;
        return join_room_name;
    }
}
