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
    public LobbyUIManager lobbyuimanager;
    [SerializeField] private TMP_InputField input_id;
    [SerializeField] private TMP_InputField room_name;
    [SerializeField] private GameObject serverprefab;
    [SerializeField] private Transform parent;
    [SerializeField] private TMP_Text statusText;
    [SerializeField] private TMP_Text nickname;

    [HideInInspector] public string join_room_name;
    [HideInInspector] public Player[] playerList;
    //bool name_ischeck = false;
    List<RoomInfo> myList = new List<RoomInfo>();

    public string name_ui;
    List<GameObject> prefabList = new List<GameObject>();
    List<Player> player_list = new List<Player>();

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
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        statusText.text = PhotonNetwork.NetworkClientState.ToString();
        //SetPlayerList();
        player_list = PhotonNetwork.PlayerList.ToList();
    }
    
    public void Connect()
    {
        PhotonNetwork.LocalPlayer.NickName = input_id.text;
        nickname.text = input_id.text;
        name_ui = PhotonNetwork.LocalPlayer.NickName;
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

        DontDestroyOnLoad(this.gameObject);
        PhotonNetwork.LoadLevel("4.InGameUI");
    }
    public override void OnLeftRoom()
    {
        Debug.Log("나가기");
        Destroy(this.gameObject);
        PhotonNetwork.LoadLevel("2.Lobby");
        LobbyUIManager.instance.nickName.gameObject.SetActive(false);
    }

    
    public override void OnJoinedRoom()
    {

        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            Debug.Log(PhotonNetwork.PlayerList[i].NickName);
        }
        DontDestroyOnLoad(this.gameObject);
        PhotonNetwork.LoadLevel("4.InGameUI");
    }

    public void OnClickToCreateRoom()
    {
        join_room_name = room_name.text;
        PhotonNetwork.CreateRoom(room_name.text, new RoomOptions { MaxPlayers = 4 });
    }

    public void OnClickToJoinRoom()
    {
        PhotonNetwork.JoinRoom(join_room_name, null);
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        int roomCount = roomList.Count;
        for(int i = 0; i < roomCount; i++)
        {   
            
            if (!roomList[i].RemovedFromList)
            {
                if (!myList.Contains(roomList[i]))
                {
                    myList.Add(roomList[i]);
                    GameObject roomlist = Instantiate(serverprefab, parent);
                    roomlist.transform.GetChild(4).GetComponent<TMP_Text>().text = roomList[i].Name;
                    roomlist.transform.GetChild(0).GetComponent<Image>().color = Color.green;
                    prefabList.Add(roomlist);
                    roomlist.transform.GetChild(0).GetComponent<Image>().color = Color.green;
                }
                else
                {
                    myList[myList.IndexOf(roomList[i])] = roomList[i];
                    for(int k = 0; k < prefabList[i].transform.childCount - 1; k++)
                    {
                        prefabList[i].transform.GetChild(k).GetComponent<Image>().color = Color.white;
                    }
                    for (int j = 0; j < roomList[i].PlayerCount; j++)
                    {
                        
                        prefabList[i].transform.GetChild(j).GetComponent<Image>().color = Color.green;
                        if (roomList[i].PlayerCount == roomList[i].MaxPlayers)
                        {
                            prefabList[i].transform.GetChild(j).GetComponent<Image>().color = Color.red;
                        }
                    }
                }
            }
            else if(myList.IndexOf(roomList[i]) != -1)
            {
                for(int k = 0; k < parent.childCount; k++)
                {
                    if(parent.transform.GetChild(k).GetChild(4).GetComponent<TMP_Text>().text == roomList[i].Name)
                    {
                        prefabList.RemoveAt(i);
                        Destroy(parent.transform.GetChild(k).gameObject);
                    }
                }
                myList.RemoveAt(myList.IndexOf(roomList[i]));
            }
        }
    }
    
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
       
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        for (int i = 0; i < player_list.Count; i++)
        {
            if (player_list[i].NickName == otherPlayer.NickName)
            {
                InGameUI.instance. player[i].transform.GetChild(0).GetComponent<TMP_Text>().text = string.Empty;
                InGameUI.instance.ready_Image[i].transform.GetChild(0).GetComponent<TMP_Text>().text = string.Empty;
                InGameUI.instance.ready_Image[i].transform.GetChild(1).gameObject.SetActive(false);
            }
        }

    }
}
