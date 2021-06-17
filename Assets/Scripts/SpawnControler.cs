using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnControler : MonoBehaviour
{

    public GameObject redCar, yellowCar, blackCar, blueCar;
    public GameObject spawnPointMountain, spawnPointGrass, spawnPointDesert;
    static int spawnNumberRed, spawnNumberYellow, spawnNumberBlack, spawnNumberBlue;
    int spawnLineID;
    static int linesCount = 0;
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
        spawnLineID = linesCount++;

        Invoke("SetStartPosition", 2.0f);
        InvokeRepeating("Controlling", 10.0f, 2.0f);
    }

    private void Update()
    {
        if (spawnLineID == 0)
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
        if (spawnLineID == 1)
        {
            RedCarRespawn();
            redPosition = new Vector3(redCar.transform.position.x, redCar.transform.position.y, redCar.transform.position.z);
        }

        else if (spawnLineID == 0)
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

        if (other.gameObject == blueCar)
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
            redCar.GetComponent<Engine>().Reset();
        }
    }

    void BlackCarRespawn()
    {
        if (Vector3.Distance(blackCar.transform.position, blackPosition) < 1 && blackCar.GetComponent<Engine>().enabled == true)
        {
            Respawn(blackCar, spawnNumberBlack);
            blackCar.GetComponent<Engine>().Reset();
        }
    }

    void YellowCarRespawn()
    {
        if (Vector3.Distance(yellowCar.transform.position, yellowPosition) < 1 && yellowCar.GetComponent<Engine>().enabled == true)
        {
            Respawn(yellowCar, spawnNumberYellow);
            yellowCar.GetComponent<Engine>().Reset();
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
}
