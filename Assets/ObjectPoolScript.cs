using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolScript : MonoBehaviour
{
    // Start is called before the first frame update
    public static ObjectPoolScript instance;
    public List<GameObject> pool = new List<GameObject>();
    public List<PoolObject> poolItems = new List<PoolObject>();
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        return;
    }
    void Start()
    {
        AddToPool();

    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            GameObject temp = GetObjectsFromPool("Enemy");
                temp.gameObject.SetActive(true);    
        }
    }

    // Update is called once per frame
    public void AddToPool()
    {
        foreach(PoolObject item in poolItems)
        {
            for(int i=0; i<item.amount; i++)
            {
                GameObject temp = Instantiate(item.prefab);
                
                pool.Add(temp);
                temp.SetActive(false);
            }
        }
    }
    public GameObject GetObjectsFromPool(string tagname)
    {
        foreach (GameObject item in pool)
        {
            if (item.gameObject.tag == tagname && !item.activeInHierarchy)
            {
                //item.SetActive(true);
                
                Debug.Log("Item = "+item);
                return item;
                // pool[i].gameObject.SetActive(true);
            }

        }
        return null;

    }
    [System.Serializable]
    public class PoolObject
    {
        public GameObject prefab;
        public string name;
        public int amount;
    }
}
