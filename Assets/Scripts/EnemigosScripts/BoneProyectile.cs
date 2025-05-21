using UnityEngine;

public class BoneProjectile : MonoBehaviour
{
    public float speed = 6f;
    public float lifeTime = 3f;
    public int damage = 1;

    private float timer = 0f;
    private Vector2 moveDirection = Vector2.zero;

    public void SetDirection(Vector2 direction)
    {
        moveDirection = direction.normalized;
        // No rotamos el proyectil porque la animación ya lo representa visualmente
    }

    void Update()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);

        timer += Time.deltaTime;
        if (timer >= lifeTime)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // other.GetComponent<PlayerHealth>()?.TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (!other.isTrigger)
        {
            Destroy(gameObject);
        }
    }
}
