using UnityEngine;

public class Sounds : MonoBehaviour
{
    [SerializeField] private AudioSource m_AudioSource;
    // Start is called before the first frame update
    public void LaunchSound(AudioClip audioClip)
    {
        m_AudioSource.clip = audioClip;
        m_AudioSource.Play();
    }
}
