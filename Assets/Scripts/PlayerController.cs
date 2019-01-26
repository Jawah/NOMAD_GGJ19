using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Renderer cloth1;
    [SerializeField] Renderer cloth2;
    [SerializeField] Renderer cloth3;

    private Rigidbody rb;

    public int playerId;

    [SerializeField] private float moveSpeed;

    private float horizontalValue;
    private float verticalValue;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        cloth1.material.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        cloth2.material.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        cloth3.material.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    }

    public void MovePlayer(float x, float y)
    {
            horizontalValue = x;
            verticalValue = -y;
    }

    private void FixedUpdate()
    {
        Vector3 moveDirection = new Vector3(horizontalValue, 0f, verticalValue);

        if (verticalValue != 0 || horizontalValue != 0)
        {
            rb.velocity = moveDirection * moveSpeed;
        }

        transform.LookAt(transform.position + moveDirection);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.DEATH_AREA))
        {
            Debug.Log(playerId);
            GameManager.Instance.airConsoleLogic.SendMessageToController(
            playerId,
            "Dead"
            );
            gameObject.SetActive(false);
        }
        else if (other.CompareTag(Tags.WIN_AREA))
        {
            GameManager.Instance.airConsoleLogic.SendMessageToController(
            playerId,
            "Win"
            );
        }
    }
}
