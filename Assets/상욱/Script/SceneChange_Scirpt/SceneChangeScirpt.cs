using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeScirpt : MonoBehaviour
{
    public void OnLoadingBtn() // 1. Loading_UI 버튼
    {
        SceneManager.LoadScene("2.PlayerName_Setting(UI)");
        Debug.Log("버튼눌림");
    }
}
