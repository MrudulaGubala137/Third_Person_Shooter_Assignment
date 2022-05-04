using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateMachineScript : MonoBehaviour
{
    // Start is called before the first frame update
    // Start is called before the first frame update

    NavMeshAgent agent;
    public Transform target;
    public float gotoDistance;
    public float attackDistance;
    public Animator anim;
    PlayerMovement playerMovement;
    float attackTime = 3;
    float currentTime = 3;
    bool isGameOver = false;
  
    //int maxHealth = 10;
    RagDollScript ragDollScript;
   // public GameObject enemyRagDoll;
    public enum STATE { LOOKFOR, GOTO, ATTACK, DEAD };
    public STATE currentState = STATE.LOOKFOR;
    SpawnManager spawnManager;
    // Update is called once per frame
    IEnumerator Start()
    {
        currentTime = attackTime;
        agent = GetComponent<NavMeshAgent>();
       //spawnManager =GameObject.Find("SpawnPoint").GetComponent<SpawnManager>();
        if (target == null && isGameOver==false)
        {
            target = GameObject.Find("Player").GetComponent<Transform>();
            agent.SetDestination(target.position);      // Targeting player position to enemies if game is not true
            //target = GameObject.Find("Player").GetComponent<Transform>();
            Debug.Log(target);       
        }
        if (target != null)
        {
            playerMovement = target.GetComponent<PlayerMovement>();
        }

        while (true)
        {
            switch (currentState)
            {
                case STATE.LOOKFOR:
                    LookFor();

                    break;
                case STATE.GOTO:
                    Goto();
                    break;
                case STATE.ATTACK:
                    Attack();
                    break;
                case STATE.DEAD:
                    Dead();
                    break;
                default:
                    break;
            }
            yield return null;
        }

    }
    public void LookFor()  //Look for method from LookFor state
    {
        TurnOffAllAnim();
        anim.SetTrigger("isWalking");
        float randValueX = transform.position.x + Random.Range(-5f, 5f);
        float randValueZ = transform.position.z + Random.Range(-5f, 5f);
        float ValueY = Terrain.activeTerrain.SampleHeight(new Vector3(randValueX, 0f, randValueZ));
        Vector3 destination = new Vector3(randValueX, ValueY, randValueZ);  //Wander for enemy
        agent.SetDestination(destination);
        
        if (PlayerDistance() < gotoDistance)
        {
            currentState = STATE.GOTO;
        }
        /* else if(Vector3.Distance(target.transform.position, this.transform.position)<attackDistance)
         {
             currentState=STATE.ATTACK;
         }*/
        print("This is LookForState");
    }

    private float PlayerDistance()
    {
        return Vector3.Distance(target.position, this.transform.position);  //Calculating the player distance from enemy
    }

    public void Goto()    // Goto method called from Goto state
    {
        TurnOffAllAnim();
        anim.SetTrigger("isRunning");
        if (PlayerDistance() > attackDistance)
        {
            Debug.Log("Attacking");
            //transform.position = Vector3.MoveTowards(transform.position, target.transform.position * Time.deltaTime);
            agent.SetDestination(target.position);
        }
       /* else if(PlayerDistance() > gotoDistance)
        {
            currentState=STATE.LOOKFOR;
        }*/
        else
        {
            currentState = STATE.ATTACK;
        }
        print("This is GotoState");
    }
    public void Attack()    //Attack method called from Attack State 
    {

        currentTime = currentTime - Time.deltaTime;
        if (currentTime <= 0f&& playerMovement.health>0)
        {
            playerMovement.health--;
            // playerMovement.healthText.text="Health:"+playerMovement.health;
            Debug.Log(playerMovement.health);
            currentTime = attackTime;
        }
        if (playerMovement.health == 0)
        {
            isGameOver = true;
            TurnOffAllAnim();
            playerMovement.GameOver();
        }
        TurnOffAllAnim();
        anim.SetTrigger("isAttacking");
       /* if (PlayerDistance() > attackDistance)
        {
            currentState = STATE.GOTO;
        }*/

         if (PlayerDistance() > gotoDistance)
        {
            currentState = STATE.LOOKFOR;
        }
        print("This is AttackState");
    }
    public void Dead()  //Dead method called from death state
    {
        TurnOffAllAnim();
        anim.SetTrigger("isDead");
        print("I am dead.");
        //gameObject.transform.position = ragDoll.transform.position;
        /* GameObject tempRd = (ObjectPoolScript.instance.GetObjectsFromPool("RagDoll"));
         tempRd.transform.position = this.transform.position; 
         //Instantiate(ragDoll, this.transform.position, this.transform.rotation);
         tempRd.transform.Find("Hips").GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * 10000);
         tempRd.SetActive(true);*/
       this.gameObject.SetActive(false);
        print("Enemy Dead");
    }
    
    public void TurnOffAllAnim()
    {
        anim.ResetTrigger("isAttacking");
        anim.ResetTrigger("isWalking");
        anim.ResetTrigger("isRunning");
        anim.ResetTrigger("isDead");
    }
    

    
}
