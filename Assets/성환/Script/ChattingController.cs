using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChattingController : MonoBehaviour
{
    [SerializeField] private TMP_Text chat_text;
    [SerializeField] private Transform chat_parent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateChat()
    {
        Instantiate(chat_text, chat_parent);
    }
}
