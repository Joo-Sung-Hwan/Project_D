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
    public RectTransform scrollview;
    public TMP_InputField chat_input;
    public GameObject chat_text;
    public Transform chat_parent;
    float prev_content;

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
    // �ΰ��ӽ����� �غ�â 

    [Header("Synergy")]
    public Synergy[] Synergy_prefab;
    public Transform Synergy_parent;
    [HideInInspector] public Dictionary<string, int> unit_dic = new Dictionary<string, int>();
    [HideInInspector] public Dictionary<string, Synergy> synergy_list = new Dictionary<string, Synergy>();
    [HideInInspector] public List<Synergy> s_list = new List<Synergy>();



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
        
        // ����(������)�̸� ���ӽ��� ��ư Ȱ��ȭ
        if (PhotonNetwork.IsMasterClient)
        {
            startBtn.gameObject.SetActive(true);
        }
        ready_Image[GetBtnIndex()].transform.GetChild(1).gameObject.SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {
        photonview = GetComponent<PhotonView>();
        LeftBtn.onClick.AddListener(OnclickLeftRoom);
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
        // �����Ҷ� ���ھ�� ����
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
        // ä��â
        string str = $"{PhotonNetwork.LocalPlayer.NickName} : {chat_input.text}";
        if (Input.GetKeyDown(KeyCode.Return))
        {
            chat_input.ActivateInputField();
            photonview.RPC("RPC_Chat", RpcTarget.All, str);
            chat_input.text = string.Empty;
        }
        
    }

    /// <summary>
    /// �ó��� UI ���� true�� �߰�, false�� ����
    /// </summary>
    /// <param name="e_type"></param>
    /// <param name="isAdd"></param>

    
    public void SetSynergy(Unit unit, bool isAdd)
    {
        if (isAdd)
        {
            if (unit_dic.ContainsKey(unit.name))
            {
                Debug.Log("�ߺ�");
                unit_dic[unit.name] += 1;
                return;
            }
            else
            {
                if (!synergy_list.ContainsKey(unit.ud.element_type.ToString()))
                {
                    Synergy sp = Instantiate(Synergy_prefab[(int)unit.ud.element_type], Synergy_parent);
                    sp.count += 1;
                    s_list.Add(sp);
                    synergy_list.Add((sp.s_type).ToString(), sp);
                    unit_dic.Add(unit.name, 1);
                }
                else
                {
                    synergy_list[unit.ud.element_type.ToString()].count += 1;
                    unit_dic.Add(unit.name, 1);
                }
            }

        }
        else
        {
            unit_dic[unit.name] -= 1;
            if (unit_dic[unit.name] <= 0)
            {
                unit_dic.Remove(unit.name);
                synergy_list[unit.ud.element_type.ToString()].count -= 1;
                if (synergy_list[unit.ud.element_type.ToString()].count <= 0)
                {
                    Destroy(synergy_list[unit.ud.element_type.ToString()].gameObject);
                    synergy_list.Remove(unit.ud.element_type.ToString());
                    foreach (var item in s_list)
                    {
                        if(item.s_type == unit.ud.element_type)
                        {
                            s_list.Remove(item);
                        }
                    }
                }
            }
        }
        
       
    }

    // ���ھ��� player �̸� �����Լ�
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
        player[GetBtnIndex()].transform.GetChild(0).GetComponent<TMP_Text>().color = Color.yellow;
    }

    /// <summary>
    /// ���漭���� CustomProperies�� �����ϴ� �Լ� str = "Gold", "Monster_Left", "Life" �� �ϳ�)
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

    // �� ������ ��ư �Լ�(�̿ϼ�)
    public void OnclickLeftRoom()
    {
        Debug.Log("������");
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel("2.Lobby");
    }

    // �÷��̾ �ε��� ���ϴ� �Լ�
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
    // ������
    public void OnLevelUP()
    {
        Debug.Log("������");
        levelUPText.text = levelup;
        levelupNum += 1;
        
    }*/

    // ĳ���� �̹��� ����
    public void SetBottomImage()
    {
        for (int i = 0; i < parent.Length; i++)
        {
            randomInt = Random.Range(0, prefabCharacter.Length);
            Instantiate(prefabCharacter[randomInt], parent[i]);
        }
    }

    // ���ΰ�ħ
    public void OnRefresh()
    {
        Debug.Log("���ΰ�ħ");
        if(GameManager.instance.playermanager.Gold < 2)
        {
            return;
        }
        for (int i = 0; i < parent.Length; i++)
        {
            Destroy(parent[i].transform.GetChild(0).gameObject);
        }
        SetBottomImage();
        GameManager.instance.playermanager.SetGold(2, false);
    }

    // ���ھ�� ������ �ݾҴ� �ϴ� �Լ�
    public void OnFirendList()
    {
        firendList.transform.gameObject.SetActive(ischeck);
        ischeck = !ischeck;
    }

    // ä�� ������ ���� �Լ�
    public void CreateChat(string msg)
    {
        prev_content = chat_parent.GetComponent<RectTransform>().sizeDelta.y;
        GameObject ct = Instantiate(chat_text, chat_parent);
        ct.GetComponent<TMP_Text>().text = msg;
        StartCoroutine("AutoScrollBottom");
    }

    // ä��â �ֽ�ȭ
    IEnumerator AutoScrollBottom()
    {
        yield return null;
        // ��ũ�Ѻ� H���� Content H���� Ŭ ����(��ũ���� ������ ���¶��)
        // ��ũ�ѹٰ� ����� ���� (ä���� ������� �ļ�, Content �� Scroll View �� �Ѿ ����)
        if (chat_parent.GetComponent<RectTransform>().sizeDelta.y > scrollview.sizeDelta.y)
        {
            //  ==    (Content.y >= ����Ǳ� �� content Hight �� - ScrollView �� Hight ��)
            // ��ũ�� �ٰ� �ٴڿ� �ִ°Ŷ��
            if (chat_parent.GetComponent<RectTransform>().anchoredPosition.y >= prev_content - scrollview.sizeDelta.y)
            {
                //5. �߰��� ���̸�ŭ content y���� �����ϰڴ�. (������ �̹� �߰� �� �����ΰ���)
                chat_parent.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, chat_parent.GetComponent<RectTransform>().sizeDelta.y - scrollview.sizeDelta.y);
            }
        }
    }

    // �����ͷ� ���� �� �� �÷��̾�� �ѷ��ִ� �Լ�
    [PunRPC]
    void RPC_Chat(string msg)
    {
        CreateChat(msg);
    }

    public void OnNotNextPlayChack()
    {
        not_NextPlay.gameObject.SetActive(false);
    }

    // ���常 ������ �� �ִٴ� ����ݴ¹�ư
    public void OnNotExecutive()
    {
        not_Executive.gameObject.SetActive(false);
    }

    // �غ� ��ư
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

    // �� �÷��̾� ������Ƽ�� �غ� ���� ����
    public void OnReady()
    {
        var hash = PhotonNetwork.LocalPlayer.CustomProperties;
        if (isready == false)
        {
            hash["Ready"] = true;
            hash["index"] = GetBtnIndex();
            isready = true;
            Debug.Log("�غ�Ϸ�");
        }
        else
        {
            hash["Ready"] = false;
            hash["index"] = GetBtnIndex() * -1;
            isready = false;
            Debug.Log("�غ����");
        }
        PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
    }

    // �÷��̾��� ������Ƽ�� ����ɶ����� ����Ǵ� �Լ�
    // �غ� ����, ���, ���� ��, ������ ����
    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        int change_index = 0;
        if (changedProps.ContainsKey("Ready") && ready_ui.activeInHierarchy)
        {
            Debug.Log("����");
            OnClickReady((int)targetPlayer.CustomProperties["index"]);
        }
        else if(changedProps.ContainsKey("Gold") || changedProps.ContainsKey("Monster_Left") || changedProps.ContainsKey("Life"))
        {
            
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
            return;
        }
        
        base.OnPlayerPropertiesUpdate(targetPlayer, changedProps);
    }

    // �÷��̾� �غ� ���°� �Ϸ�Ǹ� ����
    public void CheckAllPlayersReady()
    {
        var players = PhotonNetwork.PlayerList;
        
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
                Debug.Log("���ӽ��� X");
                not_NextPlay.gameObject.SetActive(true);
            }
        }
        else
        {
            Debug.Log("�ο��� �����մϴ�.");
        }
    }

    // �غ�â ������� ���� ����
    [PunRPC]
    public void Off_ReadyUI()
    {
        ready_ui.SetActive(false);
        mapsManager.Map_instantiate();
    }

    // ȭ�� �ϴ� ��� UI
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
