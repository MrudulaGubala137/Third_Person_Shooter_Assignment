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
    public enum STATE { LOOKFOR, GOTO, ATTACK, DEAD };
    public STATE currentState = STATE.LOOKFOR;

    // Update is called once per frame
    IEnumerator Start()
    {

        agent = GetComponent<NavMeshAgent>();
        if (target == null)
        {
            target = GameObject.Find("Player").GetComponent<Transform>();
            agent.SetDestination(target.position);
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
    public void LookFor()
    {
        float randValueX = transform.position.x + Random.Range(-5f, 5f);
        float randValueZ = transform.position.z + Random.Range(-5f, 5f);
        float ValueY = Terrain.activeTerrain.SampleHeight(new Vector3(randValueX, 0f, randValueZ));
        Vector3 destination = new Vector3(randValueX, ValueY, randValueZ);

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
        return Vector3.Distance(target.position, this.transform.position);
    }

    public void Goto()
    {
        anim.SetTrigger("isRunning");
        if (PlayerDistance() > attackDistance)
        {
            Debug.Log("Attacking");
            //transform.position = Vector3.MoveTowards(transform.position, target.transform.position * Time.deltaTime);
            agent.SetDestination(target.position);
        }
        else
        {
            currentState = STATE.ATTACK;
        }
        print("This is GotoState");
    }
    public void Attack()
    {
        anim.SetTrigger("isAttacking");
        if (PlayerDistance() > attackDistance)
        {
            currentState = STATE.GOTO;
        }
        else if (PlayerDistance() > gotoDistance)
        {
            currentState = STATE.LOOKFOR;
        }
        print("This is AttackState");
    }
    public void Dead()
    {
        anim.SetTrigger("isDead");
        gameObject.SetActive(false);
        // this.gameObject.SetActive(false);
        print("Enemy Dead");
    }


}
