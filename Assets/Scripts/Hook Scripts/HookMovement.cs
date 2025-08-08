using System.IO.Ports;
using UnityEngine;

public class HookMovement : MonoBehaviour
{

    // rotation Z
    public float min_Z = -55f, max_Z = 55f;
    public float rotate_Speed = 5f;

    private float rotate_Angle;
    private bool rotate_Right;
    private bool canRotate;

    public float move_Speed = 3f;
    private float initial_Move_Speed;

    public float min_Y = -2.5f;
    private float initial_Y;

    private bool moveDown;

    SerialPort serialPort = new SerialPort("COM11", 115200);
    private string serialInput = "";

    // FOR LINE RENDERER
    private RopeRenderer ropeRenderer;

    void Awake()
    {
        ropeRenderer = GetComponent<RopeRenderer>();
    }

    void Start()
    {
        initial_Y = transform.position.y;
        initial_Move_Speed = move_Speed;

        canRotate = true;

        // Open Serial Port
        try
        {
            if (!serialPort.IsOpen)
            {
                serialPort.Open();
                serialPort.ReadTimeout = 50; // Set a read timeout
                Debug.Log("Serial port opened successfully.");
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to open serial port: " + e.Message);
        }
    }

    void Update()
    {
        ReadSerialInpput();
        Rotate();
        GetInput();
        MoveRope();
    }

    void ReadSerialInpput()
    {
        if (serialPort.IsOpen)
        {
            try
            {
                serialInput = serialPort.ReadLine();
                if (!string.IsNullOrEmpty(serialInput))
                {
                    Debug.Log("Received from ESP: " + serialInput);
                }
            }
            catch (System.TimeoutException)
            {
            }
        }
    }

    void GetInput()
    {
        bool mousePressed = Input.GetMouseButton(0);
        bool espPressed = (serialInput == "FIRE");

        if ((mousePressed || espPressed) && canRotate)
        {
            canRotate = false;
            moveDown = true;
            serialInput = "";
        }

    } // get input

    void Rotate()
    {

        if (!canRotate)
        {
            return;
        }

        if (rotate_Right)
        {
            rotate_Angle += rotate_Speed * Time.deltaTime;
        }
        else
        {
            rotate_Angle -= rotate_Speed * Time.deltaTime;
        }

        transform.rotation = Quaternion.AngleAxis(rotate_Angle, Vector3.forward);

        if (rotate_Angle >= max_Z)
        {
            rotate_Right = false;
        }
        else if (rotate_Angle <= min_Z)
        {
            rotate_Right = true;
        }

    } // can rotate

    void MoveRope()
    {

        if (canRotate)
        {
            return;
        }

        if (!canRotate)
        {
            Vector3 temp = transform.position;

            if (moveDown)
            {
                temp -= transform.up * Time.deltaTime * move_Speed;
            }
            else
            {
                temp += transform.up * Time.deltaTime * move_Speed;
            }

            transform.position = temp;

            if (temp.y <= min_Y)
            {
                moveDown = false;
            }

            if (temp.y >= initial_Y)
            {
                canRotate = true;
                ropeRenderer.RenderLine(temp, false);
                move_Speed = initial_Move_Speed;
            }

            ropeRenderer.RenderLine(transform.position, true);

        } // cannot rotate


    } // move rope

    public void HookAttachedItem()
    {
        moveDown = false;
    }
    private void OnDestroy()
    {
        CloseSerialPort();
    }

    void CloseSerialPort()
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            try
            {
                serialPort.Close();
                Debug.Log("Serial port ditutup.");
                System.Threading.Thread.Sleep(200); // beri waktu OS melepas port
            }
            catch (System.Exception e)
            {
                Debug.LogWarning("Gagal menutup port: " + e.Message);
            }
        }
    }


} // class