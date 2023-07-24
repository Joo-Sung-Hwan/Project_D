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
    [SerializeField] private TMP_InputField create_room_id;
    [SerializeField] private TMP_InputField create_name;
    
    [HideInInspector] public int isCreate;
    string join_room_name;
    bool name_ischeck = false;
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
        isCreate = 0;
        if (GameManager.instance.isConnect == false)
        {
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.ConnectUsingSettings();
            GameManager.instance.isConnect = true;
        }
        OnConnectedToMaster();
        photonview = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        
    }
    public void Connect()
    {
        if (isCreate == 1)
        {
            PhotonNetwork.CreateRoom(create_name.text, new RoomOptions { MaxPlayers = 4 }, null);
            
        }
        else if (isCreate == 2)
        {
            PhotonNetwork.JoinRoom(GetRoomName(), null);
        }
        else
        {
            return;
        }
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)

    {
        UpdatedRoomList(roomList);
    }

    public void UpdatedRoomList(List<RoomInfo> roomList)
    {

    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("�� �ο��� �ִ��Դϴ�. �ٸ� ���� �������ּ���.");
    }
    public override void OnCreatedRoom()
    {
        //Debug.Log("�����");
    }

    public override void OnJoinedRoom()
    {
        //UpdatePlayer();
        Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} ����");
    }

    public void OnClickCreateRoom()
    {
        isCreate = 1;
        Connect();
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            // �г��� �ߺ� ���
            if (input_id.text.Equals(PhotonNetwork.PlayerList[i]))
            {
                name_ischeck = true;
                return;
            }
        }
        if (name_ischeck == true)
        {
            // �г��� �ߺ�
            return;
        }
        else
        {
            PhotonNetwork.NickName = create_name.text;
        }
        PhotonNetwork.LoadLevel("3.Lobby");
        DontDestroyOnLoad(this.gameObject);
    }
   
    public void OnclickToLobby()
    {
        isCreate = 2;
        Connect();
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            // �г��� �ߺ� ���
            if (input_id.text.Equals(PhotonNetwork.PlayerList[i]))
            {
                name_ischeck = true;
                return;
            }
        } 
        if(name_ischeck == true)
        {
            // �г��� �ߺ�
            return;
        }
        else
        {
            PhotonNetwork.NickName = input_id.text;
        }
        PhotonNetwork.LoadLevel("3.Lobby");
        DontDestroyOnLoad(this.gameObject);
    }

    public string GetRoomName()
    {
        join_room_name = create_room_id.text;
        return join_room_name;
    }
}
