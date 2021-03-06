using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{

    public static GameObject playersCar;
    Camera playerCamera;
    static Camera yellowCamera, blackCamera, redCamera;
    static Camera podiumCamera;
    static Camera[] cameras;
    GameObject startImage;
    static GameObject metaImage;
    Text timeText;
    public static GameObject yellowCar, redCar, blackCar;
    int carTimeWait;


    // Start is called before the first frame update
    void Start()
    {
        playersCar = GameObject.Find("CarPlayer");
        startImage = GameObject.Find("ImageStart");
        metaImage = GameObject.Find("ImageMeta");
        timeText = GameObject.Find("TextTime").GetComponent<Text>();
        cameras = Camera.allCameras;
        playersCar.GetComponent<EnginePlayer>().enabled = false;
        yellowCar = GameObject.Find("CarYellow");
        redCar = GameObject.Find("CarRed");
        blackCar = GameObject.Find("CarBlack");
        carTimeWait = 1;
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
            if (camera.name == "CameraPodium")
            {
                podiumCamera = camera;
            }
        }

        foreach (Camera camera in cameras)
        {
            camera.enabled = false;
        }
        playerCamera.enabled = true;
        metaImage.SetActive(false);
        timeText.text = "00:00:000";
        redCar.GetComponent<Engine>().enabled = false;
        blackCar.GetComponent<Engine>().enabled = false;
        yellowCar.GetComponent<Engine>().enabled = false;
        StartCoroutine(CarsStartControl());
        StartCoroutine(RaceStart());

    }

    // Update is called once per frame
    void Update()
    {

        SwitchCamera();

        if(Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

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
        if (Input.GetKey("5"))
        {
            foreach (Camera camera in cameras)
            {
                camera.enabled = false;
            }
            podiumCamera.enabled = true;
        }
    }

    public static void SwitchCameraPodium()
    {
        foreach (Camera camera in cameras)
        {
            camera.enabled = false;
        }
        podiumCamera.enabled = true;
    }

    public IEnumerator RaceStart()
    {
        startImage.GetComponentInChildren<Text>().text = "3";
        yield return new WaitForSeconds(2);
        startImage.GetComponentInChildren<Text>().text = "2";
        yield return new WaitForSeconds(2);
        startImage.GetComponentInChildren<Text>().text = "1";
        yield return new WaitForSeconds(2);
        startImage.GetComponentInChildren<Text>().text = "START";
        startImage.GetComponent<RawImage>().color = Color.green;
        playersCar.GetComponent<EnginePlayer>().enabled = true;
        Timer.ResetTime();
        yield return new WaitForSeconds(1);
        startImage.SetActive(false);
    }

    IEnumerator CarsStartControl()
    {
        yellowCar.GetComponent<Engine>().enabled = true;
        yield return new WaitForSeconds(carTimeWait);
        redCar.GetComponent<Engine>().enabled = true;
        yield return new WaitForSeconds(carTimeWait);
        blackCar.GetComponent<Engine>().enabled = true;
        yield return new WaitForSeconds(carTimeWait);
    }

    public static void Finish()
    {
        metaImage.SetActive(true);
    }
    

}
