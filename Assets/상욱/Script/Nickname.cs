using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Nickname : MonoBehaviour
{
    private InputField inputField;
    [SerializeField] private TMP_Text playerNametxt;

    string playername;
    string playerset;

    private void Start()
    {
        playername = playerNametxt.text;
    }
    public void OnEndNicknameSetting() // �г��� ������ ����
    {
        playerset = $"{playername}";
        Debug.Log(playerset);
    }

    public void OnButton()
    {
        //SceneManager.LoadScene("Lobby");
        
    }
}
