using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirendUI : MonoBehaviour
{
    [SerializeField] private Image firendList;
    // Start is called before the first frame update
    void Start()
    {
        firendList.transform.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnFirendList()
    {
        firendList.transform.gameObject.SetActive(true);
    }
    public void OnOffFirendList()
    {
        firendList.transform.gameObject.SetActive(false);
    }
}
