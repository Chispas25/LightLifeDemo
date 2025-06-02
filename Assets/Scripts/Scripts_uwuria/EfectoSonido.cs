using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;


public class EfectoSonido : MonoBehaviour
{

    //public Animation_movement movement;
    //public Animation_Attack animationSarten;

    //sonidos bolas
    public bool controlador = false;


    //audios bolita (el de la sarten esta en Animation_movement)
    public AudioClip fxHit;
    public AudioClip fxDeath;
    public AudioClip fxRecuperarVida;
    public AudioClip fxSarten;

    //audios enemigos
    public AudioClip fxenemyAttack;
    public AudioClip fxdamagetoEnemy;
    public AudioClip fxBone;

    //audios extra
    public AudioClip fxbotonesmenuInicio;
    public AudioClip fxenergiaPuzzleGenerador;
    public AudioClip fxPuzzleLavadora;
    public AudioClip fxPickUp_inventory;
    

    public AudioSource _audioManager;



    // Start is called before the first frame update
    void Start()
    {
        _audioManager = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //este if solo esta para poner las lineas de codigo de los sonidos en el codigo bueno sin que explote nada(por si acaso)
        if (controlador == true)
        {
            _audioManager.PlayOneShot(fxRecuperarVida);
            _audioManager.PlayOneShot(fxHit);
            _audioManager.PlayOneShot(fxDeath);
            _audioManager.PlayOneShot(fxSarten);

            _audioManager.PlayOneShot(fxenemyAttack);
            _audioManager.PlayOneShot(fxdamagetoEnemy);
            _audioManager.PlayOneShot(fxBone);

            _audioManager.PlayOneShot(fxbotonesmenuInicio);
            _audioManager.PlayOneShot(fxenergiaPuzzleGenerador);
            _audioManager.PlayOneShot(fxPuzzleLavadora);
            _audioManager.PlayOneShot(fxPickUp_inventory);
            
        }

    }
    
}
