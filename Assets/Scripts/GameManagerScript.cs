using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{

    GameObject playersCar;
    List<GameObject> spawnsPoints;

    public static int actualSpawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        playersCar = GameObject.Find("CarPlayer");
        spawnsPoints = new List<GameObject>(GameObject.FindGameObjectsWithTag("SpawnPoint"));
        actualSpawnPoint = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("space"))
        {
            Spawn();
        }
    }

    void Spawn()
    {
        playersCar.transform.position = spawnsPoints[actualSpawnPoint].transform.position;
        playersCar.transform.rotation = spawnsPoints[actualSpawnPoint].transform.rotation;
    }

}
