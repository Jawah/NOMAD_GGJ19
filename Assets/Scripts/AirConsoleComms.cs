using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class AirConsoleComms : MonoBehaviour
{
    private void Awake()
    {
        AirConsole.instance.onMessage += OnMessage;
    }

    private void OnMessage(int fromDeviceID, JToken data)
    {
        // Debug.Log("Message from: " + fromDeviceID + ", data: " + data);

        if (data["joystick-right"] == null) return;
        if (data["joystick-right"]["message"]["x"] != null || data["joystick-right"]["message"]["y"] != null)
        {
            Debug.Log(data["joystick-right"]["message"]["x"].ToString());
            Debug.Log(data["joystick-right"]["message"]["y"].ToString());
        }
    }
    
    private void OnDestroy()
    {
        if (AirConsole.instance != null)
            AirConsole.instance.onMessage -= OnMessage;
    }
}
