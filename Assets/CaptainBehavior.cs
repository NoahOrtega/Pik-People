using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptainBehavior : MonoBehaviour
{
    public float horizontalSpeed = 7f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 toMove = (Vector3.right * horizontalSpeed * Time.deltaTime);

        if (Input.GetKey("left"))
        {
            transform.position -= toMove;
        }
        if (Input.GetKey("right"))
        {
            transform.position += toMove;
        }
    }

}