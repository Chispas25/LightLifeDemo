using UnityEngine;
using UnityEngine.InputSystem;

public class AtkDef : MonoBehaviour
{
    private Animator attackAnimator;
    public int framesToWait = 30;
    private int currentFrame = 0;
    private bool isAttacking = false;

    private void Start()
    {
        attackAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isAttacking)
        {
            currentFrame++;

            if (currentFrame > framesToWait)
            {
                currentFrame = 0;
                attackAnimator.SetBool("Attack", false);
                isAttacking = false;
            }
        }
    }

    // Este método se llamará desde el PlayerInput
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            attackAnimator.SetBool("Attack", true);
            isAttacking = true;
            currentFrame = 0;
        }
    }
}
