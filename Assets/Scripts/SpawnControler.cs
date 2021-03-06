using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnControler : MonoBehaviour
{

    public GameObject redCar, yellowCar, blackCar, blueCar;
    public GameObject spawnPointMountain, spawnPointGrass, spawnPointDesert;
    public GameObject lineGrass, lineDesert;
    static int spawnNumberRed, spawnNumberYellow, spawnNumberBlack, spawnNumberBlue;
    int spawnLineID;
    static Vector3 blackPosition, yellowPosition, redPosition;
    // Start is called before the first frame update
    void  Start()
    {
        //redCar = GameManagerScript.redCar;
        //yellowCar = GameManagerScript.yellowCar;
        //blackCar = GameManagerScript.blackCar;
        //blueCar = GameManagerScript.playersCar;
        //spawnPointMountain = GameObject.Find("SpawnPointMountain");
        //spawnPointGrass = GameObject.Find("SpawnPointGrass");
        //spawnPointDesert = GameObject.Find("SpawnPointDesert");
        spawnNumberBlack = 0;
        spawnNumberBlue = 0;
        spawnNumberRed = 0;
        spawnNumberYellow = 0;

        if (this.gameObject == lineGrass)
        { 
            spawnLineID = 1;
        }
        else if (this.gameObject == lineDesert)
        {
            spawnLineID = 2;
        }

        Invoke("SetStartPosition", 2.0f);
        InvokeRepeating("Controlling", 10.0f, 2.0f);
        //InvokeRepeating("SpawnLineTest", 10.0f, 10.0f);
    }

    private void Update()
    {
        if (spawnLineID == 1)
        {
            PlayerRespawn();
        }

    }

    void SetStartPosition()
    {
        blackPosition = blackCar.transform.position;
        blackPosition = blackCar.transform.position;
        yellowPosition = yellowCar.transform.position;
    }

    void Controlling()
    {
        if (spawnLineID == 2)
        {
            RedCarRespawn();
            redPosition = new Vector3(redCar.transform.position.x, redCar.transform.position.y, redCar.transform.position.z);
        }

        else if (spawnLineID == 1)
        {
            BlackCarRespawn();
            YellowCarRespawn();
            blackPosition = new Vector3(blackCar.transform.position.x, blackCar.transform.position.y, blackCar.transform.position.z);
            yellowPosition = new Vector3(yellowCar.transform.position.x, yellowCar.transform.position.y, yellowCar.transform.position.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
       if (other.gameObject == redCar)
        {
            if (spawnNumberRed > spawnLineID)
            {
                return;
            }

            spawnNumberRed = spawnLineID;
        }

        if (other.gameObject == blackCar)
        {
            if (spawnNumberBlack > spawnLineID)
            {
                return;
            }

            spawnNumberBlack = spawnLineID;
        }

        if (other.gameObject == yellowCar)
        {
            if (spawnNumberYellow > spawnLineID)
            {
                return;
            }

            spawnNumberYellow = spawnLineID;
        }

        if (GameObject.Equals(other.gameObject, blueCar))
        {

            if (spawnNumberBlue > spawnLineID)
            {
                return;
            }

            spawnNumberBlue = spawnLineID;
        }

    }

    void PlayerRespawn()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            Respawn(blueCar, spawnNumberBlue);
        }
    }

    void RedCarRespawn()
    {
        if (Vector3.Distance(redCar.transform.position, redPosition) < 1 && redCar.GetComponent<Engine>().enabled == true)
        {
            Respawn(redCar, spawnNumberRed);
            switch(spawnNumberRed)
            {
                case 0:
                    redCar.GetComponent<Engine>().Reset(0);
                    break;
                case 1:
                    redCar.GetComponent<Engine>().Reset(30);
                    break;
                case 2:
                    redCar.GetComponent<Engine>().Reset(69);
                    break;
            }
        }
    }

    void BlackCarRespawn()
    {
        if (Vector3.Distance(blackCar.transform.position, blackPosition) < 1 && blackCar.GetComponent<Engine>().enabled == true)
        {
            Respawn(blackCar, spawnNumberBlack);
            switch (spawnNumberBlack)
            {
                case 0:
                    blackCar.GetComponent<Engine>().Reset(0);
                    break;
                case 1:
                    blackCar.GetComponent<Engine>().Reset(30);
                    break;
                case 2:
                    blackCar.GetComponent<Engine>().Reset(69);
                    break;
            }
        }
    }

    void YellowCarRespawn()
    {
        if (Vector3.Distance(yellowCar.transform.position, yellowPosition) < 1 && yellowCar.GetComponent<Engine>().enabled == true)
        {
            Respawn(yellowCar, spawnNumberYellow);
            switch (spawnNumberYellow)
            {
                case 0:
                    yellowCar.GetComponent<Engine>().Reset(0);
                    break;
                case 1:
                    yellowCar.GetComponent<Engine>().Reset(30);
                    break;
                case 2:
                    yellowCar.GetComponent<Engine>().Reset(69);
                    break;
            }
        }
    }

    void Respawn(GameObject car, int number)
    {
        switch (number)
        {
            case 0:
                car.transform.position = spawnPointMountain.transform.position;
                car.transform.rotation = spawnPointMountain.transform.rotation;
                break;
            case 1:
                car.transform.position = spawnPointGrass.transform.position;
                car.transform.rotation = spawnPointGrass.transform.rotation;
                break;
            case 2:
                car.transform.position = spawnPointDesert.transform.position;
                car.transform.rotation = spawnPointDesert.transform.rotation;
                break;
        }
    }

    void SpawnLineTest()
    {
        Debug.Log("Red: " + spawnNumberRed);
        Debug.Log("Blue: " + spawnNumberBlue);
        Debug.Log("Black: " + spawnNumberBlack);
        Debug.Log("Yellow: " + spawnNumberYellow);
    }
}
