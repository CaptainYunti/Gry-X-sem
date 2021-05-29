using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    public WheelCollider wheelCollider;

    private Vector3 wheelPosition = new Vector3();
    private Quaternion wheelRotation = new Quaternion();

    private void Update()
    {
        wheelCollider.GetWorldPose(out wheelPosition, out wheelRotation);

        transform.position = wheelPosition;
        transform.rotation = wheelRotation;
    }
}
