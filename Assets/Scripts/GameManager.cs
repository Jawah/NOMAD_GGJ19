using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;

    public Dictionary<int, PlayerController> players = new Dictionary<int, PlayerController>();

    // Singleton GameManager Instance
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }

        AirConsole.instance.onMessage += OnMessage;
        AirConsole.instance.onConnect += OnConnect;
        AirConsole.instance.onReady += OnReady;
    }

    void OnConnect(int device_id)
    {
        AddNewPlayer(device_id);
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
        GameObject newPlayer = Instantiate(playerPrefab, transform.position + new Vector3(Random.Range(-10f,10f),2f, Random.Range(-10f, 10f)), transform.rotation) as GameObject;
        players.Add(deviceID, newPlayer.GetComponent<PlayerController>());
    }

    void OnMessage(int deviceId, JToken data)
    {
        //When I get a message, I check if it's from any of the devices stored in my device Id dictionary
        if (players.ContainsKey(deviceId))
        {
            if (data["joystick-right"]["message"]["x"] != null || data["joystick-right"]["message"]["y"] != null)
            {
                Debug.Log(players[deviceId]);
                players[deviceId].MovePlayer((float)data["joystick-right"]["message"]["x"], (float)data["joystick-right"]["message"]["y"]);
            }

            Debug.Log(data["joystick-right"]["message"]["x"].ToString());
            Debug.Log(data["joystick-right"]["message"]["y"].ToString());
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
}
