using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;

public class ChatManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField chat_input;
    [SerializeField] private GameObject chat_text;
    [SerializeField] private Transform chat_parent;

    PhotonView photonview;

    // Start is called before the first frame update
    void Start()
    {
        photonview = GetComponent<PhotonView>();
    }
    public void CreateChat(string msg)
    {
        GameObject ct = PhotonView.Instantiate(chat_text, chat_parent);
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
