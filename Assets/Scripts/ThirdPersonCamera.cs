using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField] Transform subject;
    [SerializeField] Vector3 defaultOffset = new Vector3(0, 2, -10);
    [SerializeField] float interpolationFactor = 0.3f;

    Vector3 velocity = Vector3.one;
    
    void FixedUpdate()
    {
        //Linear interpolation for an elasitc feel, and we could change field of view depending on the speed
        Vector3 nextPosition = subject.position + (subject.rotation * defaultOffset);
        nextPosition.y = defaultOffset.y + subject.position.y; // Quick fix to prevent X axis rotation. Better suited for kart, or maybe platformers too
                                                               // (We could also find a way to only to only keep what we want from subject.rotation)
        transform.position = Vector3.SmoothDamp(transform.position, nextPosition, ref velocity, interpolationFactor);
        
        transform.LookAt(subject, Vector3.up);
    }
}
