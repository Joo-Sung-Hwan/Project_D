using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;

public class PhotonTest : MonoBehaviourPunCallbacks
{
    public TMP_InputField inf;
    
    void Start()
    {
        // ���� �� �ػ� ����
        Screen.SetResolution(1920, 1080, false);
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

    
    public void Connect()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.LoadLevel("Lobby");
    }
}
