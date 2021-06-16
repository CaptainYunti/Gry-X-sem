using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EnginePlayer : MonoBehaviour
{

    public float maxSteerAngle = 40f;
    public float maxMotorTorque = 500f;
    public float maxBrakeTorque = 400f;
    public float withoutGasTorque = 50f;
    public float maxSpeed = 200f;
    public float turnSpeed = 15.0f;

    public float currentSpeed = 0f;

    public Vector3 centerOfMass;

    public WheelCollider wheelFrontFL;
    public WheelCollider wheelFrontFR;
    public WheelCollider wheelRearFL;
    public WheelCollider wheelRearFR;

    public Renderer carBody;

    public bool isBraking = false;
    public bool gasPush = false;

    Text speedText;

    List<GameObject> spawnsLines;

    private float targetSteerAngle = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().centerOfMass = centerOfMass;
        speedText = GameObject.Find("TextSpeedValue").GetComponent<Text>();
        spawnsLines = new List<GameObject>(GameObject.FindGameObjectsWithTag("SpawnLine"));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ApplySteer();
        LerpToSteerAngle();
        Drive();

        speedText.text = currentSpeed.ToString();
    }

    private void ApplySteer()
    {
        if (Input.GetKey("a")) {
            targetSteerAngle = -40f;
        } else if (Input.GetKey("d")) {
            targetSteerAngle = 40f;
        } else {
            targetSteerAngle = 0f;
        }
    }

    private void Drive()
    {
        CalculateCurrentSpeed();

        if (Input.GetKey("w")) {
            StopSlownDown();

            if (currentSpeed < maxSpeed) {
                SpeedUp();
            } else {
                StopSpeedUp();
            }

            carBody.materials[1].SetColor("_Color", new Color(170f, 10f, 10f));
            gasPush = true;
            isBraking = false;
        } else if (Input.GetKey("s")) {
            gasPush = false;
            isBraking = true;
            StopSpeedUp();
            SlowDown();

            carBody.materials[1].SetColor("_Color", new Color(255f, 0f, 0f));
        } else {
            isBraking = false;
            gasPush = false;
            StopSpeedUp();
            WithoutGasDrive();
        }
    }

    private void CalculateCurrentSpeed()
    {
        currentSpeed = 2 * Mathf.PI * wheelFrontFL.radius * wheelFrontFL.rpm * 60 / 100;
        if(currentSpeed > maxSpeed)
        {
            currentSpeed = maxSpeed;
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "SpawnLine")
        {
            GameManagerScript.actualSpawnPoint++;
            other.gameObject.SetActive(false);
        }
    }
}
