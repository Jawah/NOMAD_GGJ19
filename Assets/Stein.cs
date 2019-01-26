using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Stein : MonoBehaviour
{

    public GameObject middlePoint;
    public GameObject endPoint;

    public bool middlePointReached = false;
    public bool endPointReached = false;

    public int amountOfPlayers;
    public float speed;

    private void FixedUpdate()
    {
        if (amountOfPlayers == 1 && !middlePointReached)
        {
            transform.position = Vector3.MoveTowards(transform.position, middlePoint.transform.position, speed);
        }

        if (Vector3.Distance(transform.position, middlePoint.transform.position) < 0.1f && !middlePointReached)
        {
            transform.position = middlePoint.transform.position;
            middlePointReached = true;
        }

        if (middlePointReached && !endPointReached)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPoint.transform.position, speed);
        }

        if (Vector3.Distance(transform.position, endPoint.transform.position) < 0.1f && !endPointReached)
        {
            transform.position = endPoint.transform.position;
            endPointReached = true;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            amountOfPlayers++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            amountOfPlayers--;
        }
    }

}
