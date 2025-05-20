using UnityEngine;

public class BoneProjectile : MonoBehaviour
{
    public float speed = 6f;
    public float lifeTime = 3f;
    public int damage = 1;

    private float timer = 0f;

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        timer += Time.deltaTime;

        if (timer >= lifeTime)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //other.GetComponent<PlayerHealth>()?.TakeDamage(damage);
            //Aqui tambien influye la vida
            Destroy(gameObject);
        }
        else if (!other.isTrigger)
        {
            Destroy(gameObject); // Se destruye al chocar con muros, etc.
        }
    }
}
