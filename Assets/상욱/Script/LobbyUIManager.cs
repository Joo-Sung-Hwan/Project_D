using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LobbyUIManager : MonoBehaviour
{
    [Header("닉네임 설정")]
    [SerializeField] private TMP_InputField playerNametxt;
    [SerializeField] private TMP_InputField c_playerNametext;
    public Image lastNicknameSettingUI;

    // playerNametxt에 쓴 닉네임을 입력
    [SerializeField] private TMP_Text playerLasttxt;
    [SerializeField] private TMP_Text questionstxt;

    // 닉네임UI
    public Image nickName;

    [HideInInspector] public string playersetName;

    string playerLast;

    [Header("방 설정")]
    // 방생성 UI
    
    [SerializeField] private Image roomCreate_BG;
    [SerializeField] private Button createBtn;

    string roomTexttext;
    bool isCreatecheck = false;
    bool first_start = true;

    private void Awake()
    {
        if (first_start == false)
        {
            nickName.gameObject.SetActive(false);
        }
        else
        {
            nickName.gameObject.SetActive(true);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (first_start == false)
        {
            nickName.gameObject.SetActive(false);
        }
        else
        {
            nickName.gameObject.SetActive(true);
        }
    }
    public void Set_First(bool ischeck)
    {
        first_start = ischeck;
    }
    // Update is called once per frame
    void Update()
    {
        //Debug.Log(first_start);
    }
    // 마지막 질문창 텍스트
    public void Imagetext()
    {
        playerLast = playersetName;
        playerLasttxt.text = $"캐릭터 이름 : {playerLast}";
        questionstxt.text = $"캐릭터 이름은 {playerLast} 입니다. 이대로 진행하시겠습니까?";
    }

    // 닉네임 마지막질문창에서 "아니요"누르면 마지막질문창이 꺼짐
    public void OnOffNickNameUI()
    {
        isCreatecheck = false;
        c_playerNametext.text = string.Empty;
        lastNicknameSettingUI.gameObject.SetActive(false);
        nickName.gameObject.SetActive(true);
    }

    // 마지막 닉네임질문 나오기전 확인버튼
    public void OnButton()
    {
        if (isCreatecheck == false)
        {
            playersetName = playerNametxt.text;
        }
        else
        {
            playersetName = c_playerNametext.text;
        }
        lastNicknameSettingUI.transform.gameObject.SetActive(true);
        Imagetext();
        nickName.transform.gameObject.SetActive(false);
    }
    // 방생성버튼
    public void OnRoomCreate()
    {
        // isCreatecheck = true;
        roomCreate_BG.transform.gameObject.SetActive(true);
    }

    // 방생성UI 가리는 버튼
    public void OnRoomCreateOff()
    {
        roomCreate_BG.transform.gameObject.SetActive(false);
    }
    public void OnCreateStartBtn()
    {
        roomCreate_BG.transform.gameObject.SetActive(true);
    }
    public void OnCreateStartBtn_OFF()
    {
        roomCreate_BG.transform.gameObject.SetActive(false);
    }
    public void OnClickCreate()
    {
        PhotonManager.instance.OnClickToCreateRoom();
    }
}
