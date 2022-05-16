using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public int playerSpeed;
    public int rotateSpeed;
    CharacterController characterController;
    SpawnManagerScript spawnManager;
    AudioSource audioSource;
    public AudioClip WalkClip;
    public AudioClip ShootClip;
    // Start is called before the first frame update
    //Rigidbody rb;
    public ParticleSystem particle;
    public GameObject playerRagDoll;
    Animator animator;
    public Transform bulletPoint;
    StateMachineScript stateMachine;
    public int health = 10;
    public Slider healthSlider;
    public GameObject ragDoll;
    int enemyCount = 0;
    public Text score;
    public GameObject gameOverPanel;
    public Text wonLoseText;
    int maxAmmo = 25;
    int maxHealth = 10;
    int ammo = 25;
    public Text ammoText;
    public GameObject explosion;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //stateMachine = GameObject.FindGameObjectWithTag("Enemy").GetComponent<StateMachineScript>();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        spawnManager = GameObject.Find("SpawnPoint").GetComponent<SpawnManagerScript>();
        //rb= GetComponent<Rigidbody>();
        gameOverPanel.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {
        healthSlider.value = (float)health / 10;
        ammoText.text = ammo + "/" + maxAmmo;
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(inputX, 0f, inputZ);
        animator.SetFloat("Speed", movement.magnitude);
        characterController.SimpleMove(movement * Time.deltaTime * playerSpeed);


        // player rotation
        if (movement.magnitude > 0f)
        {
            Quaternion tempDirection = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, tempDirection, Time.deltaTime * rotateSpeed);
        }


        if (Input.GetMouseButtonDown(0) && ammo > 0)
        {
            Instantiate(explosion, this.transform.position, this.transform.rotation);
            Destroy(explosion, 1f);
            Fire();//FireMethod to fire gun
        }
        if (enemyCount == 5)
        {
            Won();
        }

    }

    private void Fire()
    {
        particle.Play();
       
        Debug.DrawRay(bulletPoint.position, transform.forward * 100, Color.red, 2f); //Draw ray in firing direction
        Ray ray = new Ray(bulletPoint.position, bulletPoint.forward);
        RaycastHit hit;
        audioSource.clip = ShootClip;
        audioSource.Play();
        print("Firing");
        ammo--;
        print("Ammo" + ammo);
        if (Physics.Raycast(ray, out hit, 100f))
        {
            print("collider hit");
            if (hit.collider.gameObject.tag == "Enemy")       //collider hit is Checking whether the tag is enemy 
            {
                GameObject tempRd = Instantiate(ragDoll, hit.collider.transform.position, hit.collider.transform.rotation);
                tempRd.transform.Find("Hips").GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * 10000);
                stateMachine=hit.collider.GetComponent<StateMachineScript>();
                print("enemy got hit");
                enemyCount++;
                score.text = "Score:" + enemyCount;
                print("enemy count"+ enemyCount);
                stateMachine.Dead();
                

            }
        }

    }
    private void Walk()
        {
        audioSource.clip = WalkClip;    //EventMethod for walk adding audio 
        audioSource.Play();
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="SpawnPoint")         
        {
            other.gameObject.SetActive(false);          
            spawnManager.SpawnEnemies();        //Spawing enemies from spawn Manager when spawnPoint is triggered
        }
       else if (other.gameObject.tag == "Ammo" && ammo < maxAmmo)
        {
            print("Ammo picked");
            ammo = Mathf.Clamp(ammo + 25, 0, maxAmmo);      //Picking ammo To load the bullets
          //  ammoText.text = "Ammo:" + ammo;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "Medical" && health < maxHealth)
        {
            print("Medical picked");
            health = Mathf.Clamp(health + 10, 0, maxHealth);        //Picking Health kit to increase player health
            //healthText.text = "Health:"+ health;
            Destroy(other.gameObject);
        }
    }


    public void GameOver()
    {
      /* Instantiate(playerRagDoll, this.transform.position, this.transform.rotation);
        tempRd.transform.Find("Hips").GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * 10000);*/
        gameOverPanel.SetActive(true);   //If player won then Setting GameOver pannel to true 
        wonLoseText.text = "You Lost";  
        
        this.gameObject.SetActive(false);

       
        print("GameOver");
    }
    public void Won()
    {
        gameOverPanel.SetActive(true);      
        wonLoseText.text = "You Won";    //If player won then Setting GameOver pannel to true 
        print("Won");
    }

}
