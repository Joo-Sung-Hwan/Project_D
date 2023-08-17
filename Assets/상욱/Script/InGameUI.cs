using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;
using Hashtable = ExitGames.Client.Photon.Hashtable;


public class InGameUI : MonoBehaviourPunCallbacks
{
    public static InGameUI instance = null;
    [Header("BottomBG")]
    [SerializeField] private GameObject[] prefabCharacter;
    [SerializeField] private Transform[] parent;

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

    [Header("Gold_UI")]
    [SerializeField] private TMP_Text gold_ui;
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
        
        // 방장(마스터)이면 게임시작 버튼 활성화
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
        SetUserInformation();
        // 시작할때 스코어보드 적용
        for(int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            player[i].transform.GetChild(1).GetComponent<TMP_Text>().text = ((int)PhotonNetwork.PlayerList[i].CustomProperties["Gold"]).ToString();
            player[i].transform.GetChild(2).GetComponent<TMP_Text>().text = ((int)PhotonNetwork.PlayerList[i].CustomProperties["Monster_Left"]).ToString();
            player[i].transform.GetChild(3).GetComponent<TMP_Text>().text = ((int)PhotonNetwork.PlayerList[i].CustomProperties["Life"]).ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        SetUserName();
        SetGoldUI();
        if (chat_input.text == string.Empty)
        {
            return;
        }

        // 채팅창
        string str = $"{PhotonNetwork.LocalPlayer.NickName} : {chat_input.text}";
        if (Input.GetKeyDown(KeyCode.Return))
        {
            chat_input.ActivateInputField();
            photonview.RPC("RPC_Chat", RpcTarget.All, str);
            chat_input.text = string.Empty;
        }
        
    }

    // 스코어판 player 이름 적용함수
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

    /// <summary>
    /// 포톤서버에 CustomProperies에 저장하는 함수 str = "Gold", "Monster_Left", "Life" 중 하나)
    /// </summary>
    /// <param name="str"></param>
    public void SetUserInformation()
    {
        var hash = PhotonNetwork.LocalPlayer.CustomProperties;
        hash["Gold"] = GameManager.instance.playermanager.Gold;
        hash["Monster_Left"] = GameManager.instance.playermanager.Monster_Left;
        hash["Life"] = GameManager.instance.playermanager.Life;
        
        PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
    }

    // 방 나가기 버튼 함수(미완성)
    public void OnclickLeftRoom()
    {
        Debug.Log("나가기");
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel("2.Lobby");
    }

    // 플레이어별 인덱스 구하는 함수
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

    // 캐릭터 이미지 생성
    public void SetBottomImage()
    {
        for (int i = 0; i < parent.Length; i++)
        {
            randomInt = Random.Range(0, prefabCharacter.Length);
            Instantiate(prefabCharacter[randomInt], parent[i]);
        }
    }

    // 새로고침
    public void OnRefresh()
    {
        Debug.Log("새로고침");
        for (int i = 0; i < parent.Length; i++)
        {
            Destroy(parent[i].transform.GetChild(0).gameObject);
        }
        SetBottomImage();
    }

    // 스코어보드 열었다 닫았다 하는 함수
    public void OnFirendList()
    {
        firendList.transform.gameObject.SetActive(ischeck);
        ischeck = !ischeck;
    }

    // 채팅 프리팹 생성 함수
    public void CreateChat(string msg)
    {
        GameObject ct = Instantiate(chat_text, chat_parent);
        ct.GetComponent<TMP_Text>().text = msg;
    }

    // 마스터로 보낸 후 각 플레이어에게 뿌려주는 함수
    [PunRPC]
    void RPC_Chat(string msg)
    {
        CreateChat(msg);
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

    // 준비 버튼
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

    // 각 플레이어 프로퍼티에 준비 상태 전달
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

    // 플레이어의 프로퍼티가 변경될때마다 실행되는 함수
    // 준비 상태, 골드, 몬스터 수, 라이프 적용
    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        int change_index = 0;
        if (changedProps.ContainsKey("Ready") && ready_ui.activeInHierarchy)
        {
            Debug.Log("레디");
            OnClickReady((int)targetPlayer.CustomProperties["index"]);
        }
        else if(changedProps.ContainsKey("Gold") || changedProps.ContainsKey("Monster_Left") || changedProps.ContainsKey("Life"))
        {
            Debug.Log("으악");
            for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
            {
                if(targetPlayer.NickName == PhotonNetwork.PlayerList[i].NickName)
                {
                    change_index = i;
                }
            }
            player[change_index].transform.GetChild(1).GetComponent<TMP_Text>().text = ((int)targetPlayer.CustomProperties["Gold"]).ToString();
            player[change_index].transform.GetChild(2).GetComponent<TMP_Text>().text = ((int)targetPlayer.CustomProperties["Monster_Left"]).ToString();
            player[change_index].transform.GetChild(3).GetComponent<TMP_Text>().text = ((int)targetPlayer.CustomProperties["Life"]).ToString();
        }
        else
        {
            Debug.Log("안대");
            return;
        }
        
        base.OnPlayerPropertiesUpdate(targetPlayer, changedProps);
    }

    // 플레이어 준비 상태가 완료되면 시작
    public void CheckAllPlayersReady()
    {
        var players = PhotonNetwork.PlayerList;
        /*
        if(players.Length == 4)
        {
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
        else
        {
            Debug.Log("인원이 부족합니다.");
        }
        */
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

    // 화면 하단 골드 UI
    public void SetGoldUI()
    {
        gold_ui.text = GameManager.instance.playermanager.Gold.ToString();

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
