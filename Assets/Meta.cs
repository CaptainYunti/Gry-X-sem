using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meta : MonoBehaviour
{

    public GameObject blueCar, redCar, blackCar, yellowCar;
    public GameObject imageEnd;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == blueCar)
        {
            imageEnd.SetActive(true);
        }

        if (other.gameObject.CompareTag("Car"))
        {
            other.gameObject.GetComponent<Engine>().enabled = false;
        }
    }
}
