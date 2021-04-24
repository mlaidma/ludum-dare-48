using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dwarf : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 1.0f;
    [SerializeField] private float maxJumpForce = 250f;

    private Rigidbody2D rigidBody;


    private float xVelocity = 0f;
    private float jumpForce = 0f;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();        
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
        
        if (Input.GetButtonDown("Jump"))
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
}
