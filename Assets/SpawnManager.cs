using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Start is called before the first frame update

    // public GameObject enemyPrefab;

    public void SpawnEnemies()
    {
        GameObject tempEnemy = ObjectPoolScript.instance.GetObjectsFromPool("Enemy");
        Debug.Log("tempEnemy:" +tempEnemy);

        Vector3 randomPoint = transform.position + Random.insideUnitSphere * 10;
        NavMeshHit hit;
        for (int i = 0; i < 5; i++)
        {
           
            if (NavMesh.SamplePosition(randomPoint, out hit, 10f, NavMesh.AllAreas))
            {
                if (tempEnemy != null && tempEnemy.tag == "Enemy")
                {
                    Vector3 result = hit.position;
                    print("enemy spawned");
                    tempEnemy.GetComponent<Transform>().position = result;
                    tempEnemy.SetActive(true);
                   // Debug.Log(tempEnemy.activeInHierarchy?"true":"false");
                    //print("enemy is true");
                   // tempEnemy.transform.position = randomPoint;
                     //tempEnemy.SetActive(true);
                }
                //Instantiate(enemyPrefab, randomPoint, Quaternion.identity);

            }
            else
                i--;
         
        }

    }
    

    // Update is called once per frame



}
    

