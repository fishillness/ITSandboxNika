using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip[] clips;
    [SerializeField] private AudioSource audioSource; 

    void Start()
    {
       audioSource.clip = clips[0];
       audioSource.Play();
    }

    void Update()
    {
        //CheckEnd();
    }

    public void CheckEnd()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = clips[Random.Range(0, clips.Length)];
            audioSource.Play();
        }
    }
}
