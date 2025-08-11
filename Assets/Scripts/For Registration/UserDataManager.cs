using UnityEngine;

public class UserDataManager : MonoBehaviour
{
    public static UserDataManager Instance;
    public string Port;

    public Data userData; // class Data kamu yang berisi nama, email, dll

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // agar tidak hilang saat pindah scene
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetData(Data data)
    {
        userData = data;
    }

    public Data GetData()
    {
        return userData;
    }
}