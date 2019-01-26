using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class GameManager : MonoBehaviour
{
    public AirConsoleLogic airConsoleLogic;

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

        airConsoleLogic = GameObject.FindGameObjectWithTag(Tags.AIR_CONSOLE_LOGIC).GetComponent<AirConsoleLogic>();
    }

}
