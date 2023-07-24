using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartSettingManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField playerNametxt;
    [SerializeField] private TMP_InputField c_playerNametext;
    [SerializeField] private Image imageUI;
    // imageUI �̹����ȿ� �ִ� �ؽ�Ʈ
    [SerializeField] private TMP_Text playerLasttxt; // playerNametxt�� �� �г����� �Է�
    // ������ ���� �ؽ�Ʈ
    [SerializeField] private TMP_Text Questionstxt;

    [HideInInspector] public string playersetName;
    // ����� UI
    [SerializeField] private Image roomCreate;
    // �г��� ��UI
    [SerializeField] private Image nickNameCheck;

    bool isCreatecheck = false;


    string playerLast;

    string roomTexttext;

    void Start()
    {
        imageUI.transform.gameObject.SetActive(false);
        roomCreate.transform.gameObject.SetActive(false);
        nickNameCheck.transform.gameObject.SetActive(false);
    }


    // ������ ����â �ؽ�Ʈ
    public void Imagetext()
    {
        playerLast = playersetName;
        playerLasttxt.text = $"ĳ���� �̸� : {playerLast}";
        Questionstxt.text = $"ĳ���� �̸��� {playerLast} �Դϴ�. �̴�� �����Ͻðڽ��ϱ�?";
    }

    // ������ �г���imageUI �������� Ȯ�ι�ư
    public void OnButton()
    {
        if(isCreatecheck == false)
        {
            playersetName = playerNametxt.text;
        }
        else
        {
            playersetName = c_playerNametext.text;
        }
        imageUI.transform.gameObject.SetActive(true);
        Imagetext();
    }

    // �г��� ����������â���� "�ƴϿ�"������ ����������â�� ����
    public void OnOffNickNameUI()
    {
        isCreatecheck = false;
        c_playerNametext.text = string.Empty;
        imageUI.transform.gameObject.SetActive(false);
    }

    // �������ư
    public void OnRoomCreate()
    {
        isCreatecheck = true;
        roomCreate.transform.gameObject.SetActive(true);
    }

    // �����UI ������ ��ư
    public void OnRoomCreateOff()
    {
        roomCreate.transform.gameObject.SetActive(false);
    }

    public void SetRoomText()
    {
       
    }
}
