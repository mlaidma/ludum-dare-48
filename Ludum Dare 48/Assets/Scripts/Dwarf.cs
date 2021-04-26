using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dwarf : MonoBehaviour
{

    [SerializeField] private LayerMask tilemapLayerMask;

    [SerializeField] private float moveSpeed = 1.0f;
    [SerializeField] private float maxJumpForce = 250f;

    [SerializeField] PlayerItem staffPrefab;
    [SerializeField] PlayerItem hammerPrefab;

    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D capsuleCollider2D;

    private Transform itemCoord;
    private PlayerItem activeItem; 

    private Animator animator;

    private float xVelocity = 0f;
    private float jumpForce = 0f;

    private const float maxGems = 100.0f;
    private float playerGems = 20f;

    public float PlayerGems {
        get {return playerGems;} 
    }

    public int PlayerDepth {
        get { return Mathf.FloorToInt(transform.position.y);}
    }

    private bool gameOn = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        capsuleCollider2D = GetComponent<BoxCollider2D>();    
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        itemCoord = transform.Find("Item");

        SetHammer();

        StartCoroutine(SpendGems());
    }

    // Update is called once per frame
    void Update()
    {
        GetPlayerInput();
        activeItem.SetLight(playerGems / maxGems);

    }

    void FixedUpdate() {

        if(gameOn)
        {

            Move();
        }
    }

    IEnumerator SpendGems()
    {
        while(true)
        {   
            if(gameOn)
            {
                if (playerGems != 0)
                {
                    playerGems -= 2;
                }
            }
            yield return new WaitForSeconds(1f);
        }
    }

    void GetPlayerInput()
    {

        // Item
        if(Input.GetKeyDown(KeyCode.E))
        {
            SetStaff();
        }
        
        if(Input.GetKeyDown(KeyCode.Q))
        {
            SetHammer();
        }

        // Movement
        xVelocity = Input.GetAxisRaw("Horizontal") * moveSpeed;
        float absVelocity = Mathf.Abs(xVelocity);

        animator.SetFloat("playerVelocity", absVelocity);

        if(xVelocity < 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
        
        if (isGrounded() && Input.GetButtonDown("Jump"))
        {
            Debug.Log("Jump");
            jumpForce = maxJumpForce;
        }
    }

    private void Move()
    {
        rigidBody.velocity = new Vector2(xVelocity, rigidBody.velocity.y);

        if(jumpForce != 0f)
        {
            rigidBody.AddForce(new Vector2(0, jumpForce));
            jumpForce = 0f;
        }
    }

    private bool isGrounded()
    {
        float rayExtension = 0.1f;
        RaycastHit2D hit = Physics2D.Raycast(capsuleCollider2D.bounds.center, Vector2.down, capsuleCollider2D.bounds.extents.y + rayExtension, tilemapLayerMask);

        return hit.collider != null;
    }

    private void SetHammer()
    {
        activeItem = hammerPrefab;
        hammerPrefab.gameObject.SetActive(true);
        staffPrefab.gameObject.SetActive(false);
    }

    private void SetStaff()
    {
        activeItem = staffPrefab;
        hammerPrefab.gameObject.SetActive(false);
        staffPrefab.gameObject.SetActive(true);
    }


    public void GameOn(bool value)
    {
        gameOn = value;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        
        if(other.tag == "gems")
        {
            playerGems += new System.Random().Next(3, 12);
            Destroy(other.gameObject);
        }
    }

}
