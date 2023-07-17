using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;

public class PhotonTest : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField inf;
    [SerializeField] private TMP_Text text_log;
    [SerializeField] private TMP_Text text_player;

    [SerializeField] private TMP_InputField chat_input;
    [SerializeField] private GameObject chat_text;
    [SerializeField] private Transform chat_parent;

    PhotonView photonview;

    void Start()
    {
        // ���� �� �ػ� ����
        Screen.SetResolution(1920, 1080, false);
        photonview = GetComponent<PhotonView>();

    }

    public override void OnConnectedToMaster()
    {
        // �� ���� �ִ� �ο� ����
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 4;

        PhotonNetwork.LocalPlayer.NickName = inf.text;

        // ���� ������ �ٷ� ����, ������ ���� �� ����
        PhotonNetwork.JoinOrCreateRoom("Room1", options, null);
        base.OnConnectedToMaster();
    }

    public override void OnJoinedRoom()
    {
        UpdatePlayer();
        text_log.text += $"{inf.text}���� �濡 �����Ͽ����ϴ�.\n";
        //base.OnJoinedRoom();
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePlayer();
        text_log.text += $"{newPlayer.NickName}���� �����Ͽ����ϴ�.\n";
        //base.OnPlayerEnteredRoom(newPlayer);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdatePlayer();
        text_log.text += $"{otherPlayer.NickName}���� �����Ͽ����ϴ�.\n";
        //base.OnPlayerLeftRoom(otherPlayer);
    }
    public void Connect()
    {
        PhotonNetwork.ConnectUsingSettings();
    }
    void UpdatePlayer()
    {
        text_player.text = string.Empty;
        for(int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            text_player.text += $"{PhotonNetwork.PlayerList[i].NickName}\n";
        }
    }

    public void CreateChat(string msg)
    {
        GameObject ct = Instantiate(chat_text, chat_parent);
        ct.GetComponent<TMP_Text>().text = msg;
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
}
