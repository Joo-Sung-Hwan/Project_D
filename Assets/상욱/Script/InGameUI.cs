using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    [SerializeField] private Image[] prefabCharacte;
    [SerializeField] private Transform parent1;
    [SerializeField] private Transform parent2;
    [SerializeField] private Transform parent3;
    [SerializeField] private Transform parent4;
    [SerializeField] private Transform parent5;
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
        randomInt = Random.Range(0,100);
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

    public void OnRefresh() // ���ΰ�ħ
    {
        Debug.Log("���ΰ�ħ");
        for (int i = 0; i <= prefabCharacte.Length; i++)
        {
            if (i <= 5)
            {
                Instantiate(prefabCharacte[i], parent1);
            }
        }
                     
        

    }
    // ĳ���� ��ȯ
    public void OnCharacterImage()
    {
        Debug.Log("ĳ���� ��ȯ");        
    }


}
