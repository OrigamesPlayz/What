using UnityEngine;
using System.Collections;

public class FlipRoom : MonoBehaviour
{
    public Transform player;
    public Transform groundCheck;
    public ConstantForce cForce;
    public Transform cam;
    public Transform camPos;
    public PlayerMovement pMove;

    private bool flipRequested;
    private bool readyToFlip;
    [HideInInspector] public bool flipped;

    private void Start()
    {
        readyToFlip = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            flipRequested = true;
    }

    private void FixedUpdate()
    {
        if (flipRequested && readyToFlip)
        {
            Flip();
            flipRequested = false;
            readyToFlip = false;
            StartCoroutine(FlipCooldown());
        }
    }

    private void Flip()
    {
        player.position = new Vector3(
            player.position.x,
            -player.position.y,
            player.position.z
        );

        camPos.localPosition = new Vector3(
            camPos.localPosition.x,
            -camPos.localPosition.y,
            camPos.localPosition.z
        );

        cForce.force = new Vector3(
            cForce.force.x,
            -cForce.force.y,
            cForce.force.z
        );

        groundCheck.localPosition = new Vector3(
            groundCheck.localPosition.x,
            -groundCheck.localPosition.y,
            groundCheck.localPosition.z
        );

        pMove.jumpForce = -pMove.jumpForce;
        flipped = !flipped;
    }

    private IEnumerator FlipCooldown()
    {
        yield return new WaitForSeconds(0.67f);
        readyToFlip = true;
    }
}
