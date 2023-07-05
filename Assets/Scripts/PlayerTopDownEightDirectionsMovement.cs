using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTopDownEightDirectionsMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    public float movementSpeed;
    private Vector2 movementInput;
    private static readonly int MovementX = Animator.StringToHash("MovementX");
    private static readonly int MovementY = Animator.StringToHash("MovementY");

    [SerializeField] private GameObject m_input_left;
    [SerializeField] private GameObject m_input_right;
    
    [Header("Platform")]
    public bool PC = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Move();
        Animate();
    }

    private void Move()
    {
        float horizontal = 0;
        float vertical = 0;
        
        if (PC)
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");            
        }
        else
        {
            float rightValueHorizontal = m_input_right.GetComponent<MobileInputController>().Horizontal; 
            float rightValueVertical = m_input_right.GetComponent<MobileInputController>().Vertical; 
        
            horizontal = m_input_left.GetComponent<MobileInputController>().Horizontal; 
            vertical = m_input_left.GetComponent<MobileInputController>().Vertical;
        }

        if (horizontal == 0 && vertical == 0)
        {
            rb.velocity = new Vector2(0, 0);
            return;
        }

        movementInput = new Vector2(horizontal, vertical);
        rb.velocity = movementInput * movementSpeed * Time.deltaTime;
    }

    private void Animate()
    {
        anim.SetFloat(MovementX, movementInput.x);
        anim.SetFloat(MovementY, movementInput.y);
    }
}
