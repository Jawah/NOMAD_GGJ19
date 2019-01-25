using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float moveSpeed;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float horizontalValue = Input.GetAxis("Horizontal");
        float verticalValue = Input.GetAxis("Vertical");

        if(verticalValue != 0 || horizontalValue != 0)
        {
            transform.Translate(new Vector3(horizontalValue, 0f, verticalValue) * moveSpeed, Space.World);
            //rb.AddForce(new Vector3(horizontalValue, verticalValue, 0f) * moveSpeed);
        }
    }
}
