using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // ��差
    public int Gold
    {
        get;set;
    }

    // ���� ���� ��
    public int Monster_Left
    {
        get; set;
    }

    // ���� ������ ��
    public int Life
    {
        get; set;
    }

    /// <summary>
    /// ��� ����(true ���̸� +, false ���̸� -)  / ���� ��ġ�� a �Ű�����
    /// </summary>
    /// <param name="a"></param>
    /// <param name="isAdd"></param>
    public void SetGold(int a, bool isAdd)
    {
        Gold = (isAdd == true) ? Gold += a : Gold -= a;
        InGameUI.instance.SetUserInformation();
    }

    /// <summary>
    /// ���� �� ����(true ���̸� +, false ���̸� -)
    /// </summary>
    /// <param name="isAdd"></param>
    public void SetMonsterLeft(bool isAdd)
    {
        Monster_Left = (isAdd == true) ? Monster_Left += 1 : Monster_Left -= 1;
        InGameUI.instance.SetUserInformation();
    }

    /// <summary>
    /// �������� 1�� ����
    /// </summary>
    public void SetLife(bool isboss)
    {
        Life = (isboss == true) ? Life -= 5 : Life -= 1;
        InGameUI.instance.SetUserInformation();
    }

    private void Start()
    {
        SetGold(10, true);
        Life = 30;
        Monster_Left = 0;
    }

    // �׽�Ʈ
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F8))
        {
            SetGold(5, true);
            Monster_Left += 1;
            InGameUI.instance.SetUserInformation();
        }
    }
}
