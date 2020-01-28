using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scale : MonoBehaviour
{
    [SerializeField]
    private float scalingSpeed = 0.3f;
    
    [SerializeField]
    private float targetScale = 0.01f;

    float scalingTime;

    [SerializeField]
    private float length = 0.001f;

    void Update()
    {
        scalingTime = Time.time * scalingSpeed; 
        transform.localScale = new Vector3(transform.localScale.x, Mathf.PingPong(scalingTime, length) + targetScale, transform.localScale.z);
    }
}
