using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dwarf : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 1.0f;

    private Rigidbody2D rigidBody; 

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), 0).normalized;
        Vector2 moveVelocity = moveInput * moveSpeed;

        rigidBody.velocity = moveVelocity;

    }
}
