using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenBounce : MonoBehaviour
{
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            rb.AddForce(0, 10f, 0, ForceMode.Impulse);
        }
    }
}
