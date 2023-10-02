using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceReceiver : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private float drag = 0.3f;

    public Vector3 Movement => Vector3.up * verticalVelocity;

    private Vector3 impact;
    private Vector3 dampingVelocity;
    private float verticalVelocity;

    private void Update()
    {
        if(verticalVelocity < 0 && controller.isGrounded)
        {
            verticalVelocity = Physics.gravity.y * Time.deltaTime;
        }
        else
        {
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }

        impact = Vector3.SmoothDamp(impact, Vector2.zero, ref dampingVelocity, drag);
    }
    public void AddForce(Vector3 force)
    {
        impact += force;
    }
    
}
