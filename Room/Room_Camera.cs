using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_Camera : MonoBehaviour
{
    private GameObject camera;
    private Camera cameras;

    public float camera_zoom, camera_zoom_1, camera_zoom_2;
    private float camera_size;

    public bool is_camera;

    public Vector3 camera_empty, camera_position;
    [System.NonSerialized]
    public Vector3 pos_wall01, pos_wall_accessory01, pos_ground_accessory01;
    [System.NonSerialized]
    public Vector3 pos_wall02, pos_wall_accessory02, pos_ground_accessory02;
    [System.NonSerialized]
    public Vector3 pos_ground, pos_chair01, pos_chair02, pos_table, pos_table_accessory;

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

        camera_size = 6.42f;
        camera_zoom_1 = 3.21f;
        camera_zoom_2 = 4.53f;

        pos_wall01 = new Vector3(1.77f, 6.73f, -7.07f);
        pos_wall_accessory01 = new Vector3(-0.43f, 9.82f, -9.18f);
        pos_ground_accessory01 = new Vector3(1.19f, 5.15f, -8.67f);

        pos_wall02 = new Vector3(-1.28f, 6.38f, -2.82f);
        pos_wall_accessory02 = new Vector3(-3.02f, 9.69f, -4.34f);
        pos_ground_accessory02 = new Vector3(-5.36f, 5.05f, -2.21f);

        pos_ground = new Vector3(-1.75f, 4.21f, -5.57f);
        pos_chair01 = new Vector3(0.43f, 4.21f, -4.80f);
        pos_chair02 = new Vector3(-5.05f, 4.21f, -6.56f);
        pos_table = new Vector3(-1.98f, 4.06f, -5.49f);
        pos_table_accessory = new Vector3(-2.05f, 5.12f, -5.47f);
    }

    private void Update()
    {
        Change_Camera(camera_empty, camera_zoom);
    }

    public void Change_Camera(Vector3 target, float value)
    {
        if (!is_camera) {
            t = 0;
            timeStart = Time.time;
            camera.transform.position = Vector3.Lerp(camera.transform.position, camera_position, Time.deltaTime);
            cameras.orthographicSize = camera_size;
        } else {
            t = (Time.time - timeStart) / timeDuration;
            camera.transform.position = Vector3.Lerp(camera.transform.position, target, Time.deltaTime);
            if (t <= 1.0f) {
                cameras.orthographicSize = (1 - t) * camera_size + t * camera_zoom;
            }
        }
    }
}
