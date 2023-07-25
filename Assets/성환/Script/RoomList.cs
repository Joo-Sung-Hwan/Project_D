using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoomList : MonoBehaviour
{
    [SerializeField] private TMP_Text room;

    private void Update()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(() => OnClickRoomList());
    }
    public string OnClickRoomList()
    {
        return room.text;
    }
}
