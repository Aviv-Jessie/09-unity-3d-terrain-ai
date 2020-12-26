using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Renderer))]
/*
 * This component switch between the "Hey" mode when touch with collider of the player.
 */
public class HeySwitcher : MonoBehaviour
{
    [Tooltip("Player's tag")]
    [SerializeField] string Player = "Player";

    // components
    CapsuleCollider playerCollider;
    Renderer Hey;




    // Start is called before the first frame update
    void Start()
    {
        Hey = GetComponentInParent<Renderer>();
        playerCollider = GetComponentInParent<CapsuleCollider>();
        Hey.enabled = false;
    }



    // when the player gets into the friend trigger- and clicks on "E" he can to turn on/off the "Hey".
    private void OnTriggerStay(Collider other)
    {
        // in the trigger area
        if (other.tag == Player)
        {
            // clicks "E" and turning off
            if (Input.GetKeyDown(KeyCode.E))
            {
                Hey.enabled = true;
            }
        }
    }








}
