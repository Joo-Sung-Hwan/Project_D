using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class RoomList : MonoBehaviour
{
    public TMP_Text room;

    
    public void OnClickRoomList()
    {
        PhotonManager.instance.join_room_name = room.text;
        Debug.Log(PhotonManager.instance.join_room_name);
    }
}
