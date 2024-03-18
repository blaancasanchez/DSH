using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamagable
{
    protected bool dead;
    protected float health;
    public float healthStart;
    public delegate void OnDeath();
    public static event OnDeath onDeathAnother;
    public delegate void GonnaDie();
    public static event GonnaDie gonnadie;

    public void TakeHit (float damage, RaycastHit hit)
    {
        TakeDamage(damage);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0 && !dead)
        {
            Die();
        }
    }

    public void Die()
    {
        dead = true;
        if(gonnadie != null)
        {
            gonnadie();
        }
        StartCoroutine(EsperarAntesDeContinuar());
        if(onDeathAnother != null)
        {
            onDeathAnother();
        }
        
        
    }
    // Start is called before the first frame update
    public virtual void Start()
    {
        health = healthStart;
        dead = false;
    }

    IEnumerator EsperarAntesDeContinuar()
    {
        Debug.Log("Este es el primer mensaje.");

        // Esperar 3 segundos
        yield return new WaitForSeconds(4.6f);

        Destroy(gameObject);

        Debug.Log("Este es el segundo mensaje, después de esperar 3 segundos.");
    }

}