using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{

    public float horizontal;
    public float vertical;

    public float mass;
    public float jump_speed;
    public float walk_speed;
    public float dash_speed;
    public float move_speed;
    public float velocity;
    public float velocity_downhill;
    public float ground_height;
    public float ground_range;

    public bool is_joystick;
    public bool is_ground;
    public bool is_attack;
    public bool is_jump;
    public bool is_dash;
    public bool is_fly;
    public bool is_wall;
    public bool is_idle;

    public LayerMask ground_hit;
    private RaycastHit wall_hit;

    public Vector3 movement;
    public Vector3 animation_movement;
    public Vector3 player_dir;
    public Vector3 player_forward;
    public Vector3 impact;
    private Vector3 dir_forward, dir_f_right, dir_right, dir_b_right, dir_back, dir_b_left, dir_left, dir_f_left;

    CharacterController cc;
    //TPSCamera tpscamera;

    void Awake()
    {
        cc = GetComponent<CharacterController>();
        //tpscamera = GameObject.Find("TPS_Camera").GetComponent<TPSCamera>();
    }

    void Start()
    {
        mass = 1.2f;
        jump_speed = 2.0f;
        walk_speed = 5.0f;
        dash_speed = 5.0f;
        velocity = 0.0f;
        velocity_downhill = -6.0f;
        ground_height = -0.05f;
        ground_range = 0.25f;
        impact = Vector3.zero;
        is_joystick = false;
        is_idle = true;
    }

    void Update()
    {
        Check_Input();
        Check_Ground();
        Check_Value();
    }

    void FixedUpdate()
    {
        Move();
    }

    void OnDrawGizmos()
    {
        Color green = new Color(0.0f, 1.0f, 0.0f, 0.5f);
        Color red = new Color(1.0f, 0.0f, 0.0f, 0.5f);

        Gizmos.color = is_ground ? green : red;
        Gizmos.DrawSphere(transform.position - new Vector3(0, ground_height, 0), ground_range);
    }

    void Check_Input()
    {
        if (!is_joystick)
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
        }
        else
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
        }

        //player_dir = tpscamera.player_dir.normalized;

        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetButtonDown("J_B"))
        {
            is_dash = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetButtonUp("J_B"))
        {
            is_dash = false;
        }

        if ((Input.GetKeyDown(KeyCode.Space) && is_ground) ||
            (Input.GetButtonDown("J_A") && is_ground) )
        {
            StartCoroutine(Jump(2.0f, 30.0f));
        }
    }

    void Check_Value()
    {
        if (is_ground)
        {
            is_fly = false;
            if (is_dash && is_ground)
            {
                is_idle = false;
            }
            else
            {
                is_idle = true;
            }
            if (velocity < 0.0f)
            {
                velocity = -2.0f;
            }
        }
        else
        {
            is_fly = true;
            if (velocity < 53.0f)
            {
                velocity += -15.0f * Time.deltaTime;
            }
        }

        if (is_dash)
        {
            move_speed = walk_speed + dash_speed;

            if (is_fly && is_dash)
            {
                move_speed = walk_speed;
            }
        }
        else
        {
            move_speed = walk_speed;
        }

        if (is_fly)
        {
            cc.slopeLimit = 0f;
            is_idle = false;
        }
        else
        {
            cc.slopeLimit = 60f;
        }

        if (is_wall)
        {
        }

        if (is_joystick)
        {
            //tpscamera.is_joystick = true;
        }
        else
        {
            //tpscamera.is_joystick = false;
        }
    }

    void Move()
    {
        movement.Set(horizontal, 0, vertical);
        movement = movement.normalized;

        dir_forward = Quaternion.Euler(0f, 0f, 0f) * player_dir;
        dir_f_right = Quaternion.Euler(0f, 45f, 0f) * player_dir;
        dir_right = Quaternion.Euler(0f, 90f, 0f) * player_dir;
        dir_b_right = Quaternion.Euler(0f, 135f, 0f) * player_dir;
        dir_back = Quaternion.Euler(0f, 180f, 0f) * player_dir;
        dir_b_left = Quaternion.Euler(0f, 225f, 0f) * player_dir;
        dir_left = Quaternion.Euler(0f, 270f, 0f) * player_dir;
        dir_f_left = Quaternion.Euler(0f, 315f, 0f) * player_dir;

        if (movement.z == 0 && movement.x == 0)
        {
            cc.Move(movement * (walk_speed * Time.deltaTime) + new Vector3(0.0f, velocity, 0.0f) * Time.deltaTime);
        }

        // ¾Õ¿À
        if ((movement.z > 0 && movement.z <= 1) && (movement.x > 0 && movement.x <= 1))
        {
            Move_Vector(dir_f_right, dir_f_right);
        }

        // ¾Õ¿Þ
        if ((movement.z > 0 && movement.z <= 1) && (movement.x < 0 && movement.x >= -1))
        {
            Move_Vector(dir_f_left, dir_f_left);
        }

        // µÚ¿Þ
        if ((movement.z < 0 && movement.z >= -1) && (movement.x < 0 && movement.x >= -1))
        {
            Move_Vector(dir_b_left, dir_b_left);
        }

        // µÚ¿À
        if ((movement.z < 0 && movement.z >= -1) && (movement.x > 0 && movement.x <= 1))
        {
            Move_Vector(dir_b_right, dir_b_right);
        }

        // ¾Õ
        if ((movement.z > 0 && movement.z <= 1) && (movement.x == 0))
        {
            Move_Vector(dir_forward, dir_forward);
        }

        // µÚ
        if ((movement.z < 0 && movement.z >= -1) && (movement.x == 0))
        {
            Move_Vector(dir_back, dir_back);
        }

        // ¿À
        if ((movement.x > 0 && movement.x <= 1) && (movement.z == 0))
        {
            Move_Vector(dir_right, dir_right);
        }

        // ¿Þ
        if ((movement.x < 0 && movement.x >= -1) && (movement.z == 0))
        {
            Move_Vector(dir_left, dir_left);
        }

        if (impact.magnitude > 0.2)
            cc.Move(impact * Time.deltaTime);

        impact = Vector3.Lerp(impact, Vector3.zero, 5 * Time.deltaTime);
    }

    void Move_Vector(Vector3 input_vector, Vector3 rotate_vector)
    {
        Quaternion newRotation = Quaternion.LookRotation(rotate_vector);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, newRotation, 6.0f * Time.deltaTime);
        cc.Move(input_vector * (move_speed * Time.deltaTime) + new Vector3(0.0f, velocity, 0.0f) * Time.deltaTime);
    }

    IEnumerator Jump(float forward, float force)
    {
        is_jump = true;
        //velocity = Mathf.Sqrt(jump_speed * -2.0f * -15.0f);
        velocity = -2.0f;
        if (movement.z == 0 && movement.x == 0)
        {
            StartCoroutine(Add_Impact(transform.up * jump_speed, force));
        }
        else
        {
            StartCoroutine(Add_Impact(transform.forward / forward + (transform.up * jump_speed), force));
        }
        yield return new WaitForSeconds(0.2f);
        is_jump = false;
    }

    void Check_Ground()
    {
        Vector3 ground_pos = transform.position - new Vector3(0, ground_height, 0);
        Vector3 wall_pos = transform.position + new Vector3(0, 1f, 0);
        is_ground = Physics.CheckSphere(ground_pos, ground_range, ground_hit, QueryTriggerInteraction.Ignore);
        Debug.DrawRay(wall_pos, transform.forward * 1.2f, Color.blue);
        if (Physics.Raycast(wall_pos, transform.forward, out wall_hit, 1.2f))
        {
            if (wall_hit.transform.tag == "Ground")
            {
                is_wall = true;
            }
        }
        else
        {
            is_wall = false;
        }
    }

    IEnumerator Add_Impact(Vector3 dir, float force)
    {
        dir.Normalize();
        if (dir.y < 0)
            dir.y = -dir.y;
        impact += dir.normalized * force / mass;
        yield return 0;
    }
}
