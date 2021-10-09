using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip audioclip;
    public bool isLoop;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = audioclip;
        audioSource.playOnAwake = false;
        audioSource.loop = isLoop;
    }

    public void PlaySound()
    {
        audioSource.Play();
    }

    public void StopSound()
    {
        audioSource.Stop();
    }
}
