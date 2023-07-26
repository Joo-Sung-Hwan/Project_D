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
    // 게임오브젝트를 담는 곳(부모)
    [SerializeField] private Transform callCharacter1;
    [SerializeField] private Transform callCharacter2;
    [SerializeField] private Transform callCharacter3;
    // 게임오브젝트생성(자식)
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

    // 레벌업
    public void OnLevelUP() 
    {
        Debug.Log("레벨업");
    }

    public void OnRefresh() // 새로고침
    {
        Debug.Log("새로고침");
        for (int i = 0; i <= prefabCharacte.Length; i++)
        {
            if (i <= 5)
            {
                Instantiate(prefabCharacte[i], parent1);
            }
        }
                     
        

    }
    // 캐릭터 소환
    public void OnCharacterImage()
    {
        Debug.Log("캐릭터 소환");        
    }


}
