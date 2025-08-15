using UnityEngine;

public class HookSound : MonoBehaviour
{
    public AudioClip catchSfx;
    private AudioSource audioSource;

    // Daftar tag yang memicu SFX
    public string[] targetTags =
    {
        "LargeGold",
        "MiddleGold",
        "SmallGold",
        "LargeStone",
        "MiddleStone"
    };

    void Start()
    {
        // Ambil AudioSource dari Hook, kalau belum ada, buat baru
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hook menyentuh: " + collision.gameObject.name);

        // Cek apakah tag-nya ada di daftar targetTags
        foreach (string tag in targetTags)
        {
            if (collision.CompareTag(tag))
            {
                if (catchSfx != null && audioSource != null)
                {
                    audioSource.PlayOneShot(catchSfx);
                }
                break; // Keluar loop biar nggak cek tag lain
            }
        }
    }
}
