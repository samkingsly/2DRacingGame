using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacleScript : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] int horizontal = 0; // -1 left, 1 right
    [SerializeField] int vertical = 0;   // -1 down, 1 up
    [SerializeField] float maxHorizontal = 5f;
    [SerializeField] float maxVertical = 5f;

    Vector3 initPos;

    void Start()
    {
        initPos = transform.position;
    }

    void Update()
    {
        float x = transform.position.x;
        float y = transform.position.y;

        if (horizontal != 0)
        {
            x += horizontal * moveSpeed * Time.deltaTime;

            // Check distance from initial X
            if (Mathf.Abs(x - initPos.x) > maxHorizontal)
            {
                horizontal *= -1;
            }
        }

        if (vertical != 0)
        {
            y += vertical * moveSpeed * Time.deltaTime;

            // Check distance from initial Y
            if (Mathf.Abs(y - initPos.y) > maxVertical)
            {
                vertical *= -1;
            }
        }

        transform.position = new Vector3(x, y, initPos.z);
    }
}
