using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // 골드량
    public int Gold
    {
        get;set;
    }

    // 남은 몬스터 수
    public int Monster_Left
    {
        get; set;
    }

    // 남은 라이프 수
    public int Life
    {
        get; set;
    }

    /// <summary>
    /// 골드 세팅(true 값이면 +, false 값이면 -)  / 증감 수치는 a 매개변수
    /// </summary>
    /// <param name="a"></param>
    /// <param name="isAdd"></param>
    public void SetGold(int a, bool isAdd)
    {
        Gold = (isAdd == true) ? Gold += a : Gold -= a;
        InGameUI.instance.SetUserInformation();
    }

    /// <summary>
    /// 몬스터 수 세팅(true 값이면 +, false 값이면 -)
    /// </summary>
    /// <param name="isAdd"></param>
    public void SetMonsterLeft(bool isAdd)
    {
        Monster_Left = (isAdd == true) ? Monster_Left += 1 : Monster_Left -= 1;
        InGameUI.instance.SetUserInformation();
    }

    /// <summary>
    /// 라이프는 1씩 감소
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

    // 테스트
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
