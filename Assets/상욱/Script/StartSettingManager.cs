using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartSettingManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField playerNametxt;
    [SerializeField] private TMP_InputField c_playerNametext;
    [SerializeField] private Image imageUI;
    // imageUI 이미지안에 있는 텍스트
    [SerializeField] private TMP_Text playerLasttxt; // playerNametxt에 쓴 닉네임을 입력
    // 마지막 질문 텍스트
    [SerializeField] private TMP_Text Questionstxt;

    [HideInInspector] public string playersetName;
    // 방생성 UI
    [SerializeField] private Image roomCreate;
    // 닉네임 비교UI
    [SerializeField] private Image nickNameCheck;

    bool isCreatecheck = false;


    string playerLast;

    string roomTexttext;

    void Start()
    {
        imageUI.transform.gameObject.SetActive(false);
        roomCreate.transform.gameObject.SetActive(false);
        nickNameCheck.transform.gameObject.SetActive(false);
    }


    // 마지막 질문창 텍스트
    public void Imagetext()
    {
        playerLast = playersetName;
        playerLasttxt.text = $"캐릭터 이름 : {playerLast}";
        Questionstxt.text = $"캐릭터 이름은 {playerLast} 입니다. 이대로 진행하시겠습니까?";
    }

    // 마지막 닉네임imageUI 나오기전 확인버튼
    public void OnButton()
    {
        if(isCreatecheck == false)
        {
            playersetName = playerNametxt.text;
        }
        else
        {
            playersetName = c_playerNametext.text;
        }
        imageUI.transform.gameObject.SetActive(true);
        Imagetext();
    }

    // 닉네임 마지막질문창에서 "아니요"누르면 마지막질문창이 꺼짐
    public void OnOffNickNameUI()
    {
        isCreatecheck = false;
        c_playerNametext.text = string.Empty;
        imageUI.transform.gameObject.SetActive(false);
    }

    // 방생성버튼
    public void OnRoomCreate()
    {
        isCreatecheck = true;
        roomCreate.transform.gameObject.SetActive(true);
    }

    // 방생성UI 가리는 버튼
    public void OnRoomCreateOff()
    {
        roomCreate.transform.gameObject.SetActive(false);
    }

    public void SetRoomText()
    {
       
    }
}
