using UnityEngine;

public class Music : MonoBehaviour
{
    [SerializeField] private AudioClip[] clips;
    [SerializeField] private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
       // audioSource.clip = clips[0];
       // audioSource.Play();
    }

    // Update is called once per frame
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
