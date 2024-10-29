using UnityEngine;

public class SoundsPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource m_AudioSource; 

    public void LaunchSound(AudioClip audioClip)
    {
        m_AudioSource.clip = audioClip;
        m_AudioSource.Play();
    }
}
