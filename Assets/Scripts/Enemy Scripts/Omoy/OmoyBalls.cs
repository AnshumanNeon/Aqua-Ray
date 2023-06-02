using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OmoyBalls : MonoBehaviour
{
    public float hitForce, radius, upwardsModifier;
    void OnCollisionEnter2D(Collision2D collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            Rigidbody2Dext.AddExplosionForce(collider.gameObject.GetComponent<Rigidbody2D>(), hitForce, transform.position, radius, upwardsModifier, ForceMode2D.Impulse);
            OmoyEnemy.HitBalls += 1;
            Destroy(gameObject);
        }
    }
}
