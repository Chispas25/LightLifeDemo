using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_anim_attack : MonoBehaviour
{
    private Animator attackAnimator;
    public bool Attack = false;

    //orientacion bichi
    private Rigidbody2D rb;
    public float detectionDistance = 5f;
    public LayerMask playerLayer;


    //esperar
    public int framesToWait = 30;
    //uwuw
    int currentFrame = 0;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
    bool LookForPlayer(out Transform detectedPlayer)
    {
        Collider2D hit = Physics2D.OverlapCircle(rb.position, detectionDistance, playerLayer);
        if (hit != null)
        {
            detectedPlayer = hit.transform;
            return true;
        }

        detectedPlayer = null;
        return false;
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(rb.position, detectionDistance);
    }










}