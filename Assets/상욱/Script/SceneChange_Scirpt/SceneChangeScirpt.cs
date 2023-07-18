using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneChangeScirpt : MonoBehaviour
{
    public void OnLoadingBtn() // 1. Loading_UI 버튼
    {
        SceneManager.LoadScene("2.PlayerName_Setting(UI)");
        Debug.Log("버튼눌림");
    }
    public void OnPlayerName_Setting_UIBtn()
    {
        SceneManager.LoadScene("3.Lobby");
        Debug.Log("버튼눌림");
    }
    
    public void OnInGameUI() // 임시 인게임UI신 전환
    {
        SceneManager.LoadScene("4.InGameUI");
    }
 
    public void OnScene() // 인게임신전환버튼
    {
        SceneManager.LoadScene("Scene");
    }

}
