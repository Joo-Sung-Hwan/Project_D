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
    [SerializeField] private Button btn;

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

    public override void OnJoinedRoom()
    {
        UpdatePlayer();
        text_log.text += $"{inf.text}님이 방에 참가하였습니다.\n";
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
    public void Connect()
    {
        PhotonNetwork.ConnectUsingSettings();
    }
    void UpdatePlayer()
    {
        for(int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            text_player.text += $"{PhotonNetwork.PlayerList[i].NickName}\n";
        }
    }
}
