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

    // bool ischeck = false;
    bool[] ischeck_array = new bool[4];
    int count = 0;

    private void Start()
    {
        /*
        for (int i = 0; i < ready_Image.Length; i++)
        {
            ready_Image[i].gameObject.SetActive(false);
        }
        */
        for(int i = 0; i < ischeck_array.Length; i++)
        {
            ischeck_array[i] = true;
        }
        notNextPlay.gameObject.SetActive(false);
    }

    public void OnClickReady(int a)
    {
        if (ischeck_array[a])
        {
            ready_Image[a].color = Color.yellow;
            count++;
        }
        else
        {
            ready_Image[a].color = Color.white;
            count--;
        }
        ischeck_array[a] = !ischeck_array[a];


    }
    /*
    // ��ư 
    public void OnReady_Image1()
    {
        ready_Image[0].gameObject.SetActive(!ischeck);
        ischeck = !ischeck;
        ready_Image[0].gameObject.SetActive(ischeck);
        Debug.Log("ù��° ��ư�� ���Ƚ��ϴ�.");
    }
    public void OnReady_Image2()
    {
        ready_Image[1].gameObject.SetActive(!ischeck);
        ischeck = !ischeck;
        ready_Image[1].gameObject.SetActive(ischeck);
        Debug.Log("�ι�° ��ư�� ���Ƚ��ϴ�.");
    }

    public void OnReady_Image3()
    {
        ready_Image[2].gameObject.SetActive(!ischeck);
        ischeck = !ischeck;
        ready_Image[2].gameObject.SetActive(ischeck);
        Debug.Log("����° ��ư�� ���Ƚ��ϴ�.");
    }
    public void OnReady_Image4()
    {
        ready_Image[3].gameObject.SetActive(!ischeck);
        ischeck = !ischeck;
        ready_Image[3].gameObject.SetActive(ischeck);
        Debug.Log("�׹�° ��ư�� ���Ƚ��ϴ�.");
    }*/

    public void OnGameStart()
    {
        if(count == 4)
        {
            Debug.Log("���ӽ���");
        }
        else
        {
            Debug.Log("���ӽ��� X");
            notNextPlay.gameObject.SetActive(true);
        }
        /*
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
        */
    }

    public void OnNotNextPlayChack()
    {
        notNextPlay.gameObject.SetActive(false);
    }
}
