using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Nickname : MonoBehaviour
{
    [SerializeField] private TMP_InputField playerNametxt;
    [SerializeField] private Image imageUI;
    // imageUI 이미지안에 있는 텍스트
    [SerializeField] private TMP_Text playerLasttxt; // playerNametxt에 쓴 닉네임을 입력
    // 마지막 질문 텍스트
    [SerializeField] private TMP_Text Questionstxt;

    public string playersetName;
    string playerLast;

    void Start()
    {
        imageUI.transform.gameObject.SetActive(false);

    }

    public void OnNicknameSetting() // 닉네임설정
    {
        playersetName = $"{playerNametxt.text}";
        Debug.Log(playersetName);
    }

    public void Imagetext()
    {
        playerLast = $"{playersetName}";
        Debug.Log(playerLast);
        playerLasttxt.text = $"캐릭터 이름 : {playerLast}";
        Questionstxt.text = $"캐릭터 이름은 {playerLast} 입니다. 이대로 진행하시겠습니까?";
    }

    // 마지막 닉네임imageUI 나오기전 확인버튼
    public void OnButton()
    {
        imageUI.transform.gameObject.SetActive(true);
        Imagetext();
    }
    // 
    public void OnOffNickNameUI()
    {
        imageUI.transform.gameObject.SetActive(false);
    }
}
