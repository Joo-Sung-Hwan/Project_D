using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InGameUI : MonoBehaviour
{
    [Header("BottomBG")]
    [SerializeField] private Image[] prefabCharacter;
    [SerializeField] private Transform[] parent;

    [Header("대기석")]
    // 게임오브젝트를 담는 곳(부모)
    [SerializeField] private Transform callCharacter1;
    [SerializeField] private Transform callCharacter2;
    [SerializeField] private Transform callCharacter3;
    // 게임오브젝트생성(자식)
    [SerializeField] private GameObject create1;

    [Header("Score_UI")]
    [SerializeField] private Image firendList;
    [SerializeField] private GameObject[] player; 

    [Header("Server_Name_UI")]
    public TMP_Text server_name;
    public int randomInt;

    bool ischeck = true;

    private void Awake()
    {
        server_name.text = PhotonManager.instance.join_room_name;
        SetUserName();


    }
    // Start is called before the first frame update
    void Start()
    {
        SetBottomImage();
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetUserName()
    {
        for(int i = 0; i < PhotonManager.instance.playerList.Length; i++)
        {
            player[i].transform.GetChild(0).GetComponent<TMP_Text>().text = PhotonManager.instance.playerList[i].NickName;
        }
    }
    // 레벌업
    public void OnLevelUP()
    {
        Debug.Log("레벨업");
    }

    public void SetBottomImage()
    {
        for (int i = 0; i < parent.Length; i++)
        {
            randomInt = Random.Range(0, 9);
            Instantiate(prefabCharacter[randomInt], parent[i]);
        }
    }

    // 새로고침
    public void OnRefresh()
    {
        Debug.Log("새로고침");
        for (int i = 0; i < parent.Length; i++)
        {
            randomInt = Random.Range(0, 9);
            parent[i].transform.GetChild(0).GetComponent<Image>().sprite = prefabCharacter[randomInt].sprite;
        }
    }
    // 캐릭터 소환
    public void OnCharacterImage()
    {
        Debug.Log("캐릭터 소환");
    }

    public void OnFirendList()
    {
        firendList.transform.gameObject.SetActive(ischeck);
        ischeck = !ischeck;
    }



}
