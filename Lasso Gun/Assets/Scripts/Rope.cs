using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    private Spring spring;
    private LineRenderer lr;
    public LassoGun lassoGun;
    public GameObject gun;
    private Vector3 currentPos;
    public int quality;
    public float damper, strength, velocity, waveCount, waveHeight;
    public AnimationCurve affectCurve;
    void Awake()
    {
        lr = gun.GetComponent<LineRenderer>();
        spring = new Spring();
        spring.SetTarget(0);
    }
    void LateUpdate()
    {
        DrawRope();
    }
    void DrawRope()
    {
        if(!lassoGun.IsGrappling()) 
        {
            currentPos = lassoGun.muzzle.position;
            spring.Reset();
            if (lr.positionCount > 0)
                lr.positionCount = 0;
                return;
        }
        if (lr.positionCount == 0)
            {
                spring.SetVelocity(velocity);
                lr.positionCount = quality + 1;
            }

        spring.SetDamper(damper);
        spring.SetStrength(strength);
        spring.Update(Time.deltaTime);

        var grapplePoint = lassoGun.GetGrapplePoint();
        var muzzlePosition = lassoGun.muzzle.position;
        var up = Quaternion.LookRotation((grapplePoint - muzzlePosition).normalized) * Vector3.up;

        currentPos = Vector3.Lerp(currentPos, grapplePoint, Time.deltaTime * 12f);

        for (var i = 0; i < quality + 1; i++) 
        {
            var delta = i / (float) quality;
            var offset = up * waveHeight * Mathf.Sin(delta * waveCount * Mathf.PI) * spring.Value *
                         affectCurve.Evaluate(delta);
            
            lr.SetPosition(i, Vector3.Lerp(muzzlePosition, currentPos, delta) + offset);
        }
    }
}
