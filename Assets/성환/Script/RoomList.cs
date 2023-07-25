using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoomList : MonoBehaviour
{
    public TMP_Text room;

    private void Update()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(OnClickRoomList);
    }
    public void OnClickRoomList()
    {
        PhotonManager.instance.join_room_name = room.text;
    }
}
