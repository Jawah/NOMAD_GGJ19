using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMover : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    
    private void FixedUpdate()
    {
        transform.Translate(Vector3.back * moveSpeed);
    }
}
