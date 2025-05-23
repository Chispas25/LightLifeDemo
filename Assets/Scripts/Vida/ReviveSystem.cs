using UnityEngine;
using System.Collections.Generic;

public class ReviveSystem : MonoBehaviour
{
    public float reviveRadius = 2f;
    public LayerMask playerLayer;

    private PlayerHealthManager myHealth;
    private Dictionary<PlayerHealthManager, float> reviveTimers = new();

    void Start()
    {
        myHealth = GetComponent<PlayerHealthManager>();
    }

    void Update()
    {
        if (myHealth.IsDead()) return;

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, reviveRadius, playerLayer);
        foreach (Collider2D hit in hits)
        {
            if (hit.gameObject == this.gameObject) continue;

            PlayerHealthManager other = hit.GetComponent<PlayerHealthManager>();
            if (other != null && other.IsDead())
            {
                if (!reviveTimers.ContainsKey(other))
                    reviveTimers[other] = 0f;

                reviveTimers[other] += Time.deltaTime;

                if (reviveTimers[other] >= other.reviveDuration)
                {
                    other.Revive();
                    reviveTimers.Remove(other);
                }
            }
        }

        // Reset timer si el jugador ya no está cerca
        List<PlayerHealthManager> toRemove = new();
        foreach (var pair in reviveTimers)
        {
            if (Vector2.Distance(transform.position, pair.Key.transform.position) > reviveRadius)
                toRemove.Add(pair.Key);
        }
        foreach (var key in toRemove)
            reviveTimers.Remove(key);
    }
}
