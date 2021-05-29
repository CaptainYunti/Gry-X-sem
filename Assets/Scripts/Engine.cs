using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour
{
    public Transform path;
    public List<Transform> nodes;
    int currNode = 0;

    public float maxSteerAngle = 45f;
    public float maxMotorTorque = 100f;
    public float maxBrakeTorque = 150f;
    public float minSpeed = 100f;
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

    [Header("Sensors")]
    public float sensorsLength = 10f;
    public Vector3 frontSensorPostion = new Vector3(0f, 0.75f, 0.0f);
    public float frontSideSensorPosition = 0.8f;
    public float frontSensorAngle = 30f;

    private bool avoiding = false;
    private float targetSteerAngle = 0.0f;

    private void Start()
    {
        GetComponent<Rigidbody>().centerOfMass = centerOfMass;

        Transform[] pathTransforms = path.GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();

        for (int i = 0; i < pathTransforms.Length; i++)
        {
            if (pathTransforms[i] != path.transform)
            {
                nodes.Add(pathTransforms[i]);
            }
        }
    }

    private void FixedUpdate()
    {
        Sensors();
        ApplySteer();
        Drive();
        CheckWaypointDistance();
        Braking();
        LerpToSteerAngle();
    }

    private void Sensors()
    {
        float avoidMultiplier = 0.0f;

        avoiding = false;

        RaycastHit hit;
        Vector3 sensorStartPosition = transform.position;
        
        sensorStartPosition += transform.forward * frontSensorPostion.z;
        sensorStartPosition += transform.up * frontSensorPostion.y;
        sensorStartPosition += transform.right * frontSideSensorPosition;

        if (Physics.Raycast(sensorStartPosition, transform.forward, out hit, sensorsLength))
        {   // front right sensor
            if (!hit.collider.CompareTag("Terrain"))
            {
                avoiding = true;
                avoidMultiplier -= 1f;

                Debug.DrawLine(sensorStartPosition, hit.point, Color.yellow);
            }
        }
        else if (Physics.Raycast(sensorStartPosition, Quaternion.AngleAxis(frontSensorAngle, transform.up) * transform.forward, out hit, sensorsLength))
        {   // front right angle sensor
            if (!hit.collider.CompareTag("Terrain"))
            {
                avoiding = true;
                avoidMultiplier -= 0.5f;

                Debug.DrawLine(sensorStartPosition, hit.point, Color.blue);
            }
        }

        sensorStartPosition -= 2 * transform.right * frontSideSensorPosition;

        if (Physics.Raycast(sensorStartPosition, transform.forward, out hit, sensorsLength))
        {   // front left sensor
            if (!hit.collider.CompareTag("Terrain"))
            {
                avoiding = true;
                avoidMultiplier += 1f;

                Debug.DrawLine(sensorStartPosition, hit.point, Color.magenta);
            }
        }
        else if (Physics.Raycast(sensorStartPosition, Quaternion.AngleAxis(-frontSensorAngle, transform.up) * transform.forward, out hit, sensorsLength))
        {   // front left angle sensor
            if (!hit.collider.CompareTag("Terrain"))
            {
                avoiding = true;
                avoidMultiplier += 0.5f;

                Debug.DrawLine(sensorStartPosition, hit.point, Color.black);
            }
        }

        // front center sensor
        if (avoidMultiplier == 0 )
        {
            if (Physics.Raycast(sensorStartPosition, transform.forward, out hit, sensorsLength))
            {
                if (!hit.collider.CompareTag("Terrain"))
                {
                    avoiding = true;

                    if (hit.normal.x < 0)
                    {
                        avoidMultiplier = -1.0f;
                    }
                    else
                    {
                        avoidMultiplier = 1.0f;
                    }

                    Debug.DrawLine(sensorStartPosition, hit.point, Color.green);
                }
            }
        }
        
        if (avoiding)
        {
            targetSteerAngle = maxSteerAngle * avoidMultiplier;
        }
    }
  
    private void ApplySteer()
    {
        if (avoiding)
        {
            return;
        }

        Vector3 relativeVector = transform.InverseTransformPoint(nodes[currNode].position);
        float newSteer = (relativeVector.x / relativeVector.magnitude) * maxSteerAngle;

        targetSteerAngle = newSteer;
    }

    private void Drive()
    {
        CalculateCurrentSpeed();

        if (isBraking)
        {
            return;
        }

        if (currentSpeed < maxSpeed)
        {
            SpeedUp();
        }
        else
        {
            StopSpeedUp();
        }
    }

    private void CheckWaypointDistance()
    {
        float distanceToNode = Vector3.Distance(transform.position, nodes[currNode].position);

        if (distanceToNode < 0.5f)
        {
            if (currNode == nodes.Count - 1)
            {
                currNode = 0;
            }
            else
            {
                currNode++;
            }
        }
    }

    private void Braking()
    {
        float distanceToNode = Vector3.Distance(transform.position, nodes[currNode].position);

        if (distanceToNode < 10f && currentSpeed > minSpeed)
        {

            SlowDown();

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

    private void LerpToSteerAngle()
    {
        wheelFrontFL.steerAngle = Mathf.Lerp(wheelFrontFL.steerAngle, targetSteerAngle, Time.deltaTime * turnSpeed);
        wheelFrontFR.steerAngle = Mathf.Lerp(wheelFrontFR.steerAngle, targetSteerAngle, Time.deltaTime * turnSpeed);
    }

    private void CalculateCurrentSpeed()
    {
        currentSpeed = 2 * Mathf.PI * wheelFrontFL.radius * wheelFrontFL.rpm * 60 / 100;
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
}
