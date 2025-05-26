using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class AudioInicial : MonoBehaviour
{


    public AudioClip bandaSonora;
    public AudioClip fxButton;
    AudioSource _audioSource;

    AudioSource audioMusic;

    public static AudioInicial Instance;


    public AudioMixerSnapshot defaultSnapshot;



    void Awake()
    {

        if(Instance != null && Instance != this){
            Destroy(this);
        }else{
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }


    }

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = this.GetComponent<AudioSource>();


        _audioSource.clip = bandaSonora;
        _audioSource.loop = true;
        _audioSource.volume = 0.1f;
        _audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void SonarClipUnaVez(AudioClip ac)
    {

        _audioSource.PlayOneShot(ac);

    }



    public void IniciarEfectoDefault()
    {
        defaultSnapshot.TransitionTo(0.05f);
    }




}
