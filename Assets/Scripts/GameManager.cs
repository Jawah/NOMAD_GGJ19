using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public AirConsoleLogic airConsoleLogic;
    public UIHandler uiHandler;

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
        uiHandler = GameObject.Find("UIHandler").GetComponent<UIHandler>();
    }

    public void StartGame()
    {
        GameObject.Find("Tiles").GetComponent<TileMover>().moveSpeed = 0.4f;
    }

    public void StartCutscene()
    {
        SceneManager.LoadScene("EndSequence");
    }
}
