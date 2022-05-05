using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnManagerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void SpawnEnemies()
    {
        Vector3 randomPoint=transform.position+Random.insideUnitSphere*30;
        NavMeshHit hit;
        for (int i = 0; i < 5; i++)
        {
            if (NavMesh.SamplePosition(randomPoint, out hit, 10f, NavMesh.AllAreas))
            {

                GameObject tempEnemy = ObjectPoolScript.instance.GetObjectsFromPool("Enemy");  //Getting enemy prefabs from pool
                tempEnemy.transform.position = hit.position;
                tempEnemy.SetActive(true);
                print("enemy is true");

            }
            else
                i--;
        }
    }
}
