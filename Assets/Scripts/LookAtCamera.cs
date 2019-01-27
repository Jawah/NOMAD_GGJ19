using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LookAtCamera : MonoBehaviour
{
    Camera cam;

    private void Awake()
    {
        cam = GetComponent<Canvas>().worldCamera;
    }

    private void Update()
    {
        transform.LookAt(cam.transform);
    }
}
