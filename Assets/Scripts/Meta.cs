using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meta : MonoBehaviour
{

    public GameObject blueCar, redCar, blackCar, yellowCar;
    public GameObject imageEnd;
    public GameObject firstPlace, secondPlace, thirdPlace, fourthPlace;
    GameObject[] podium;
    int podiumPlace;

    private void Start()
    {
        podiumPlace = 0;
        podium = new GameObject[] { firstPlace, secondPlace, thirdPlace, fourthPlace };
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == blueCar)
        {
            imageEnd.SetActive(true);
            blueCar.GetComponent<EnginePlayer>().enabled = false;
            StartCoroutine(CarFinishRace(blueCar));
            Invoke("CloseMetaImage", 10.0f);
            
        }

        if (other.gameObject.CompareTag("Car"))
        {
            other.gameObject.GetComponent<Engine>().enabled = false;
            StartCoroutine(CarFinishRace(other.gameObject));
            
        }
    }

    IEnumerator CarFinishRace(GameObject car)
    {
        yield return new WaitForSeconds(3);
        car.transform.position = podium[podiumPlace].transform.position;
        car.transform.rotation = podium[podiumPlace].transform.rotation;
        car.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        car.GetComponent<Rigidbody>().freezeRotation = true;
        podiumPlace++;
        if (GameObject.Equals(car, blueCar))
        {
            GameManagerScript.SwitchCameraPodium();
        }
        yield return new WaitForEndOfFrame();
    }

    private void CloseMetaImage()
    {
        imageEnd.SetActive(false);
    }
}
