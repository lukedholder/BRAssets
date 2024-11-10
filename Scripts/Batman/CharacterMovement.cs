using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Main Character Movement speed and controls

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of the character
    private Vector2 movement;    // Movement vector

    private Rigidbody2D rb;      // Reference to Rigidbody2D component
    private bool isFacingRight = true; // Track whether the player is facing right
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Get the horizontal and vertical input (WASD or arrow keys)
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        // Create movement vector based on input
        movement = new Vector2(moveX, moveY).normalized;

        // Call Flip() to handle character direction
        if (moveX != 0)
        {
            Flip(moveX);
        }
    }

    void FixedUpdate()
    {
        // Move the character by setting the Rigidbody2D velocity
        rb.velocity = movement * moveSpeed;
        animator.SetFloat("xVelocity", Math.Abs(rb.velocity.x));
        //animator.SetFloat("yVelocity", Math.Abs(rb.velocity.y));
    }

    // Flip the character sprite based on movement direction
    void Flip(float horizontal)
    {
        if (horizontal > 0 && !isFacingRight)
        {
            // Moving right but currently facing left, so flip
            FlipSprite();
        }
        else if (horizontal < 0 && isFacingRight)
        {
            // Moving left but currently facing right, so flip
            FlipSprite();
        }
    }

    // Handles the actual flipping of the sprite
    void FlipSprite()
    {
        isFacingRight = !isFacingRight; // Toggle the facing direction

        // Multiply the player's x local scale by -1 to flip
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
