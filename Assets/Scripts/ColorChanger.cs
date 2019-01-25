using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorChanger : MonoBehaviour
{
    Image image;

    public GameObject go;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void ChangeColor()
    {
        Color newColor = new Color(Random.Range(0f,1f), Random.Range(0f,1f), Random.Range(0f,1f));
        go.GetComponent<Renderer>().material.color = newColor;
    }
}
