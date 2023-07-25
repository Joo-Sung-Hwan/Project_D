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

    bool isroom_name = false;
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

        if(isroom_name == true)
        {
            room_name = GameObject.Find("").GetComponent<TMP_InputField>();
            isroom_name = true;
            return;
        }
        // inputfield ��� �����ؾ���!!!
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("������ ���� ����");
    }
    
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("�� �ο��� �ִ��Դϴ�. �ٸ� ���� �������ּ���.");
    }
    public override void OnCreatedRoom()
    {
        Debug.Log("�����");
    }

    public override void OnJoinedRoom()
    {
        //UpdatePlayer();
        Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} ����");
    }

    public void OnClickToChannelSelect()
    {
        for(int i =0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            if (input_id.text.Equals(PhotonNetwork.PlayerList[i]))
            {
                name_ischeck = true;
                Debug.Log("�г��� �ߺ�");
            }
        }
        if(name_ischeck == false)
        {
            PhotonNetwork.LoadLevel("3.Lobby");
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void OnClickToCreateRoom()
    {
        PhotonNetwork.CreateRoom(room_name.text, new RoomOptions { MaxPlayers = 4 }, null);
    }

    public void OnClickToJoinRoom()
    {
        //PhotonNetwork.JoinRoom()
    }
}
