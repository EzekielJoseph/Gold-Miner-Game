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

    public void OnRegister()
    {
        string nama = namaInput.text;
        string email = emailInput.text;
        int umur = int.Parse(umurInput.text);
        string domisili = domisiliInput.text;

        Data userData = new Data(nama, email, umur, domisili);

        userData.TampilkanDebug();

        SceneManager.LoadScene("Main Game");

    }

}
