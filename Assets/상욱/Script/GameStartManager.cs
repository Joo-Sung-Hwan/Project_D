using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    [SerializeField] private TMP_Text nameText;

    //[SerializeField] private Color ready_Image_color;
    //[SerializeField] private Color not_ReadyImage_color;

    bool ischack = false;

    private void Start()
    {
        ready_Image1_1.gameObject.SetActive(false);
        ready_Image2_2.gameObject.SetActive(false);
        ready_Image3_3.gameObject.SetActive(false);
        ready_Image4_4.gameObject.SetActive(false);
    }
    // 버튼 
    public void OnReady_Image1()
    {
        Debug.Log("첫번째 버튼이 눌렸습니다.");
        ready_Image1.color = new Color(255, 244, 0, 255);
        ready_Image1.gameObject.SetActive(!ischack);
        ischack = !ischack;
        ready_Image1.gameObject.SetActive(ischack);

    }
    public void OnReady_Image2()
    {
        Debug.Log("두번째 버튼이 눌렸습니다.");
        ready_Image2.color = new Color(255, 244, 0, 255);
        ready_Image2.gameObject.SetActive(!ischack);
        ischack = !ischack;
        ready_Image2.gameObject.SetActive(ischack);

    }
    public void OnReady_Image3()
    {
        Debug.Log("세번째 버튼이 눌렸습니다.");
        ready_Image3.color = new Color(255, 244, 0, 255);
        ready_Image3.gameObject.SetActive(!ischack);
        ischack = !ischack;
        ready_Image3.gameObject.SetActive(ischack);

    }
    public void OnReady_Image4()
    {
        Debug.Log("네번째 버튼이 눌렸습니다.");
        ready_Image4.color = new Color(255, 244, 0, 255);
        ready_Image4.gameObject.SetActive(!ischack);
        ischack = !ischack;
        ready_Image4.gameObject.SetActive(ischack);

    }
}
