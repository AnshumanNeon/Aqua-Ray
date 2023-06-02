using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandSplash : MonoBehaviour
{
    ParticleSystem ps;
    public List<ParticleCollisionEvent> collisionEvents;

    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnParticleCollision(GameObject other)
    {
        if(other.layer == LayerMask.GetMask("Ground") || other.tag == "Player")
        {
            int numCollisionEvents = ps.GetCollisionEvents(other, collisionEvents);
            int i = 0;

            while(i < numCollisionEvents)
            {
                i++;
            }
        }
    }
}
