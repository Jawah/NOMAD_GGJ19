using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    [SerializeField] GameObject bridge1;
    [SerializeField] GameObject bridge2_1;
    [SerializeField] GameObject bridge2_2;
    [SerializeField] GameObject bridge3_1;
    [SerializeField] GameObject bridge3_2;
    [SerializeField] GameObject bridge3_3;

    [SerializeField] Text stone1;
    [SerializeField] Text stone2_1;
    [SerializeField] Text stone2_2;

    bool changeable = true;

    public void CalculateNewUIValues()
    {
        if (changeable)
        {
            //Stones

            int neededPlayersStone = (int)(GameManager.Instance.airConsoleLogic.currentPlayers / 2 + 1);
            stone1.text = neededPlayersStone.ToString();
            stone2_1.text = neededPlayersStone.ToString();
            stone2_2.text = neededPlayersStone.ToString();

            //Bridges

            int neededPlayersBridge1 = (int)(GameManager.Instance.airConsoleLogic.currentPlayers) + 1;
            bridge1.GetComponentInChildren<Text>().text = neededPlayersBridge1.ToString();
            bridge1.GetComponent<Bridge>().peopleCounter = neededPlayersBridge1;

            int neededPlayersBridge2_1 = (int)(GameManager.Instance.airConsoleLogic.currentPlayers / 1.5) + 1;
            int neededPlayersBridge2_2 = (int)(GameManager.Instance.airConsoleLogic.currentPlayers / 3 ) + 1;
            bridge2_1.GetComponentInChildren<Text>().text = neededPlayersBridge2_1.ToString();
            bridge2_1.GetComponent<Bridge>().peopleCounter = neededPlayersBridge2_1;
            bridge2_2.GetComponentInChildren<Text>().text = neededPlayersBridge2_2.ToString();
            bridge2_2.GetComponent<Bridge>().peopleCounter = neededPlayersBridge2_2;

            int neededPlayersBridge3_1 = (int)(GameManager.Instance.airConsoleLogic.currentPlayers / 5) + 1;
            int neededPlayersBridge3_2 = (int)(GameManager.Instance.airConsoleLogic.currentPlayers / 1.5) + 1;
            int neededPlayersBridge3_3 = (int)(GameManager.Instance.airConsoleLogic.currentPlayers / 5) + 1;
            bridge3_1.GetComponentInChildren<Text>().text = neededPlayersBridge3_1.ToString();
            bridge3_1.GetComponent<Bridge>().peopleCounter = neededPlayersBridge3_1;
            bridge3_2.GetComponentInChildren<Text>().text = neededPlayersBridge3_2.ToString();
            bridge3_2.GetComponent<Bridge>().peopleCounter = neededPlayersBridge3_2;
            bridge3_3.GetComponentInChildren<Text>().text = neededPlayersBridge3_3.ToString();
            bridge3_3.GetComponent<Bridge>().peopleCounter = neededPlayersBridge3_3;
        }

    }

    public IEnumerator SetChangableStatus()
    {
        if (changeable)
        {
            changeable = false;
            yield return new WaitForSeconds(4f);
            changeable = true;
        }
    }
}
