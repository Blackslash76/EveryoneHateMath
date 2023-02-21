using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicManager : MonoBehaviour
{
    [SerializeField] AudioClip[] music;
    [SerializeField] AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        int index = Random.Range(0, music.Length);
        audioSource.clip = music[index];
        audioSource.loop = true;
        PlayMusic();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayMusic()
    {
        audioSource.Play();
    }
}
