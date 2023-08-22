using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PersonalCamera : MonoBehaviour
{
    [SerializeField] private Camera main_camera;
    Vector3 camera_pos;
    

    // Start is called before the first frame update
    void Start()
    {
        //Add_Camera_pos();
        Set_Camera_Pos();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Set_Camera_Pos()
    {
        camera_pos = Camera.main.transform.position;
        switch (PhotonNetwork.LocalPlayer.ActorNumber)
        {
            case 1:
                break;
            case 2:
                main_camera.transform.position += Vector3.forward * 17;
                break;
            case 3:
                main_camera.transform.position += Vector3.right * 17;
                break;
            case 4:
                main_camera.transform.position += (Vector3.forward + Vector3.right) * 17;
                break;
            default:
                break;
        }
    }

    public void Onclick_Change_Camera(int i)
    {
        switch (i)
        {
            case 0:
                main_camera.transform.position = camera_pos + new Vector3(0, 0, 0);
                break;
            case 1:
                main_camera.transform.position = camera_pos + new Vector3(0, 0, 17);
                break;
            case 2:
                main_camera.transform.position = camera_pos + new Vector3(17, 0, 0);
                break;
            case 3:
                main_camera.transform.position = camera_pos + new Vector3(17, 0, 17);
                break;
            default:
                break;
        }
        //InGameUI.instance.ShowSynergy(i);
        //Debug.Log(i);
    }
}
