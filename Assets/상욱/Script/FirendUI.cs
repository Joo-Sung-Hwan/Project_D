using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirendUI : MonoBehaviour
{
    [SerializeField] private Image firendList;
    bool ischeck = true;
    // Start is called before the first frame update
    void Start()
    {
        firendList.transform.gameObject.SetActive(false);
    }

    public void OnFirendList()
    {
        firendList.transform.gameObject.SetActive(ischeck);
        ischeck = !ischeck;
    }

}
