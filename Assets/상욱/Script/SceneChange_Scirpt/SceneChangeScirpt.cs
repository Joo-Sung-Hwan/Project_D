using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeScirpt : MonoBehaviour
{
    public void OnLoadingBtn() // 1. Loading_UI ��ư
    {
        SceneManager.LoadScene("2.PlayerName_Setting(UI)");
        Debug.Log("��ư����");
    }
}
