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
        // 실행 시 해상도 설정
        Screen.SetResolution(1920, 1080, false);
    }

    public override void OnConnectedToMaster()
    {
        // 한 서버 최대 인원 설정
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 4;

        PhotonNetwork.LocalPlayer.NickName = inf.text;

        // 방이 있으면 바로 입장, 없으면 생성 후 입장
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
