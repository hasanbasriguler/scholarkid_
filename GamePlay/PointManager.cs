using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointManager : MonoBehaviour
{
    private int totalpoint;
    private int pointplus;

    [SerializeField]
    private Text pointtext;


    void Start()
    {
        pointtext.text = totalpoint.ToString();
    }

    public void pointincreasing()
    {
        pointplus = 5;

        totalpoint += pointplus;
        pointtext.text = totalpoint.ToString();
    }


   
}
