using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartSettingManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField playerNametxt;
    [SerializeField] private Image imageUI;
    // imageUI �̹����ȿ� �ִ� �ؽ�Ʈ
    [SerializeField] private TMP_Text playerLasttxt; // playerNametxt�� �� �г����� �Է�
    // ������ ���� �ؽ�Ʈ
    [SerializeField] private TMP_Text Questionstxt;

    [HideInInspector] public string playersetName;
    // ����� UI
    [SerializeField] private Image roomCreate;

    string playerLast;

    void Start()
    {
        imageUI.transform.gameObject.SetActive(false);
        roomCreate.transform.gameObject.SetActive(false);

    }


    public void Imagetext()
    {
        playerLast = playersetName;
        playerLasttxt.text = $"ĳ���� �̸� : {playerLast}";
        Questionstxt.text = $"ĳ���� �̸��� {playerLast} �Դϴ�. �̴�� �����Ͻðڽ��ϱ�?";
    }

    // ������ �г���imageUI �������� Ȯ�ι�ư
    public void OnButton()
    {
        playersetName = playerNametxt.text;
        imageUI.transform.gameObject.SetActive(true);
        Imagetext();
    }

    public void OnOffNickNameUI()
    {
        imageUI.transform.gameObject.SetActive(false);
    }

    // �������ư
    public void OnRoomCreate()
    {
        roomCreate.transform.gameObject.SetActive(true);
    }

    public void OnRoomCreateOff()
    {
        roomCreate.transform.gameObject.SetActive(false);
    }
}
