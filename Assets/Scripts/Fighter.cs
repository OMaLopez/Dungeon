using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    //Basic Stats
    public int hitpoints = 10;
    public int maxHitpoints = 10;
    public float pushRecoverySpeed = 0.2f;

    //Inmunity
    protected float inmuneTime = 1.0f;
    protected float lastInmune;

    protected Vector3 pushDirection;

    protected virtual void RecieveDamage(Damage dmg)
    {
        if (Time.time - lastInmune > inmuneTime)
        {
            lastInmune = Time.time;
            hitpoints -= dmg.damageAmount;
            pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce;

            GameManager.instance.ShowText(dmg.damageAmount.ToString(), 25, Color.red, transform.position, Vector3.zero, 0.5f);

            if (hitpoints <= 0)
            {
                hitpoints = 0;
                Death();
            }

        }

    }

    protected virtual void Death()
    {

    }
}
