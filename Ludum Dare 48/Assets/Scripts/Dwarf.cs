using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dwarf : MonoBehaviour
{

    [SerializeField] private LayerMask tilemapLayerMask;

    [SerializeField] private float moveSpeed = 1.0f;
    [SerializeField] private float maxJumpForce = 250f;

    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;
    private CapsuleCollider2D capsuleCollider2D;

    private float xVelocity = 0f;
    private float jumpForce = 0f;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();    
    }

    // Update is called once per frame
    void Update()
    {
        GetPlayerInput();
    }

    void FixedUpdate() {
        Move();    
    }

    void GetPlayerInput()
    {
        xVelocity = Input.GetAxisRaw("Horizontal") * moveSpeed;

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
}
