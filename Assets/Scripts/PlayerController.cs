using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Renderer colorRenderer;

    public GameObject deathAnim;
    public GameObject spawnAnim;

    public AudioSource audioSource;
    //public AudioClip deathSound;
    
    public Animator anim;

    [SerializeField] private Text playerNumberText;

    private Rigidbody rb;

    public int playerId;

    [SerializeField] private float moveSpeed;

    private float horizontalValue;
    private float verticalValue;

    private bool bridgeCollision = false;

    private void Awake()
    {
        audioSource.GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        //colorRenderer = GameObject.FindGameObjectWithTag("CharacterMesh").GetComponent<Renderer>();
    }

    private void Start()
    {       
        spawnAnim.SetActive(true);

        //GameManager.Instance.airConsoleLogic.currentPlayers++;
        playerNumberText.text = playerId.ToString();
        
        colorRenderer.materials[2].color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        colorRenderer.materials[3].color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
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

        if(inputX != 0 || inputZ != 0)
        {
            anim.SetBool("fast", true);
            
            float y = rb.velocity.y;

            rb.velocity = new Vector3(inputX * moveSpeed, y, inputZ * moveSpeed);
            
            transform.rotation = Quaternion.LookRotation(new Vector3(inputX, 0f, inputZ));
        }
        else
        {
            anim.SetBool("fast", false);
        }
        
        Vector3 moveDirection = new Vector3(horizontalValue, 0f, verticalValue);

        if (verticalValue != 0 || horizontalValue != 0)
        {
            anim.SetBool("fast", true);
            
            float y = rb.velocity.y;

            rb.velocity = new Vector3(horizontalValue * moveSpeed, y, verticalValue * moveSpeed);

            transform.rotation = Quaternion.LookRotation(new Vector3(horizontalValue, 0f, verticalValue));
        }
        else
        {
            anim.SetBool("fast", false);
        }
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
            GameManager.Instance.airConsoleLogic.currentPlayers--;

            GameManager.Instance.uiHandler.CalculateNewUIValues();

            //audioSource.clip = deathSound;
            //audioSource.Play();
            Instantiate(deathAnim, gameObject.transform.position, Quaternion.Euler(-90, 0, 0));
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

            StartCoroutine(GameManager.Instance.uiHandler.SetChangableStatus());
            StartCoroutine(CollisionTimer());
        }
    }

    IEnumerator CollisionTimer()
    {
        yield return new WaitForSeconds(2f);
        bridgeCollision = false;
    }
}
