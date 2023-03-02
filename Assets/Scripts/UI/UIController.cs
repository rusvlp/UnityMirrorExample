using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject serverMenu;
    public GameObject devicesList_window;
    public GameObject sessions_window;

   


    // Update is called once per frame
    void Update()
    {

    }

    public void CloseWindow()
    {
        serverMenu.SetActive(true);
        devicesList_window.SetActive(false);
        sessions_window.SetActive(false);
    }

    public void OpenDevicesList()
    {
        serverMenu.SetActive(false);
        devicesList_window.SetActive(true);
        sessions_window.SetActive(false);
    }

    public void OpenSessions()
    {
        serverMenu.SetActive(false);
        devicesList_window.SetActive(false);
        sessions_window.SetActive(true);
    }
}
