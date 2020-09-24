using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{

    [SerializeField]
    private Sprite[] healthbar = new Sprite[4];

    [SerializeField]
    private GameObject healthbarobject;
    
    

    public void healthcontrol(int lasthealth)
    {
        switch (lasthealth)
        {
            case 4:
                healthbarobject.transform.GetComponent<Image>().sprite = healthbar[3];
                break;

            case 3:
                healthbarobject.transform.GetComponent<Image>().sprite = healthbar[2];
                break;

            case 2:
                healthbarobject.transform.GetComponent<Image>().sprite = healthbar[1];
                break;

            case 1:
                healthbarobject.transform.GetComponent<Image>().sprite = healthbar[0];
                break;

        }



    }

    
 
}
