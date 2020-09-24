using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAnimation : MonoBehaviour
{


    [Range(0.1f, 5f)]
    public float WaitBetweenWobbles = 0.5f;

    [Range(1f, 50f)]
    public float Intensity = 0.5f;

    Quaternion TargetAngle;

    void Start()
    {
        InvokeRepeating("ChangeTarget", 0, WaitBetweenWobbles);
    }


    void Update()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, TargetAngle, Time.deltaTime);
    }

    void ChangeTarget()
    {

        var intensity = Random.Range(0.1f, Intensity);
        var curve = Mathf.Sin(Random.Range(0, Mathf.PI * 2));
        TargetAngle = Quaternion.Euler(Vector3.forward * curve * Intensity);

    }


}
