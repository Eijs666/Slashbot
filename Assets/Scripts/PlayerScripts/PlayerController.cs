using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    #region Variables
    Rigidbody rb;

    Vector3 velocity;
    #endregion


    void Start()
    {
        rb = GetComponent<Rigidbody>();   
    }

    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    //Object looking at mouse direction
    public void LookAt(Vector3 lookPoint)
    {
        Vector3 heightCorrectedPoint = new Vector3(lookPoint.x, transform.position.y, lookPoint.z);
        transform.LookAt(heightCorrectedPoint);
    }

    //Physics
    public void FixedUpdate()
    {
        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
    }
}
