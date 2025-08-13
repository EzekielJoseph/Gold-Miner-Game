using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;

public class GameManagerSementara : MonoBehaviour
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

    SerialPort serialPort;
    private string serialInput = "";

    // FOR LINE RENDERER
    private RopeRenderer ropeRenderer;
    private bool isPortConnected;


    void Awake()
    {
        ropeRenderer = GetComponent<RopeRenderer>();
    }

    public void ConnectToPort(string port)
    {

        if (string.IsNullOrEmpty(port))
        {
            Debug.LogWarning("No Port Is Selected");
            return;
        }
        serialPort = new SerialPort(port, 115200);
        Debug.Log("Connecting to port: " + port);

        serialPort.ReadTimeout = 1000;
        serialPort.DataReceived += dapatData;

        if (!serialPort.IsOpen)
        {
            try
            {
                serialPort.Open();
                Debug.Log("Serial port opened successfully.");
            }
            catch (System.Exception e)
            {
                Debug.LogError("Failed to open serial port: " + e.Message);
            }
        }

    }

    private void dapatData(object sender, SerialDataReceivedEventArgs e)
    {
        Debug.Log("Data received from ESP");
    }

    IEnumerator Start()
    {
        yield return new WaitForSeconds(5); // wait for the port to be ready
        ConnectToPort("COM10");
        initial_Y = transform.position.y;
        initial_Move_Speed = move_Speed;

        canRotate = true;
        yield return new WaitForSeconds(4); // wait for the initial setup
        isPortConnected = true;
    }

    void Update()
    {
        ReadSerialInput();
        Rotate();
        GetInput();
        MoveRope();
    }

    void ReadSerialInput()
    {
        if(!isPortConnected)
        {
            return;
        }

        if (serialPort == null)
        {
            return;
        }

        if (serialPort.IsOpen)
        {
            //serialInput = serialPort.ReadLine().Trim();
            //if (!string.IsNullOrEmpty(serialInput))
            //{
            //    Debug.Log("Received from ESP: " + serialInput);
            //}
            string data = serialPort.ReadLine();
            Debug.Log("Received from ESP: " + data);
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


}
