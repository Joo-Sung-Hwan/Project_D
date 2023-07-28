using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStartManager : MonoBehaviour
{
    [SerializeField] private Image ready_Image1;
    [SerializeField] private Image ready_Image2;
    [SerializeField] private Image ready_Image3;
    [SerializeField] private Image ready_Image4;

    [Header("플레이어가 눌렀을 때 색상이 변경되는 이미지")]
    [SerializeField] private Image ready_Image1_1;
    [SerializeField] private Image ready_Image2_2;
    [SerializeField] private Image ready_Image3_3;
    [SerializeField] private Image ready_Image4_4;

    //[SerializeField] private Color ready_Image_color;
    //[SerializeField] private Color not_ReadyImage_color;

    bool ischack = false;

    // 버튼 
    public void OnReady_Image1()
    {
        Debug.Log("첫번째 버튼이 눌렸습니다.");
        ready_Image1.color = new Color(255, 244, 0, 255);
        ready_Image1.gameObject.SetActive(ischack);
        ischack = !ischack;

    }
    public void OnReady_Image2()
    {
        Debug.Log("두번째 버튼이 눌렸습니다.");
        
    }
    public void OnReady_Image3()
    {
        Debug.Log("세번째 버튼이 눌렸습니다.");
        
    }
    public void OnReady_Image4()
    {
        Debug.Log("네번째 버튼이 눌렸습니다.");
       
    }
}
