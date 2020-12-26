using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Light))]
/*
 * This component switch between the light spot mode when touch with collider of the player.
 */
public class LightSwitch : MonoBehaviour
{
    
    [Tooltip("Player's tag")]
    [SerializeField] string Player = "Player";

    // components
    CapsuleCollider playerCollider;
    Light lighter;

    // init at start
    void Start()
    {
        lighter = GetComponentInParent<Light>();
        playerCollider = GetComponentInParent<CapsuleCollider>();
    }

    // when the player gets into the lights trigger- and clicks on "E" he can to turn on/off the light.
    private void OnTriggerStay(Collider other)
    {
        // in the trigger area
        if (other.tag == Player)
        {
            // clicks "E" and turning off
            if (Input.GetKeyDown(KeyCode.E) && lighter.enabled)
            {
                lighter.enabled = false;
            }
            // clicks "E" and turning on 
            else if (Input.GetKeyDown(KeyCode.E) && (lighter.enabled == false))
            {
                lighter.enabled = true;
            }
        }
    }
}