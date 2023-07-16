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
    //[SerializeField] private TMP_Text playerSettxt;
    string playerset = "";
    //string playernameset = "";
    void Start()
    {
        imageUI.transform.gameObject.SetActive(false);
    }


    public void OnEndNicknameSetting() // 닉네임설정
    {
        playerset = $"{playerNametxt.text}";
        Debug.Log(playerset);
        //if(Input.GetKeyDown(KeyCode.E))
    }

    public void OnButton()
    {
        //SceneManager.LoadScene("Lobby");
        
    }
}
