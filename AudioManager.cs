using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioClip MergeSound;
    public AudioClip ShootSound;
    public AudioClip FreezeSound;
    public AudioClip WinSound;
    public AudioClip LoseSound;

    AudioSource source;

    void Awake()
    {
        Instance = this;

        source =
            GetComponent<AudioSource>();
    }

    public void Play(AudioClip clip)
    {
        source.PlayOneShot(clip);
    }
}
