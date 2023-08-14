using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;

public class InGameUI : MonoBehaviourPunCallbacks
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
    public GameObject[] player; 

    [Header("Server_Name_UI")]
    public TMP_Text server_name;
    public int randomInt;

    [Header("Chatting_UI")]
    public TMP_InputField chat_input;
    public GameObject chat_text;
    public Transform chat_parent;

    [Header("Level_UP")]
    [SerializeField] private TMP_Text levelUPText;

    [Header("Ready_UI")]
    [SerializeField] private GameObject ready_ui;
    public Image[] ready_Image;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private Button startBtn;
    [SerializeField] private Button LeftBtn;
    bool isready = false;

    [SerializeField] MapsManager mapsManager;
    // 인게임시작전 준비창 
    
    [SerializeField] private Image not_NextPlay;
    [SerializeField] private Image not_Executive;
    bool[] ischeck_array = new bool[4];
    bool ischeck = true;
    PhotonView photonview;
    //bool gamestart = false;

    
    private void Awake()
    {
        
        if (instance == null)
        {
            instance = this;
        }
        server_name.text = PhotonManager.instance.join_room_name;
        nameText.text = PhotonManager.instance.name_ui;
        
        if (PhotonNetwork.IsMasterClient)
        {
            startBtn.gameObject.SetActive(true);
        }
        ready_Image[GetBtnIndex()].transform.GetChild(1).gameObject.SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {
        
        LeftBtn.onClick.AddListener(OnclickLeftRoom);
        photonview = GetComponent<PhotonView>();
        ready_ui.SetActive(true);
        for (int i = 0; i < ischeck_array.Length; i++)
        {
            ischeck_array[i] = true;
        }
        not_NextPlay.gameObject.SetActive(false);
        //ready_BG.gameObject.SetActive(true);
        not_Executive.gameObject.SetActive(false);
        SetBottomImage();
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
        for(int i = 0; i < player.Length; i++)
        {
            player[i].transform.GetChild(0).GetComponent<TMP_Text>().text = string.Empty;
            ready_Image[i].transform.GetChild(0).GetComponent<TMP_Text>().text = string.Empty;
        }
        for(int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            player[i].transform.GetChild(0).GetComponent<TMP_Text>().text = PhotonNetwork.PlayerList[i].NickName;
            ready_Image[i].transform.GetChild(0).GetComponent<TMP_Text>().text = PhotonNetwork.PlayerList[i].NickName;
        }
    }
    public void OnclickLeftRoom()
    {
        Debug.Log("나가기");
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel("2.Lobby");
    }
    public int GetBtnIndex()
    {
        int btn_index = 0;
        for(int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            if(PhotonManager.instance.name_ui == PhotonNetwork.PlayerList[i].NickName)
            {
                btn_index = i;
            }
        }
        return btn_index;
    }

    
    /*
    // 레벌업
    public void OnLevelUP()
    {
        Debug.Log("레벨업");
        levelUPText.text = levelup;
        levelupNum += 1;
        
    }*/

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

    public void OnClickReady(int a)
    {
        int num = Mathf.Abs(a);
        var hash = PhotonNetwork.PlayerList[num].CustomProperties;
        if ((bool)hash["Ready"] == true)
        {
            ready_Image[num].transform.GetChild(1).GetComponent<Image>().color = Color.yellow;
            PhotonNetwork.PlayerList[num].CustomProperties["Ready"] = false;
        }
        else
        {
            ready_Image[num].transform.GetChild(1).GetComponent<Image>().color = Color.white;
            PhotonNetwork.PlayerList[num].CustomProperties["Ready"] = true;
        }
    }
    

    public void OnNotNextPlayChack()
    {
        not_NextPlay.gameObject.SetActive(false);
    }

    // 방장만 시작할 수 있다는 경고문닫는버튼
    public void OnNotExecutive()
    {
        not_Executive.gameObject.SetActive(false);
    }

    public void OnReady()
    {
        var hash = PhotonNetwork.LocalPlayer.CustomProperties;
        if (isready == false)
        {
            hash["Ready"] = true;
            hash["index"] = GetBtnIndex();
            isready = true;
            Debug.Log("준비완료");
        }
        else
        {
            hash["Ready"] = false;
            hash["index"] = GetBtnIndex() * -1;
            isready = false;
            Debug.Log("준비취소");
        }
        PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
        
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        OnClickReady((int)targetPlayer.CustomProperties["index"]);
        base.OnPlayerPropertiesUpdate(targetPlayer, changedProps);
    }

    public void CheckAllPlayersReady()
    {
        var players = PhotonNetwork.PlayerList;
        
        if (players.All(p => p.CustomProperties.ContainsKey("Ready") && (bool)p.CustomProperties["Ready"] == false))
        {
            Debug.Log("All players are ready!");
            // gamestart = true;
            photonview.RPC("Off_ReadyUI", RpcTarget.All);
            
            //not_Executive.gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("게임시작 X");
            not_NextPlay.gameObject.SetActive(true);
        }
    }

    // 준비창 사라지고 게임 시작
    [PunRPC]
    public void Off_ReadyUI()
    {
        ready_ui.SetActive(false);
        mapsManager.Map_instantiate();
    }

    
    /*
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        if (newMasterClient != PhotonNetwork.LocalPlayer) return;

        CheckAllPlayersReady();
    }
    

    private void CheckAllPlayersReady()
    {
        var players = PhotonNetwork.PlayerList;

        // This is just using a shorthand via Linq instead of having a loop with a counter
        // for checking whether all players in the list have the key "Ready" in their custom properties
        if (players.All(p => p.CustomProperties.ContainsKey("Ready") && (bool)p.CustomProperties["Ready"]))
        {
            Debug.Log("All players are ready!");
        }
    }
    */
}
