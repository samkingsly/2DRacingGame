using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public Transform target;
    [SerializeField] Vector3 offset;
    [SerializeField] float followSpeed;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {
        if (target == null)
            return;

        float x = target.position.x + offset.x;
        float y = target.position.y + offset.y;
        float z = -2f;
        transform.position = new Vector3(x, y, z);
    }


    
}
