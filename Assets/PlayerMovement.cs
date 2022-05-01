using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int playerSpeed;
    public int rotateSpeed;
    CharacterController characterController;
    // Start is called before the first frame update
    //Rigidbody rb;
    Animator animator;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        //rb= GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxis("Horizontal")*playerSpeed;
        float inputZ = Input.GetAxis("Vertical")*playerSpeed;
        Vector3 movement=new Vector3(inputX,0,inputZ);
        // transform.Translate(inputX,0f,inputZ);
        animator.SetFloat("Speed", movement.magnitude);
        transform.Rotate(Vector3.up*inputX*Time.deltaTime*rotateSpeed);
     if(inputZ!=0)
        {
            characterController.Move(transform.forward*inputZ*Time.deltaTime);
        }
        
    }
}
