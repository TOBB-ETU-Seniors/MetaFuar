using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackToPlayer : MonoBehaviour
{
    public Transform target; // the object to track (player)
    public float speed = 3.0f; // the speed at which to rotate the menu

    void Update()
    {
        // get the direction to the target
        Quaternion direction = target.rotation;

        transform.rotation = Quaternion.Lerp(transform.rotation, direction, speed * Time.deltaTime);
    }
}
