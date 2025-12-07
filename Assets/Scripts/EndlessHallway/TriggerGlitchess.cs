using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerGlitchess : MonoBehaviour
{
    public Transform player;
    [HideInInspector] public bool debugTele;
    public TriggerWallLoop tWL;

    private void Update()
    {
        if (debugTele)
        {
            tWL.Teleport();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerObj") && !debugTele)
        {
            debugTele = true;
        }
    }
}
