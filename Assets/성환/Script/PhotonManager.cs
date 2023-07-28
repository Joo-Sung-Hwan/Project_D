using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    public static PhotonManager instance = null;
    [SerializeField] private TMP_InputField input_id;
    [SerializeField] private TMP_InputField room_name;
    [SerializeField] private GameObject serverprefab;
    [SerializeField] private Transform parent;
    [SerializeField] private TMP_Text statusText;
    [SerializeField] private TMP_Text nickname;

    [HideInInspector] public string join_room_name;
    [HideInInspector] public Player[] playerList;
    bool name_ischeck = false;
    PhotonView photonview;

    Dictionary<string, int> room_inf = new Dictionary<string, int>();


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
        photonview = GetComponent<PhotonView>();
        PhotonNetwork.ConnectUsingSettings();
    }

    // Update is called once per frame
    void Update()
    {
        statusText.text = PhotonNetwork.NetworkClientState.ToString();
        //SetPlayerList();
    }
    
    public void Connect()
    {
        PhotonNetwork.LocalPlayer.NickName = input_id.text;
        nickname.text = input_id.text;
        GameManager.instance.lobbyUIManager.lastNicknameSettingUI.gameObject.SetActive(false);
        PhotonNetwork.JoinLobby();
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("마스터 서버 연결");
        
    }
    
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log(PhotonNetwork.NetworkClientState.ToString());
    }

    public override void OnJoinedLobby()
    {
        Debug.Log(PhotonNetwork.NetworkClientState.ToString());
    }

    public override void OnCreatedRoom()
    {
        Debug.Log($"{room_name.text} 생성 완료");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log($"{join_room_name} 입장 완료");

        for(int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            Debug.Log(PhotonNetwork.PlayerList[i].NickName);
        }
        PhotonNetwork.LoadLevel("4.InGameUI");
    }

    public void OnClickToCreateRoom()
    {
        PhotonNetwork.CreateRoom(room_name.text, new RoomOptions { MaxPlayers = 4 });
        join_room_name = room_name.text;
        room_inf.Add(join_room_name, 1);
        PhotonNetwork.LoadLevel("4.InGameUI");
        DontDestroyOnLoad(this.gameObject);
    }

    public void OnClickToJoinRoom()
    {
        PhotonNetwork.JoinRoom(join_room_name, null);
        room_inf[join_room_name] += 1;
        PhotonNetwork.LoadLevel("4.InGameUI");
        DontDestroyOnLoad(this.gameObject);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        for(int i = 0; i < roomList.Count; i++)
        {
            GameObject roomlist = Instantiate(serverprefab, parent);
            roomlist.transform.GetChild(4).GetComponent<TMP_Text>().text = roomList[i].Name;
            for(int j = 0; j < room_inf[roomList[i].Name]; j++)
            {
                roomlist.transform.GetChild(j).GetComponent<Image>().color = Color.green;
            }
        }
        Debug.Log(roomList.Count);
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        
    }

    
    
}
