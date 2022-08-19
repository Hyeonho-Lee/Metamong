using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House_Camera : MonoBehaviour
{
    private GameObject camera;
    private Camera cameras;

    public float camera_zoom, camera_zoom_1, camera_zoom_2;
    private float camera_size;

    public bool is_camera;

    public Vector3 camera_empty, camera_position, camera_look;
    public Vector3 pos_house;

    private float t;
    private float timeStart;
    private float timeDuration;

    private void Start()
    {
        camera = GameObject.Find("Main Camera");
        cameras = camera.GetComponent<Camera>();

        camera_position = camera.transform.position;

        camera_empty = camera_position;
        camera_zoom = camera_zoom_1;
        Check_Value();
    }

    private void Check_Value()
    {
        t = 0;
        timeDuration = 1.0f;
        timeStart = Time.time;

        camera_size = 320.0f;
        //camera_zoom_1 = 3.21f;
        //camera_zoom_2 = 4.53f;

        //pos_house = house_console.All_Position[0].position;
        //pos_wall01 = new Vector3(1.77f, 6.73f, -7.07f);
    }

    private void Update()
    {
        Change_Camera(camera_empty, camera_zoom, camera_look);
    }

    public void Change_Camera(Vector3 target, float value, Vector3 look_target)
    {
        if (!is_camera) {
            t = 0;
            timeStart = Time.time;
            camera.transform.position = Vector3.Lerp(camera.transform.position, camera_position, Time.deltaTime);
            cameras.orthographic = true;
            cameras.orthographicSize = camera_size;
            cameras.transform.rotation = Quaternion.Euler(90.0f, 0, 0);
        } else {
            t = (Time.time - timeStart) / timeDuration;
            camera.transform.position = Vector3.Lerp(camera.transform.position, target, Time.deltaTime * t);

            if (camera_look != Vector3.zero) {
                camera.transform.rotation = Quaternion.LookRotation(look_target);
            }

            cameras.orthographic = false;
            cameras.fieldOfView = 80.0f;

            if (t <= 1.0f) {
                cameras.orthographicSize = (1 - t) * camera_size + t * camera_zoom;
            }
        }
    }
}
