using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BikeMovementScript : MonoBehaviour
{
    WheelJoint2D[] wheelJoint2Ds;
    JointMotor2D frontWheel;
    JointMotor2D backWheel;

    [SerializeField] float acceleration;
    [SerializeField] float MaxAcceleration;
    [SerializeField] float Deceleration;
    [SerializeField] float brakeForce;
    [SerializeField] Transform BackWheel;
    [SerializeField] float wheelRadius;
    [SerializeField] LayerMask ground;
    [SerializeField] bool grounded;
    [SerializeField] float rotationForce;
    bool isRunning;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        wheelJoint2Ds = gameObject.GetComponents<WheelJoint2D>();
        frontWheel = wheelJoint2Ds[0].motor;
        backWheel = wheelJoint2Ds[1].motor;
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        isRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if(isRunning)
        {
            grounded = Physics2D.OverlapCircle(BackWheel.position, wheelRadius, ground);
            //Debug.Log(Physics2D.OverlapCircle(BackWheel.position, wheelRadius, ground) + "" + grounded);

            if (grounded)
            {
                Movement();
            }

            Brake();
            Rotate();
            //Crash();

            //Deceleration
            if (backWheel.motorSpeed < 0)
            {
                backWheel.motorSpeed = Mathf.Clamp(backWheel.motorSpeed + (Deceleration * Time.deltaTime), -MaxAcceleration, 0);
            }
            if (backWheel.motorSpeed > 0)
            {
                backWheel.motorSpeed = Mathf.Clamp(backWheel.motorSpeed - (Deceleration * Time.deltaTime), 0, MaxAcceleration);
            }


            //Apply to frontwheel
            frontWheel = backWheel;

            //Apply motor values
            wheelJoint2Ds[0].motor = frontWheel;
            wheelJoint2Ds[1].motor = backWheel;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(BackWheel.position, wheelRadius);
    }

    void Movement()
    {
        //Forward
        if (Input.GetKey(KeyCode.W))
        {
            //Debug.Log("Move Forward" + backWheel.motorSpeed);
            backWheel.motorSpeed = Mathf.Clamp(backWheel.motorSpeed - (acceleration * Time.deltaTime), -MaxAcceleration, 0);
            //Debug.Log("Move Forward" + backWheel.motorSpeed);
        }
        //Backward
        if (Input.GetKey(KeyCode.S))
        {
            //Debug.Log("Move Forward" + backWheel.motorSpeed);
            backWheel.motorSpeed = Mathf.Clamp(backWheel.motorSpeed + (acceleration * Time.deltaTime), 0, MaxAcceleration);
            //Debug.Log("Move Forward" + backWheel.motorSpeed);
        }
    }
    void Brake()
    {
        //Brake
        if (Input.GetKey(KeyCode.Space))
        {
            if (backWheel.motorSpeed != 0)
            {
                if (backWheel.motorSpeed > 0)
                {
                    backWheel.motorSpeed = Mathf.Clamp(backWheel.motorSpeed - (brakeForce * Time.deltaTime), 0, MaxAcceleration);
                }
                else if (backWheel.motorSpeed < 0)
                {
                    backWheel.motorSpeed = Mathf.Clamp(backWheel.motorSpeed + (brakeForce * Time.deltaTime), -MaxAcceleration, 0);
                }

            }
        }
    }

    void Rotate()
    {
        //Rotate Front
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddTorque(-rotationForce);
        }
        //Rotate Back
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddTorque(rotationForce);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Crash();
    }
    void Crash()
    {
        //Crash
        float z = transform.localEulerAngles.z;
        if (z > 180) z -= 360;

        if (z > 100 || z < -100)
        {
            Debug.Log("Crashed");
            stopBike();
            GameManager.instance.RestartLevel();
        }
    }

    public void stopBike()
    {
        isRunning = false;
        backWheel.motorSpeed = 0;
        frontWheel.motorSpeed = 0;
        wheelJoint2Ds[0].motor = frontWheel;
        wheelJoint2Ds[1].motor = backWheel;
    }

    public void resetBike()
    {
        isRunning = true;
    }
}
