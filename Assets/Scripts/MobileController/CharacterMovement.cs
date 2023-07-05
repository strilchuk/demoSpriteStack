using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {

    [Header("Movement Attribute")]
    public float speed = 5;
    public float rotAngle = 0;
    public float turnSmoothTime = 0.2f;
    public Vector3 forward;
    float turnnSmoothVelocity;
    [Header("Reference")]
    CharacterController controller;
    public float inputHorizontal;
    public float inputVertical;
    private Animator _myAnimator;
   
    [Header("Body parts reference")]

    public GameObject upperBody;
    public GameObject lowerBody;
    public GameObject m_input_left;
    public GameObject m_input_right;

    [Header("Platform")]
    public bool PC = true;

    private static readonly int AngleHash = Animator.StringToHash("angle");

    // Use this for initialization
	void Start () {
        controller = GetComponent<CharacterController>();
        
	}

    private void Awake()
    {
        _myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
	void Update ()
    {
        float rightValueHorizontal = m_input_right.GetComponent<MobileInputController>().Horizontal; 
        float rightValueVertical = m_input_right.GetComponent<MobileInputController>().Vertical; 
        
        float leftValueHorizontal = m_input_left.GetComponent<MobileInputController>().Horizontal; 
        float leftValueVertical = m_input_left.GetComponent<MobileInputController>().Vertical;

        float inputVertical = Input.GetAxisRaw("Vertical");
        float inputHorizontal = Input.GetAxisRaw("Horizontal");

        if(!PC)
            controller.Move(Vector3.up * leftValueVertical * Time.deltaTime * speed+ Vector3.right * leftValueHorizontal * Time.deltaTime * speed);
        else
            controller.Move(Vector3.up * inputVertical * Time.deltaTime * speed + Vector3.right * inputHorizontal * Time.deltaTime * speed);

        Vector3 from = new Vector3(0f,0f,1f);
        Vector3 to = new Vector3(rightValueHorizontal,0f, rightValueVertical);
        //forward = lowerBody.transform.forward;
     

        if (rightValueHorizontal != 0 && rightValueVertical != 0)
        {
            float angle = Vector3.SignedAngle(from, to, Vector3.up);

            float angle2 = Mathf.Atan2(rightValueHorizontal, rightValueVertical) * Mathf.Rad2Deg;
            if (angle2 < 0)
            {
                angle2 += 360f;
            }

            angle2 += 180;
            
            // transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            
            rotAngle = angle;
            // transform.GetComponent<SpriteStackEntity>().rotation.z = angle; << -- FOR SPRITE STACK OBJECT
            Debug.Log($"V: {rightValueVertical} H: {rightValueHorizontal} A: {angle} A2: {angle2}");
            
            _myAnimator.SetInteger(AngleHash, (int) angle2);

           // if (angle > 60 || angle <-60)
            //{
             //   float targetRotation = Mathf.Atan2(rightValueHorizontal, rightValueVertical) * Mathf.Rad2Deg;
               //-->transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, angle, ref turnnSmoothVelocity, turnSmoothTime); <--
            // }

          //  float targetRotationForUpperBody = Mathf.Atan2(rightValueHorizontal, rightValueVertical) * Mathf.Rad2Deg;
           // transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(upperBody.transform.eulerAngles.y, targetRotationForUpperBody, ref turnnSmoothVelocity, turnSmoothTime);

           // transform.eulerAngles = new Vector3(0f, angle, 0f);

        }
        



    }

}
