using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnLevelUP()
    {
        Debug.Log("레벨업");
    }

    public void OnRefresh() // 새로고침
    {
        Debug.Log("새로고침");
    }
}
