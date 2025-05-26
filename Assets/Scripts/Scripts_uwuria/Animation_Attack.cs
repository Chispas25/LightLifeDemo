using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_Attack : MonoBehaviour
{
    //animacion
    private Animator attackAnimator;
    public bool Attack = false;

    //audio
    public AudioClip fxhitSarten;
    private AudioSource _audioManager;

    //rango

    //esperar
    public int framesToWait = 30;
    //uwuw
    int currentFrame = 0;


    // Start is called before the first frame update
    void Start()
    {
        attackAnimator = GetComponent<Animator>();
        _audioManager = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        currentFrame++;
        if (Input.GetKeyDown(KeyCode.E))
        {
            attackAnimator.SetBool("Attack", true);
            _audioManager.PlayOneShot(fxhitSarten);
        }
        else
        {

            // currentFrame++;

            if (currentFrame > framesToWait)
            {
                currentFrame = 0;
                attackAnimator.SetBool("Attack", false);

            }

        }
            ;




        }
    }











