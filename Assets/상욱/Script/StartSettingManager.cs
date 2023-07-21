using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartSettingManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField playerNametxt;
    [SerializeField] private Image imageUI;
    // imageUI 이미지안에 있는 텍스트
    [SerializeField] private TMP_Text playerLasttxt; // playerNametxt에 쓴 닉네임을 입력
    // 마지막 질문 텍스트
    [SerializeField] private TMP_Text Questionstxt;

    [HideInInspector] public string playersetName;
    // 방생성 UI
    [SerializeField] private Image roomCreate;

    string playerLast;

    void Start()
    {
        imageUI.transform.gameObject.SetActive(false);
        roomCreate.transform.gameObject.SetActive(false);

    }


    public void Imagetext()
    {
        playerLast = playersetName;
        playerLasttxt.text = $"캐릭터 이름 : {playerLast}";
        Questionstxt.text = $"캐릭터 이름은 {playerLast} 입니다. 이대로 진행하시겠습니까?";
    }

    // 마지막 닉네임imageUI 나오기전 확인버튼
    public void OnButton()
    {
        playersetName = playerNametxt.text;
        imageUI.transform.gameObject.SetActive(true);
        Imagetext();
    }

    public void OnOffNickNameUI()
    {
        imageUI.transform.gameObject.SetActive(false);
    }

    // 방생성버튼
    public void OnRoomCreate()
    {
        roomCreate.transform.gameObject.SetActive(true);
    }

    public void OnRoomCreateOff()
    {
        roomCreate.transform.gameObject.SetActive(false);
    }
}
