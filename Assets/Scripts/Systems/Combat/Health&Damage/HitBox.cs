using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    [SerializeField] private Collider ownerCollider;

    private List<Collider> collidersAlredyHit = new List<Collider>();

    private int damage;

    private void OnEnable()
    {
        collidersAlredyHit.Clear();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other == ownerCollider) return;

        if (collidersAlredyHit.Contains(other)) return;


        if (other.TryGetComponent<Health>(out Health health))
        {
            health.DealDamage(damage);
            collidersAlredyHit.Add(other);
        }
    }

    public void SetDamage(int damageValue)
    {
        damage = damageValue;
    }
}
