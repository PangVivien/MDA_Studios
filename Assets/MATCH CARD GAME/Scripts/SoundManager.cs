using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public AudioSource audioSource;

    [Header("Sound Effects SFX")]
    public AudioClip clickSFX;
    public AudioClip winSFX;
    public AudioClip loseSFX;
    public AudioClip cardFlipSFX;
    public AudioClip cardPairedSFX;

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

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    public void PlayClick()
    {
        if (audioSource != null && clickSFX != null)
        {
            audioSource.PlayOneShot(clickSFX);
        }
    }
    public void PlayWin()
    {
        if (audioSource != null && winSFX != null)
        {
            audioSource.PlayOneShot(winSFX);
        }
    }

    public void PlayLose()
    {
        if (audioSource != null && loseSFX != null)
        {
            audioSource.PlayOneShot(loseSFX);
        }
    }
    public void PlayCardFlip()
    {
        if (audioSource != null && cardFlipSFX != null)
        {
            audioSource.PlayOneShot(cardFlipSFX);
        }
    }
    public void PlayCardPaired()
    {
        if (audioSource != null && cardPairedSFX != null)
        {
            audioSource.PlayOneShot(cardPairedSFX);
        }
    }

    public void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
