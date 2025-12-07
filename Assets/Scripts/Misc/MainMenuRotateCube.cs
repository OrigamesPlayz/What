using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuRotateCube : MonoBehaviour
{
    public Vector3 rotateAmount;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotateAmount * Time.deltaTime);
    }
}
