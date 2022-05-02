using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int playerSpeed;
    public int rotateSpeed;
    CharacterController characterController;
   SpawnManager spawnManager;
    // Start is called before the first frame update
    //Rigidbody rb;
    Animator animator;
    public Transform bulletPoint;
    StateMachineScript stateMachine;
    void Start()
    {
        stateMachine=GameObject.Find("Enemy"). GetComponent<StateMachineScript>();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        spawnManager = GameObject.Find("SpawnPoint").GetComponent<SpawnManager>();
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
        transform.Rotate(Vector3.up*inputX*Time.deltaTime*rotateSpeed);   //Rotating the player
     if(inputZ!=0)
        {
            characterController.Move(transform.forward*inputZ*Time.deltaTime); //For plyer movement
        }
     if(Input.GetMouseButtonDown(0))
        {
            Fire();//FireMethod to fire gun
        }
        
    }
    private void Fire()
    {
        Debug.DrawRay(bulletPoint.position,transform.forward *100, Color.red, 2f); //Draw ray in firing direction
      Ray ray=new Ray(bulletPoint.position,bulletPoint.forward);
        RaycastHit hit;
        print("Firing");
        if(Physics.Raycast(ray, out hit,100f))
        {
            if(hit.collider)       //collider hit is Checking whether the tag is enemy 
            {
                if (gameObject.tag == "Enemy")
                {
                    stateMachine.Dead();
                }
            }
        }

    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="SpawnPoint")
        {
            spawnManager.SpawnEnemies();
        }
    }
}
