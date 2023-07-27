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
    //[SerializeField] private GameObject create2;
    //[SerializeField] private GameObject create3;

    public int randomInt;

    RaycastHit raycasthit;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < parent.Length; i++)
        {
            randomInt = Random.Range(0, 9);
            Instantiate(prefabCharacter[randomInt], parent[i]);
        }
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

    // ���ΰ�ħ
    public void OnRefresh()
    {
        Debug.Log("���ΰ�ħ");
        for(int i = 0; i < parent.Length; i++)
        {
            //Destroy(parent[i].transform.GetChild(0).gameObject);
            //Image[] image = prefabCharacter;
            
        }
        for (int i = 0; i < parent.Length; i++)
        {
            randomInt = Random.Range(0, 9);
            Instantiate(prefabCharacter[randomInt], parent[i]);
        }
    }
    // ĳ���� ��ȯ
    public void OnCharacterImage()
    {
        Debug.Log("ĳ���� ��ȯ");        
    }


}
