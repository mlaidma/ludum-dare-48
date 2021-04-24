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
        float xVelocity = Input.GetAxisRaw("Horizontal") * moveSpeed;

        rigidBody.velocity = new Vector2(xVelocity, rigidBody.velocity.y);

    }
}
