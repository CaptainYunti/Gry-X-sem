using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{

    GameObject playersCar;
    List<GameObject> spawnsPoints;
    Camera playerCamera;
    Camera yellowCamera, blackCamera, redCamera;
    Camera[] cameras;

    public static int actualSpawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        playersCar = GameObject.Find("CarPlayer");
        spawnsPoints = new List<GameObject>(GameObject.FindGameObjectsWithTag("SpawnPoint"));
        actualSpawnPoint = 0;
        cameras = Camera.allCameras;
        foreach (Camera camera in cameras)
        {
            if(camera.name == "CameraYellow")
            {
                yellowCamera = camera;
            }
            if(camera.name == "CameraPlayer")
            {
                playerCamera = camera;
            }
            if(camera.name == "CameraBlack")
            {
                blackCamera = camera;
            }
            if(camera.name == "CameraRed")
            {
                redCamera = camera;
            }
        }

        foreach (Camera camera in cameras)
        {
            camera.enabled = false;
        }
        playerCamera.enabled = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("space"))
        {
            Spawn();
        }

        SwitchCamera();
    }

    void Spawn()
    {
        playersCar.transform.position = spawnsPoints[actualSpawnPoint].transform.position;
        playersCar.transform.rotation = spawnsPoints[actualSpawnPoint].transform.rotation;
    }

    void SwitchCamera()
    {
        if(Input.GetKey("1"))
        {
            foreach (Camera camera in cameras)
            {
                camera.enabled = false;
            }
            playerCamera.enabled = true;
        }
        if(Input.GetKey("2"))
        {
            foreach (Camera camera in cameras)
            {
                camera.enabled = false;
            }
            yellowCamera.enabled = true;
        }
        if(Input.GetKey("3"))
        {
            foreach (Camera camera in cameras)
            {
                camera.enabled = false;
            }
            redCamera.enabled = true;

        }
        if(Input.GetKey("4"))
        {
            foreach (Camera camera in cameras)
            {
                camera.enabled = false;
            }
            blackCamera.enabled = true;
        }
    }

}
