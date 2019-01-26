using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bridge : MonoBehaviour
{
    [SerializeField] int peopleCounter;
    [SerializeField] Text counterText;

    private void Start()
    {
        counterText.text = peopleCounter.ToString();
    }

    public void DecreaseCount()
    {
        peopleCounter--;
        counterText.text = peopleCounter.ToString();

        if(peopleCounter == 0)
        {
            gameObject.SetActive(false);
        }
    }
}
