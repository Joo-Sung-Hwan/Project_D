using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NickNameSettingManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField playerNametxt;
    [SerializeField] private TMP_InputField c_playerNametext;
    [SerializeField] private Image lastNicknameSettingUI;
    // playerNametxt에 쓴 닉네임을 입력
    [SerializeField] private TMP_Text playerLasttxt;
    [SerializeField] private TMP_Text questionstxt;
    [HideInInspector] public string playersetName;

    string playerLast;
    bool isCreatecheck = false;

    // Start is called before the first frame update
    void Start()
    {
        lastNicknameSettingUI.transform.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

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
        lastNicknameSettingUI.transform.gameObject.SetActive(false);
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
    }
}
