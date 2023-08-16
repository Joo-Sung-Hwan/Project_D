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
    /// ��� ����(true ���̸� +, false ���̸� -)
    /// </summary>
    /// <param name="a"></param>
    /// <param name="isAdd"></param>
    public void SetGold(int a, bool isAdd)
    {
        Gold = (isAdd == true) ? Gold += a : Gold -= a;
    }

    private void Start()
    {
        SetGold(0, true);
    }

    // �׽�Ʈ
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F8))
        {
            SetGold(5, true);
        }
    }
}
