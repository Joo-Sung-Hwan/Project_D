using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NickNameSettingManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField playerNametxt;
    [SerializeField] private TMP_InputField c_playerNametext;
    [SerializeField] private Image lastNicknameSettingUI;
    // playerNametxt�� �� �г����� �Է�
    [SerializeField] private TMP_Text playerLasttxt;
    [SerializeField] private TMP_Text questionstxt;

    // ����� UI
    [SerializeField] private Image roomCreate;

    // �г���UI
    [SerializeField] private Image nickName;



    [HideInInspector] public string playersetName;
    string roomTexttext;
    string playerLast;
    bool isCreatecheck = false;

    // Start is called before the first frame update
    void Start()
    {
        lastNicknameSettingUI.transform.gameObject.SetActive(false);
        roomCreate.transform.gameObject.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    // ������ ����â �ؽ�Ʈ
    public void Imagetext()
    {
        playerLast = playersetName;
        playerLasttxt.text = $"ĳ���� �̸� : {playerLast}";
        questionstxt.text = $"ĳ���� �̸��� {playerLast} �Դϴ�. �̴�� �����Ͻðڽ��ϱ�?";
    }

    // �г��� ����������â���� "�ƴϿ�"������ ����������â�� ����
    public void OnOffNickNameUI()
    {
        isCreatecheck = false;
        c_playerNametext.text = string.Empty;
        lastNicknameSettingUI.transform.gameObject.SetActive(false);
    }
    
    // ������ �г������� �������� Ȯ�ι�ư
    public void OnButton()
    {
        if (isCreatecheck == false)
        {
            playersetName = playerNametxt.text;
        }
        else
        {
            playersetName = c_playerNametext.text;
        }
        lastNicknameSettingUI.transform.gameObject.SetActive(true);
        Imagetext();
        nickName.transform.gameObject.SetActive(false);
    }
    // �������ư
    public void OnRoomCreate()
    {
        // isCreatecheck = true;
        roomCreate.transform.gameObject.SetActive(true);
    }

    // �����UI ������ ��ư
    public void OnRoomCreateOff()
    {
        roomCreate.transform.gameObject.SetActive(false);
    }
}
