using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomCreateManager : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private Transform parent;
    // Update is called once per frame
    void Update()
    {

    }

    public void OnSeverCreate()
    {
        Instantiate(prefab, parent);
    }
}
