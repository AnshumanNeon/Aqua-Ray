using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OmoyEnemy : Enemy
{
    public GameObject balls;

    public float shootForce;

    public float shootDistance;

    bool didShot = false;

    public float timeBetweenShots, playerNotInRangeTimeLimit;
    float shotTime;
    float playerNotInRangeTime;

    public static int HitBalls = 0;
    int ballsShot = 0;

    int prevHitBallValue, prevBallShotValue;

    public override void OnUpdate()
    {
        if(didShot)
        {
            shotTime -= Time.deltaTime;
        }

        if(shotTime <= 0) didShot = false;

        if(distanceFromPlayer <= shootDistance && !didShot)
        {
            ThrowBalls();
            didShot = true;
            shotTime = timeBetweenShots;
        }
        
        if(ballsShot > 3 && HitBalls == 0)
        {
            ballsShot = 0;
        }
        else if(prevHitBallValue == HitBalls && prevHitBallValue < ballsShot)
        {
            ballsShot = 0;
        }

        if(destinationSetter.target != null && Vector2.Distance(transform.position, player.transform.position) > range + 3)
        {
            playerNotInRangeTime += Time.deltaTime;
            if(playerNotInRangeTime >= playerNotInRangeTimeLimit) destinationSetter.target = null;
        }

        prevHitBallValue = HitBalls;
        prevHitBallValue = ballsShot;
    }

    void ThrowBalls()
    {
        GameObject ball = Instantiate(balls, transform.position, Quaternion.identity) as GameObject;
        Rigidbody2D ballrb = ball.GetComponent<Rigidbody2D>();
        ballrb.velocity = (player.transform.position - transform.position).normalized * shootForce;
        ballsShot += 1;
    }

    public override void Attack()
    {
    }

    public override void OnDrawGizmosEv()
    {
        Gizmos.DrawWireSphere(transform.position, shootDistance);
    }
}
