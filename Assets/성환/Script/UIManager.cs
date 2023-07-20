using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text userList;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlayer();
    }

    void UpdatePlayer()
    {
        userList.text = string.Empty;
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            userList.text += $"{PhotonNetwork.PlayerList[i].NickName}\n";
        }
    }
}
