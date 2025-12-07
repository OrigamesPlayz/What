using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gambling : MonoBehaviour
{
    public PlayerMovement pMove;
    private bool readyToChange;
    private float randomJumpForce;

    private void Start()
    {
        readyToChange = true;
    }

    private void Update()
    {
        if (!pMove.readyToJump && (readyToChange || pMove.grounded))
        {
            readyToChange = false;
            randomJumpForce = Random.Range(2f, 12f);
            pMove.jumpCooldown = 2f * (pMove.jumpForce / Mathf.Abs(Physics.gravity.y));
            pMove.jumpForce = randomJumpForce;
        }

        if (pMove.readyToJump)
        {
            readyToChange = true;
        }
    }
}
