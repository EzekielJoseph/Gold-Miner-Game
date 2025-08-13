using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using TMPro;
using UnityEngine;

public class PortSelect : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public AudioClip buttonClickSfx; // SFX untuk klik
    private AudioSource audioSource;

    string[] ports;

    void Start()
    {
        // Setup AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        GetPorts();
    }

    public void ValueChanged(int index)
    {
        PlayClickSFX(); // Mainkan suara klik setiap ganti pilihan

        if (ports.Length < 1)
        {
            Debug.LogWarning("No ports found");
            return;
        }

        Debug.Log(ports[index]);
        UserDataManager.Instance.Port = ports[index];
        Debug.Log("Selected Port: " + UserDataManager.Instance.Port);
    }

    public void GetPorts()
    {
        PlayClickSFX(); // Mainkan suara klik ketika refresh

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

    void PlayClickSFX()
    {
        if (audioSource != null && buttonClickSfx != null)
        {
            audioSource.PlayOneShot(buttonClickSfx);
        }
    }
}
