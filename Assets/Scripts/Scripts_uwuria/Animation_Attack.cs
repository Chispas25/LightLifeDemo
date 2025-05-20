using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_Attack : MonoBehaviour
{
    //tes
    private Animator attackAnimator;
    public bool Attack = false;

    //esperar
    public int framesToWait = 30;
    //uwuw
    int currentFrame = 0;


    // Start is called before the first frame update
    void Start()
    {
        attackAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        currentFrame++;
        if (Input.GetKeyDown(KeyCode.E))
        {
            attackAnimator.SetBool("Attack", true);

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











