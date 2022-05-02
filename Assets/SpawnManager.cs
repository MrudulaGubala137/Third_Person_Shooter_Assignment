using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Start is called before the first frame update
    
    public GameObject enemyPrefab;
   
    public void SpawnEnemies()
    {
        
        for (int i = 0; i < 10; i++)
        {

            Vector3 randomPoint = transform.position + Random.insideUnitSphere * 3;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 10f, NavMesh.AllAreas))
            {

                Instantiate(enemyPrefab, randomPoint, Quaternion.identity);

            }
            else
                i--;
         
        }

    }

    // Update is called once per frame
   
   
        
    }
    

