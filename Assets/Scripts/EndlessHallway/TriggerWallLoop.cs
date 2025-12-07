using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerWallLoop : MonoBehaviour
{
    public Transform player;
    public Transform otherTriggerWall;
    public TriggerGlitchess triggerGlitchess;
    private TriggerWallLoop triggerWallLoop;
    private bool teleporting;

    private void Start()
    {
        triggerWallLoop = otherTriggerWall.GetComponent<TriggerWallLoop>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerObj") && !teleporting)
        {
            Teleport();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerObj") && teleporting)
        {
            teleporting = false;
            
        }
    }

    public void Teleport()
    {
        teleporting = true;
        triggerWallLoop.teleporting = true;
        Vector3 offset = new Vector3(player.position.x - transform.position.x,
        player.position.y - transform.position.y,
        player.position.z - transform.position.z
        );

        player.position = otherTriggerWall.position + offset;
        triggerGlitchess.debugTele = false;
    }
}
