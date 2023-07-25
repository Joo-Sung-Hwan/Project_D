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
    [HideInInspector] public TMP_InputField room_name;
    [SerializeField] private GameObject serverprefab;
    [SerializeField] private Transform parent;

    [HideInInspector] public string join_room_name;
    bool name_ischeck = false;
    bool isCreate = false;
    PhotonView photonview;


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
        else
        {
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.ConnectUsingSettings();
        }
        photonview = GetComponent<PhotonView>();

        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("마스터 서버 연결");
        if(isCreate == false)
        {
            return;
        }
        else
        {
            room_name = GameObject.Find("Canvas/RoomCreate_BG/InputField (TMP)").GetComponent<TMP_InputField>();
            PhotonNetwork.CreateRoom(room_name.text, new RoomOptions { MaxPlayers = 4 }, null);
        }
    }
    
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("방 인원이 최대입니다. 다른 방을 선택해주세요.");
    }

    public override void OnJoinedLobby()
    {
        Debug.Log($"{PhotonNetwork.NickName} 입장");
    }

    public override void OnCreatedRoom()
    {
        Debug.Log($"{room_name.text} 방생성");
    }

    public override void OnJoinedRoom()
    {
        //UpdatePlayer();
        Debug.Log($"{PhotonNetwork.NickName} 입장");
    }

    public void OnClickToChannelSelect()
    {
        for(int i =0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            if (input_id.text.Equals(PhotonNetwork.PlayerList[i]))
            {
                name_ischeck = true;
                Debug.Log("닉네임 중복");
            }
        }
        if(name_ischeck == false)
        {
            PhotonNetwork.NickName = input_id.text;
            PhotonNetwork.JoinLobby();
            PhotonNetwork.LoadLevel("3.Lobby");
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void OnClickToCreateRoom()
    {
        isCreate = true;
        PhotonNetwork.LoadLevel("4.InGameUI");
        DontDestroyOnLoad(this.gameObject);
    }

    public void OnClickToJoinRoom()
    {
        PhotonNetwork.JoinRoom(join_room_name, null);
        
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        for(int i = 0; i < roomList.Count; i++)
        {
            GameObject roomlist = Instantiate(serverprefab, parent);
            roomlist.GetComponent<TMP_Text>().text = roomList[i].Name;
        }
        Debug.Log(roomList.Count);
    }
}
