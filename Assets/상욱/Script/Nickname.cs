using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Nickname : MonoBehaviour
{
    [SerializeField] private TMP_InputField playerNametxt;
    [SerializeField] private Image imageUI;
    // imageUI �̹����ȿ� �ִ� �ؽ�Ʈ
    [SerializeField] private TMP_Text playerLasttxt; // playerNametxt�� �� �г����� �Է�
    // ������ ���� �ؽ�Ʈ
    [SerializeField] private TMP_Text Questionstxt;

    public string playersetName;
    string playerLast;

    void Start()
    {
        imageUI.transform.gameObject.SetActive(false);

    }

    public void OnNicknameSetting() // �г��Ӽ���
    {
        playersetName = $"{playerNametxt.text}";
        Debug.Log(playersetName);
    }

    public void Imagetext()
    {
        playerLast = $"{playersetName}";
        Debug.Log(playerLast);
        playerLasttxt.text = $"ĳ���� �̸� : {playerLast}";
        Questionstxt.text = $"ĳ���� �̸��� {playerLast} �Դϴ�. �̴�� �����Ͻðڽ��ϱ�?";
    }

    // ������ �г���imageUI �������� Ȯ�ι�ư
    public void OnButton()
    {
        imageUI.transform.gameObject.SetActive(true);
        Imagetext();
    }
    // 
    public void OnOffNickNameUI()
    {
        imageUI.transform.gameObject.SetActive(false);
    }
}
