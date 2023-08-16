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
    /// 골드 세팅(true 값이면 +, false 값이면 -)
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

    // 테스트
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F8))
        {
            SetGold(5, true);
        }
    }
}
