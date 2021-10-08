using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip audioclip;
    public bool isLoop;

    private void Start()
    {
        AudioSource audio = gameObject.AddComponent<AudioSource>();
        audio.clip = audioclip;
        audio.playOnAwake = false;
        audio.loop = isLoop;
    }

    public void PlaySound()
    {
        GetComponent<AudioSource>().Play();
    }

    public void StopSound()
    {
        GetComponent<AudioSource>().Stop();
    }
}
