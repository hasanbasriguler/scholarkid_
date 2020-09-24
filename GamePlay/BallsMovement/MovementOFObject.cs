using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementOFObject : MonoBehaviour
{
    [SerializeField]
    float speed;
        
    Vector2 direction;

   
    
    
    //Rigidbody2D rb;






    void Start()
    {
        
        direction = Vector2.one.normalized;
        
        
    }

    
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);

                
        
   
    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.name == "Up")
        {
            direction.y = -direction.y;

        }
        else if (collision.gameObject.name == "Down")
        {
            direction.y = -direction.y;

        }

        else if (collision.gameObject.name == "Left")
        {
            direction.x = -direction.x;

        }

        else if (collision.gameObject.name == "Right")
        {
            direction.x = -direction.x;

        }


        else if (collision.tag == "ballframe")
        {
            direction.x = -direction.x;
            direction.y = -direction.y;

        }

        else if (collision.tag == "reburn")
        {       
            transform.position = new Vector2(Random.Range(-7.80f, 7.25f), Random.Range(-3.0f, 1.80f));

        }


    }
    

}





