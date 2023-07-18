using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;

public class PhotonChatting : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_Text text_log;
    [SerializeField] private TMP_Text text_player;

    [SerializeField] private TMP_InputField chat_input;
    [SerializeField] private GameObject chat_text;
    [SerializeField] private Transform chat_parent;

    PhotonView photonview;
    PhotonTest pt = new PhotonTest();
    private void Awake()
    {

        pt = GetComponent<PhotonTest>();
    }
    // Start is called before the first frame update
    void Start()
    {
        photonview = GetComponent<PhotonView>();
    }

    public override void OnConnectedToMaster()
    {
        // 한 서버 최대 인원 설정
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 4;

        PhotonNetwork.LocalPlayer.NickName = pt.inf.text;

        // 방이 있으면 바로 입장, 없으면 생성 후 입장
        PhotonNetwork.JoinOrCreateRoom("Room1", options, null);
        base.OnConnectedToMaster();
    }
    public override void OnJoinedRoom()
    {
        UpdatePlayer();
        text_log.text += $"{PhotonNetwork.LocalPlayer.NickName}님이 방에 참가하였습니다.\n";
        //base.OnJoinedRoom();
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePlayer();
        text_log.text += $"{newPlayer.NickName}님이 입장하였습니다.\n";
        //base.OnPlayerEnteredRoom(newPlayer);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdatePlayer();
        text_log.text += $"{otherPlayer.NickName}님이 퇴장하였습니다.\n";
        //base.OnPlayerLeftRoom(otherPlayer);
    }
    
    void UpdatePlayer()
    {
        text_player.text = string.Empty;
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            text_player.text += $"{PhotonNetwork.PlayerList[i].NickName}\n";
        }
    }

    public void CreateChat(string msg)
    {
        GameObject ct = Instantiate(chat_text, chat_parent);
        ct.GetComponent<TMP_Text>().text = msg;
        Debug.Log(chat_parent.GetComponent<RectTransform>().rect.height);
        if (chat_parent.GetComponent<RectTransform>().rect.height > 0)
        {
            Vector2 temp_pos = new Vector2(0f, chat_parent.GetComponent<RectTransform>().rect.height - 300);
            Debug.Log(temp_pos);
            chat_parent.GetComponent<RectTransform>().anchoredPosition = temp_pos;
        }
    }

    [PunRPC]
    void RPC_Chat(string msg)
    {
        CreateChat(msg);
    }

    private void Update()
    {
        if (chat_input.text == string.Empty)
        {
            return;
        }
        string str = $"{PhotonNetwork.LocalPlayer.NickName} : {chat_input.text}";
        if (Input.GetKeyDown(KeyCode.Return))
        {
            chat_input.ActivateInputField();
            photonview.RPC("RPC_Chat", RpcTarget.All, str);
            chat_input.text = string.Empty;
        }
    }
    private void LateUpdate()
    {
        if (chat_parent.GetComponent<RectTransform>().rect.height > 0)
        {
            Vector2 temp_pos = new Vector2(0f, chat_parent.GetComponent<RectTransform>().rect.height - 300);
            Debug.Log(temp_pos);
            chat_parent.GetComponent<RectTransform>().anchoredPosition = temp_pos;
        }
    }
}
