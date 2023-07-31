using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameStartManager : MonoBehaviour
{
    [SerializeField] private Image[] ready_Image;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private Image notNextPlay;

    bool ischack = false;

    private void Start()
    {
        for (int i = 0; i < ready_Image.Length; i++)
        {
            ready_Image[i].gameObject.SetActive(false);
        }
        notNextPlay.gameObject.SetActive(false);
    }
    // ��ư 
    public void OnReady_Image1()
    {
        ready_Image[0].gameObject.SetActive(!ischack);
        ischack = !ischack;
        ready_Image[0].gameObject.SetActive(ischack);
        Debug.Log("ù��° ��ư�� ���Ƚ��ϴ�.");
    }
    public void OnReady_Image2()
    {
        ready_Image[1].gameObject.SetActive(!ischack);
        ischack = !ischack;
        ready_Image[1].gameObject.SetActive(ischack);
        Debug.Log("�ι�° ��ư�� ���Ƚ��ϴ�.");
    }

    public void OnReady_Image3()
    {
        ready_Image[2].gameObject.SetActive(!ischack);
        ischack = !ischack;
        ready_Image[2].gameObject.SetActive(ischack);
        Debug.Log("����° ��ư�� ���Ƚ��ϴ�.");
    }
    public void OnReady_Image4()
    {
        ready_Image[3].gameObject.SetActive(!ischack);
        ischack = !ischack;
        ready_Image[3].gameObject.SetActive(ischack);
        Debug.Log("�׹�° ��ư�� ���Ƚ��ϴ�.");
    }

    public void OnGameStart()
    {
        for (int i = 0; i < ready_Image.Length; i++)
        {
            if (ready_Image[i].gameObject.activeInHierarchy == false)
            {
                Debug.Log("������ �������� �ʽ��ϴ�.");
                notNextPlay.gameObject.SetActive(true);
            }
            else
            {
                Debug.Log("������ �����մϴ�.");
            }
        }
    }

    public void OnNotNextPlayChack()
    {
        notNextPlay.gameObject.SetActive(false);
    }
}
