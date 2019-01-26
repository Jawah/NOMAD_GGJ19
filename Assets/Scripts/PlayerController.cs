using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //[SerializeField] Renderer cloth1;
    //[SerializeField] Renderer cloth2;
    //[SerializeField] Renderer cloth3;

    private Rigidbody rb;

    public int playerId;

    [SerializeField] private float moveSpeed;

    private float horizontalValue;
    private float verticalValue;

    private bool bridgeCollision = false;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        //cloth1.material.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        //cloth2.material.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        //cloth3.material.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    }

    public void MovePlayer(float x, float y)
    {
            horizontalValue = x;
            verticalValue = -y;
    }

    private void FixedUpdate()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");

        rb.AddForce(Physics.gravity * 2, ForceMode.Acceleration);

        if(inputX != 0|| inputZ != 0)
        {
            float y = rb.velocity.y;

            rb.velocity = new Vector3(inputX * moveSpeed, y, inputZ * moveSpeed);

            //transform.LookAt(transform.position + new Vector3(inputX * moveSpeed, y, inputZ * moveSpeed));
            transform.rotation = Quaternion.LookRotation(new Vector3(inputX, 0f, inputZ));
        }

        /*

        Vector3 moveDirection = new Vector3(horizontalValue, 0f, verticalValue);

        if (verticalValue != 0 || horizontalValue != 0)
        {
            rb.velocity = moveDirection * moveSpeed;
        }

    */

        //transform.LookAt(new Vector3(0f, transform.position.y, 0f) + new Vector3(0f, transform.position.y, 0f));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.DEATH_AREA))
        {
            Debug.Log(playerId);
            if(GameManager.Instance.airConsoleLogic != null)
            {
                // GameManager.Instance.airConsoleLogic.Message(playerId, { vibrate: 1000 });
                GameManager.Instance.airConsoleLogic.SendMessageToController(
                playerId,
                "Dead"
                );
            }
            gameObject.SetActive(false);
        }
        else if (other.CompareTag(Tags.WIN_AREA))
        {
            if (GameManager.Instance.airConsoleLogic != null)
            {
                // GameManager.Instance.airConsoleLogic.Message(playerId, { vibrate: 1000 });
                GameManager.Instance.airConsoleLogic.SendMessageToController(
                playerId,
                "Win"
                );
            }
        }
        else if (other.CompareTag(Tags.BRIDGE))
        {
            if (!bridgeCollision)
            {
                other.gameObject.GetComponent<Bridge>().DecreaseCount();
                bridgeCollision = true;
            }

            StartCoroutine(CollisionTimer());
        }
    }

    IEnumerator CollisionTimer()
    {
        yield return new WaitForSeconds(2f);
        bridgeCollision = false;
    }
}
