using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance;
    public AudioSource SFXSource;

    [Header("Sounds Effects")]
    public AudioClip chargingSFX;
    public AudioClip launchSFX;
    public AudioClip bumperSFX;
    public AudioClip blockSFX;
    public AudioClip loseSFX;

    private bool isCharging = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (SFXSource == null)
            SFXSource = GetComponent<AudioSource>();
    }

    public void StartCharging()
    {
        if (!isCharging && chargingSFX != null)
        {
            isCharging = true;
            SFXSource.clip = chargingSFX;
            SFXSource.loop = true;
            SFXSource.Play();
        }
    }

    public void StopCharging()
    {
        if (isCharging)
        {
            isCharging = false;
            SFXSource.Stop();
            SFXSource.loop = false;
        }
    }

    public void LaunchSFX()
    {
        if (launchSFX != null)
        {
            SFXSource.PlayOneShot(launchSFX);
        }
    }

    public void BlockSFX()
    {
        if (launchSFX != null)
        {
            SFXSource.PlayOneShot(blockSFX);
        }
    }

    public void BumperSFX()
    {
        if (bumperSFX != null)
        {
            SFXSource.PlayOneShot(bumperSFX);
        }
    }

    public void LoseSFX()
    {
        if (bumperSFX != null)
        {
            SFXSource.PlayOneShot(loseSFX);
        }
    }
}
