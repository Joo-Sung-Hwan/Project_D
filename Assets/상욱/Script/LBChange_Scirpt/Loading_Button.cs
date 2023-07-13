using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Loading_Button : MonoBehaviour
{
    public void OnScenCh()
    {
        SceneManager.LoadScene("PlayerName_Setting(UI)");
        Debug.Log("버튼눌림");
    }

}
