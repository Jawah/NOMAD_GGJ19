using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class AirConsoleLogic : MonoBehaviour
{
    public GameObject playerPrefab;

    public int currentPlayers;
    
    public Dictionary<int, PlayerController> players = new Dictionary<int, PlayerController>();

    private void Awake()
    {
        if (AirConsole.instance != null)
        {
            AirConsole.instance.onMessage += OnMessage;
            AirConsole.instance.onConnect += OnConnect;
            AirConsole.instance.onReady += OnReady;
        }
    }

    void OnConnect(int device_id)
    {
        AddNewPlayer(device_id);
        currentPlayers++;
        SendMessageToController(device_id, "Connected");
    }

    void OnReady(string code)
    {
        //Since people might be coming to the game from the AirConsole store once the game is live, 
        //I have to check for already connected devices here and cannot rely only on the OnConnect event 
        List<int> connectedDevices = AirConsole.instance.GetControllerDeviceIds();
        foreach (int deviceID in connectedDevices)
        {
            AddNewPlayer(deviceID);
        }
    }

    private void AddNewPlayer(int deviceID)
    {

        if (players.ContainsKey(deviceID))
        {
            return;
        }

        //Instantiate player prefab, store device id + player script in a dictionary
        GameObject newPlayer = Instantiate(playerPrefab, transform.position + new Vector3(Random.Range(-10f, 10f), 2f, Random.Range(-10f, 10f)), transform.rotation) as GameObject;
        players.Add(deviceID, newPlayer.GetComponent<PlayerController>());
        newPlayer.GetComponent<PlayerController>().playerId = deviceID;
    }

    void OnMessage(int deviceId, JToken data)
    {
        //When I get a message, I check if it's from any of the devices stored in my device Id dictionary
        if (players.ContainsKey(deviceId))
        {
            if (data["joystick-right"]["message"]["x"] != null || data["joystick-right"]["message"]["y"] != null )
            {
                float x = float.Parse(data["joystick-right"]["message"]["x"].ToString());
                float y = float.Parse(data["joystick-right"]["message"]["y"].ToString());
                players[deviceId].MovePlayer(x, y);
            }
            else
            {
                Debug.Log("Stop");
                players[deviceId].MovePlayer(0f, 0f);
            }
        }
    }

    private void OnDestroy()
    {
        if (AirConsole.instance != null)
        {
            AirConsole.instance.onMessage -= OnMessage;
            AirConsole.instance.onReady -= OnReady;
            AirConsole.instance.onConnect -= OnConnect;
        }
    }

    public void SendMessageToController(int deviceId, string message)
    {
        if(AirConsole.instance != null)
            AirConsole.instance.Message(deviceId, message);
    }

    public void BroadcastMessageToAllDevices()
    {
        AirConsole.instance.Broadcast("Hey everyone!");
        //logWindow.text = logWindow.text.Insert(0, "Broadcast a message. \n \n");
    }

    public int GetDeviceID()
    {
        int device_id = AirConsole.instance.GetDeviceId();
        return device_id;
    }
}
