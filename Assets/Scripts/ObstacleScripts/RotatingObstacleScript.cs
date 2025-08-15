using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingObstacleScript : MonoBehaviour
{
    [SerializeField] float rotationSpeed;
    [SerializeField] bool clockwise;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (clockwise)
        {
            Quaternion newRot = transform.rotation * Quaternion.Euler(0, 0, rotationSpeed * Time.deltaTime);
            transform.rotation = newRot;
        }
        else
        {
            Quaternion newRot = transform.rotation * Quaternion.Euler(0, 0, -rotationSpeed * Time.deltaTime);
            transform.rotation = newRot;
        }

    }
}
