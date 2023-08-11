using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PersonalCamera : MonoBehaviour
{
    [SerializeField] private Camera main_camera;
    Vector3[] camera_pos = new Vector3[4];

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

    public void Add_Camera_pos()
    {
        
        for (int i = 0; i < camera_pos.Length; i++)
        {
            Vector3 temp = main_camera.transform.position;
            switch (i)
            {
                case 0:
                    break;
                case 1:
                    temp.z = main_camera.transform.position.z + 17;
                    camera_pos[i] = temp;
                    break;
                case 2:
                    temp.x = main_camera.transform.position.x + 17;
                    break;
                case 3:
                    temp.x = main_camera.transform.position.x + 17;
                    temp.z = main_camera.transform.position.z + 17;
                    break;
            }
            camera_pos[i] = temp;
        }
    }

    void Set_Camera_Pos()
    {
        switch (PhotonNetwork.LocalPlayer.ActorNumber)
        {
            case 1:
                break;
            case 2:
                main_camera.transform.position += Vector3.forward *17;
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
        main_camera.transform.position = camera_pos[i];
    }
}
