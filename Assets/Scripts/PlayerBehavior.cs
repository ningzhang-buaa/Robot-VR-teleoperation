using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public float moveSpeed = 1f;

    private float vinput;
    private float hinput;
    
    // Update is called once per frame
    void Update()
    {
        vinput = Input.GetAxis("Vertical") * moveSpeed;
        hinput = Input.GetAxis("Horizontal") * moveSpeed;
        this.transform.Translate(Vector3.forward * vinput * Time.deltaTime);
        this.transform.Translate(Vector3.up * hinput * Time.deltaTime);
    }
}
