using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LobbyUIManager : MonoBehaviour
{
    [Header("�г��� ����")]
    [SerializeField] private TMP_InputField playerNametxt;
    [SerializeField] private TMP_InputField c_playerNametext;
    public Image lastNicknameSettingUI;

    // playerNametxt�� �� �г����� �Է�
    [SerializeField] private TMP_Text playerLasttxt;
    [SerializeField] private TMP_Text questionstxt;

    // �г���UI
    [SerializeField] private Image nickName;

    [HideInInspector] public string playersetName;

    string playerLast;

    [Header("�� ����")]
    // ����� UI
    
    [SerializeField] private Image roomCreate_BG;
    [SerializeField] private Button createBtn;

    string roomTexttext;
    bool isCreatecheck = false;

    // Start is called before the first frame update
    void Start()
    {
        lastNicknameSettingUI.transform.gameObject.SetActive(false);
        roomCreate_BG.transform.gameObject.SetActive(false);
        roomCreate_BG.transform.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        createBtn.onClick.AddListener(OnClickCreate);
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
        lastNicknameSettingUI.gameObject.SetActive(false);
        nickName.gameObject.SetActive(true);
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
        roomCreate_BG.transform.gameObject.SetActive(true);
    }

    // �����UI ������ ��ư
    public void OnRoomCreateOff()
    {
        roomCreate_BG.transform.gameObject.SetActive(false);
    }
    public void OnCreateStartBtn()
    {
        roomCreate_BG.transform.gameObject.SetActive(true);
    }
    public void OnCreateStartBtn_OFF()
    {
        roomCreate_BG.transform.gameObject.SetActive(false);
    }
    public void OnClickCreate()
    {
        PhotonManager.instance.OnClickToCreateRoom();
    }
}
