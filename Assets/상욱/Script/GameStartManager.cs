using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameStartManager : MonoBehaviour
{
    [Header("InGame")]
    [SerializeField] private Image[] ready_Image;
    [SerializeField] private TMP_Text nameText;
    [Header("Ready_BG")]
    // �ΰ��ӽ����� �غ�â 
    [SerializeField] private GameObject ready_BG;
    [SerializeField] private Image not_NextPlay;
    [SerializeField] private Image not_Executive;



    // bool ischeck = false;
    bool[] ischeck_array = new bool[4];
    int count = 0;

    private void Start()
    {
        
        for (int i = 0; i < ischeck_array.Length; i++)
        {
            ischeck_array[i] = true;
        }
        not_NextPlay.gameObject.SetActive(false);
        not_Executive.gameObject.SetActive(false);

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

    public void OnGameStart()
    {
        if (count == 4)
        {
            Debug.Log("���ӽ���");
            //ready_BG.gameObject.SetActive(false);
            not_Executive.gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("���ӽ��� X");
            not_NextPlay.gameObject.SetActive(true);
        }
    }

    public void OnNotNextPlayChack()
    {
        not_NextPlay.gameObject.SetActive(false);
    }

    // ���常 ������ �� �ִٴ� ����ݴ¹�ư
    public void OnNotExecutive()
    {
        not_Executive.gameObject.SetActive(false);
    }
}
