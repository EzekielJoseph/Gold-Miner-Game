using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Inputs : MonoBehaviour
{
    public TMP_InputField namaInput;
    public TMP_InputField emailInput;
    public TMP_InputField umurInput;
    public TMP_InputField domisiliInput;

    public GameObject assetPanel; // Panel untuk menampilkan attribution
    public AudioClip buttonClickSfx;
    private AudioSource audioSource;

    void Start()
    {
        if (assetPanel != null)
        {
            assetPanel.SetActive(false); // Hide panel saat scene dimulai
        }

        // Pastikan AudioSource ada di GameObject ini
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    public void OnRegister()
    {
        string nama = namaInput.text;
        string email = emailInput.text;
        int umur = int.Parse(umurInput.text);
        string domisili = domisiliInput.text;

        Data userData = new Data(nama, email, umur, domisili);
        userData.TampilkanDebug();

        PlayClickSFX();

        SceneManager.LoadScene("Main Game");
    }

    void PlayClickSFX()
    {
        if (audioSource != null && buttonClickSfx != null)
        {
            audioSource.PlayOneShot(buttonClickSfx);
        }
    }

    public void OnAssetClick()
    {
        PlayClickSFX(); // Mainkan efek suara saat tombol diklik
        assetPanel.SetActive(true); // Tampilkan panel attribution
    }

    public void OnBackClick()
    {
        PlayClickSFX(); // Mainkan efek suara saat tombol diklik
        assetPanel.SetActive(false); // Sembunyikan panel attribution
    }

    public void OnExitClick()
    {
        PlayClickSFX(); // Mainkan efek suara saat tombol diklik
        Application.Quit(); // Keluar dari aplikasi
    }
}
