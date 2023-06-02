using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    public float range, distanceFromPlayer, stopDistance;
    public EnemyData data;

    protected PlayerMovement player;
    protected AIDestinationSetter destinationSetter;
    protected AIPath path;

    void Awake()
    {
        destinationSetter = GetComponent<AIDestinationSetter>();
        path = GetComponent<AIPath>();
        path.slowdownDistance = stopDistance;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    void SetDistance()
    {
        distanceFromPlayer = Vector2.Distance(transform.position, player.transform.position);
    }

    public virtual void OnUpdate() {}
    public virtual void OnDrawGizmosEv() {}

    // Update is called once per frame
    void Update()
    {
        SetTarget();
        SetDistance();

        OnUpdate();
    }

    public void SetTarget()
    {
        if(player != null && Vector2.Distance(transform.position, player.transform.position) < range)
        {
            destinationSetter.target = player.transform;
        }
    }

    public virtual void Attack() {}

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, range);
        Gizmos.DrawWireSphere(transform.position, stopDistance);

        OnDrawGizmosEv();
    }
}
