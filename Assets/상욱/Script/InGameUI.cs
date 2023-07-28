using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class InGameUI : MonoBehaviour
{
    public static InGameUI instance = null;
    [Header("BottomBG")]
    [SerializeField] private Image[] prefabCharacter;
    [SerializeField] private Transform[] parent;

    [Header("대기석")]
    // 게임오브젝트를 담는 곳(부모)
    [SerializeField] private Transform callCharacter1;
    [SerializeField] private Transform callCharacter2;
    [SerializeField] private Transform callCharacter3;
    // 게임오브젝트생성(자식)
    [SerializeField] private GameObject create1;

    [Header("Score_UI")]
    [SerializeField] private Image firendList;
    [SerializeField] private GameObject[] player; 

    [Header("Server_Name_UI")]
    public TMP_Text server_name;
    public int randomInt;

    [Header("Chatting_UI")]
    public TMP_InputField chat_input;
    public GameObject chat_text;
    public Transform chat_parent;

    bool ischeck = true;
    PhotonView photonview;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        server_name.text = PhotonManager.instance.join_room_name;
    }
    // Start is called before the first frame update
    void Start()
    {
        SetBottomImage();
        photonview = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        SetUserName();
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

    public void SetUserName()
    {
        for(int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            player[i].transform.GetChild(0).GetComponent<TMP_Text>().text = PhotonNetwork.PlayerList[i].NickName;
        }
    }
    // 레벌업
    public void OnLevelUP()
    {
        Debug.Log("레벨업");
    }

    public void SetBottomImage()
    {
        for (int i = 0; i < parent.Length; i++)
        {
            randomInt = Random.Range(0, 9);
            Instantiate(prefabCharacter[randomInt], parent[i]);
        }
    }

    // 새로고침
    public void OnRefresh()
    {
        Debug.Log("새로고침");
        for (int i = 0; i < parent.Length; i++)
        {
            randomInt = Random.Range(0, 9);
            parent[i].transform.GetChild(0).GetComponent<Image>().sprite = prefabCharacter[randomInt].sprite;
        }
    }
    // 캐릭터 소환
    public void OnCharacterImage()
    {
        Debug.Log("캐릭터 소환");
    }

    public void OnFirendList()
    {
        firendList.transform.gameObject.SetActive(ischeck);
        ischeck = !ischeck;
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


}
