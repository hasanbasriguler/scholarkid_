using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomObject : MonoBehaviour
{

  

    void Start()
    {
        float x = Random.Range(-7.80f, 7.25f);
        float y = Random.Range(-3.00f, 1.80f);
        float z = transform.position.z;

        transform.position = new Vector3(x, y, z);
  
            
    }

   
}
