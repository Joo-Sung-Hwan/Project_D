using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Nickname : MonoBehaviour
{
    public InputField inputField;
    [SerializeField] private TMP_Text nickname;

    string playerNames;

    private void Start()
    {
        //playerNames = inputField.GetComponent<InputField>().text;
    }
    public void OnNicknameSetting()
    {
        playerNames = inputField.text;
        Debug.Log($"{playerNames}");
    }

    public void OnButton()
    {
        //SceneManager.LoadScene("Lobby");
        Debug.Log("닉네임이 설정되었습니다.");
    }
}
