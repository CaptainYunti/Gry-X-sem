using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnginePlayer : MonoBehaviour
{

    public float maxSteerAngle = 45f;
    public float maxMotorTorque = 100f;
    public float maxBrakeTorque = 120f;
    public float withoutGasTorque = 30f;
    public float maxSpeed = 200f;
    public float turnSpeed = 5.0f;

    public float currentSpeed = 0f;

    public Vector3 centerOfMass;

    public WheelCollider wheelFrontFL;
    public WheelCollider wheelFrontFR;
    public WheelCollider wheelRearFL;
    public WheelCollider wheelRearFR;

    public Renderer carBody;

    public bool isBraking = false;
    public bool gasPush = false;

    private float targetSteerAngle = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().centerOfMass = centerOfMass;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ApplySteer();
        LerpToSteerAngle();
        Drive();
    }


    private void ApplySteer()
    {

        if (Input.GetKey("a"))
        {
            targetSteerAngle = -90f;
        }
        else if (Input.GetKey("d"))
        {
            targetSteerAngle = 90f;
        }
        else
        {
            targetSteerAngle = 0f;
        }
       
    }

    private void Drive()
    {
        CalculateCurrentSpeed();

        if (Input.GetKey("s"))
        {
            SlowDown();

            isBraking = true;

            carBody.materials[1].SetColor("_Color", new Color(255f, 0f, 0f));
        }
        else if (isBraking)
        {
            StopSlownDown();

            carBody.materials[1].SetColor("_Color", new Color(170f, 10f, 10f));

            isBraking = false;
        }

        if (Input.GetKey("w"))
        {
            if (currentSpeed < maxSpeed)
            {
                SpeedUp();
            }
            else
            {
                StopSpeedUp();
            }

            gasPush = true;
        }

        else
        {
            gasPush = false;
        }

        if (gasPush == false)
        {
            WithoutGasDrive();
        }

        else if (!Input.GetKey("s"))
        {
            StopSlownDown();
        }


    }

    private void CalculateCurrentSpeed()
    {
        currentSpeed = 2 * Mathf.PI * wheelFrontFL.radius * wheelFrontFL.rpm * 60 / 100;
    }

    private void Braking()
    {

        if (Input.GetButton("s"))
        {
            if(System.Math.Abs(currentSpeed) < maxSpeed)
            {
                SlowDown();
            }
   
            isBraking = true;

            carBody.materials[1].SetColor("_Color", new Color(255f, 0f, 0f));
        }
        else
        {
            StopSlownDown();

            carBody.materials[1].SetColor("_Color", new Color(170f, 10f, 10f));

            isBraking = false;
        }
    }

    private void SpeedUp()
    {
        wheelFrontFL.motorTorque = maxMotorTorque;
        wheelFrontFR.motorTorque = maxMotorTorque;
        wheelRearFL.motorTorque = maxMotorTorque;
        wheelRearFR.motorTorque = maxMotorTorque;
    }

    private void StopSpeedUp()
    {
        wheelFrontFL.motorTorque = 0f;
        wheelFrontFR.motorTorque = 0f;
        wheelRearFL.motorTorque = 0f;
        wheelRearFR.motorTorque = 0f;
    }

    private void SlowDown()
    {
        wheelFrontFL.brakeTorque = maxBrakeTorque;
        wheelFrontFR.brakeTorque = maxBrakeTorque;
        wheelRearFL.brakeTorque = maxBrakeTorque;
        wheelRearFR.brakeTorque = maxBrakeTorque;
    }

    private void StopSlownDown()
    {
        wheelFrontFL.brakeTorque = 0f;
        wheelFrontFR.brakeTorque = 0f;
        wheelRearFL.brakeTorque = 0f;
        wheelRearFR.brakeTorque = 0f;
    }

    private void WithoutGasDrive()
    {
        wheelFrontFL.brakeTorque = withoutGasTorque;
        wheelFrontFR.brakeTorque = withoutGasTorque;
        wheelRearFL.brakeTorque = withoutGasTorque;
        wheelRearFR.brakeTorque = withoutGasTorque;
    }

    private void LerpToSteerAngle()
    {
        wheelFrontFL.steerAngle = Mathf.Lerp(wheelFrontFL.steerAngle, targetSteerAngle, Time.deltaTime * turnSpeed);
        wheelFrontFR.steerAngle = Mathf.Lerp(wheelFrontFR.steerAngle, targetSteerAngle, Time.deltaTime * turnSpeed);
    }
}
