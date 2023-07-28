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

    [Header("�÷��̾ ������ �� ������ ����Ǵ� �̹���")]
    [SerializeField] private Image ready_Image1_1;
    [SerializeField] private Image ready_Image2_2;
    [SerializeField] private Image ready_Image3_3;
    [SerializeField] private Image ready_Image4_4;

    //[SerializeField] private Color ready_Image_color;
    //[SerializeField] private Color not_ReadyImage_color;

    bool ischack = false;

    // ��ư 
    public void OnReady_Image1()
    {
        Debug.Log("ù��° ��ư�� ���Ƚ��ϴ�.");
        ready_Image1.color = new Color(255, 244, 0, 255);
        ready_Image1.gameObject.SetActive(ischack);
        ischack = !ischack;

    }
    public void OnReady_Image2()
    {
        Debug.Log("�ι�° ��ư�� ���Ƚ��ϴ�.");
        
    }
    public void OnReady_Image3()
    {
        Debug.Log("����° ��ư�� ���Ƚ��ϴ�.");
        
    }
    public void OnReady_Image4()
    {
        Debug.Log("�׹�° ��ư�� ���Ƚ��ϴ�.");
       
    }
}
