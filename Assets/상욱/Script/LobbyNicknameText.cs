using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LobbyNicknameText : MonoBehaviour
{
    private static Nickname nickname;
    [SerializeField] private TMP_Text nicknameTxt;

    string str = nickname.playersetName;

    public void Start()
    {
        
    }
    public void NicknameTxt()
    {
        nicknameTxt.text = str;
        //Debug.Log(nicknameTxt.text);
    }
}

