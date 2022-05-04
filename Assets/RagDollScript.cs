using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagDollScript : MonoBehaviour
{
    // Start is called before the first frame update
    float time;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time=time+Time.deltaTime;
        if(time>3f)
        {
            Destroy(gameObject);
            //this.gameObject.SetActive(false);
            time=0;

        }
        
    }
    
}
