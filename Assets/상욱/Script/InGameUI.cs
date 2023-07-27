using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    [SerializeField] private Image[] prefabCharacter;
    [SerializeField] private Transform[] parent;

    // ���ӿ�����Ʈ�� ��� ��(�θ�)
    [SerializeField] private Transform callCharacter1;
    [SerializeField] private Transform callCharacter2;
    [SerializeField] private Transform callCharacter3;
    // ���ӿ�����Ʈ����(�ڽ�)
    [SerializeField] private GameObject create1;


    [SerializeField] private Image firendList;

    public int randomInt;

    bool ischeck = true;

    // Start is called before the first frame update
    void Start()
    {
        SetBottomImage();
        firendList.transform.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    // ������
    public void OnLevelUP()
    {
        Debug.Log("������");
    }

    public void SetBottomImage()
    {
        for (int i = 0; i < parent.Length; i++)
        {
            randomInt = Random.Range(0, 9);
            Instantiate(prefabCharacter[randomInt], parent[i]);
        }
    }

    // ���ΰ�ħ
    public void OnRefresh()
    {
        Debug.Log("���ΰ�ħ");
        for (int i = 0; i < parent.Length; i++)
        {
            randomInt = Random.Range(0, 9);
            parent[i].transform.GetChild(0).GetComponent<Image>().sprite = prefabCharacter[randomInt].sprite;
        }
    }
    // ĳ���� ��ȯ
    public void OnCharacterImage()
    {
        Debug.Log("ĳ���� ��ȯ");
    }

    public void OnFirendList()
    {
        firendList.transform.gameObject.SetActive(ischeck);
        ischeck = !ischeck;
    }



}
