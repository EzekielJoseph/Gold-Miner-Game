using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;

public class Data : MonoBehaviour
{
    public string nama;
    public string email;
    public int umur;
    public string domisili;

    public Data(string nama, string email, int umur, string domisili)
    {
        this.nama = nama;
        this.email = email;
        this.umur = umur;
        this.domisili = domisili;
    }

    public virtual void TampilkanDebug()
    {
        Debug.Log("Nama: " +  nama);
        Debug.Log("Email: " +  email);
        Debug.Log("Umur: " +  umur);
        Debug.Log("Domisili: " +  domisili);
    }
}
