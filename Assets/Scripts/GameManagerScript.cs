using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{

    GameObject playersCar;
    List<GameObject> spawnsPoints;
    Camera playerCamera;
    Camera yellowCamera, blackCamera, redCamera;
    Camera[] cameras;
    GameObject startImage;
    GameObject metaImage;
    Text timeText;

    public static int actualSpawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        playersCar = GameObject.Find("CarPlayer");
        spawnsPoints = new List<GameObject>(GameObject.FindGameObjectsWithTag("SpawnPoint"));
        actualSpawnPoint = 0;
        startImage = GameObject.Find("ImageStart");
        metaImage = GameObject.Find("ImageMeta");
        timeText = GameObject.Find("TextTime").GetComponent<Text>();
        cameras = Camera.allCameras;
        playersCar.GetComponent<EnginePlayer>().enabled = false;
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
        metaImage.SetActive(false);
        timeText.text = "00:00:000";
        StartCoroutine(RaceStart());

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

    public IEnumerator RaceStart()
    {
        startImage.GetComponentInChildren<Text>().text = "3";
        yield return new WaitForSeconds(2);
        startImage.GetComponentInChildren<Text>().text = "2";
        yield return new WaitForSeconds(2);
        startImage.GetComponentInChildren<Text>().text = "1";
        yield return new WaitForSeconds(3);
        startImage.GetComponentInChildren<Text>().text = "START";
        startImage.GetComponent<RawImage>().color = Color.green;
        playersCar.GetComponent<EnginePlayer>().enabled = true;
        Timer.ResetTime();
        yield return new WaitForSeconds(1);
        startImage.SetActive(false);
    }

}
