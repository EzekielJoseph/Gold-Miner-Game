using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using TMPro;
using UnityEngine;

public class PortSelect : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    string[] ports;

    void Start()
    {
        GetPorts();
    }

    public void ValueChanged(int index)
    {

        if (ports.Count() < 1)
        {
            Debug.LogWarning("No ");
            return;
        }

        Debug.Log(ports[index]);
        UserDataManager.Instance.Port = ports[index];
        Debug.Log("Selected Port: " + UserDataManager.Instance.Port);

    }

      public void GetPorts()
    {
        Debug.Log("Refresh");
        ports = SerialPort.GetPortNames();

        List<TMP_Dropdown.OptionData> optiondata = new List<TMP_Dropdown.OptionData>();

        for (int i = 0; i < ports.Length; i++)
        {
            TMP_Dropdown.OptionData val = new TMP_Dropdown.OptionData();
            val.text = ports[i];
            optiondata.Add(val);
        }

        dropdown.options = optiondata;
        ValueChanged(0);
    }



}
