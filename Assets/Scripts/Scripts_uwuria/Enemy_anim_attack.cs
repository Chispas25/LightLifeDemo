using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_anim_attack : MonoBehaviour
{
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
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        attackAnimator.SetFloat("Horizontal", moveX);
        attackAnimator.SetFloat("Vertical", moveY);

        currentFrame++;
        if (Input.GetKeyDown(KeyCode.P))
        {
            attackAnimator.SetBool("LookforPlayer", true);

        }
        else
        {

            // currentFrame++;

            if (currentFrame > framesToWait)
            {
                currentFrame = 0;
                attackAnimator.SetBool("LookforPlayer", false);

            }

        }
            ;




        }
    }