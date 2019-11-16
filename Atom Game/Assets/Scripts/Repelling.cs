using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repelling : Force
{
    private void FixedUpdate()
    {
        //repel all objects with this script from this object
        Repelling[] repels = FindObjectsOfType<Repelling>();
        foreach (Repelling repelling in repels)
        {
            if (gameObject.name != repelling.gameObject.name)
            {
                Repel(repelling);
            }
        }
    }

    /// <summary>
    /// repel another object from this object using newton's law of gravitation
    /// </summary>
    /// <param name="objRepelling">The object to repel.</param>
    private void Repel(Repelling objRepelling)
    {
        Rigidbody2D bodyToRepel = objRepelling.rigidBody;

        Vector3 direction = rigidBody.position - bodyToRepel.position;
        float distance = direction.magnitude;

        float forceMagnitude = (rigidBody.mass * bodyToRepel.mass) / Mathf.Pow(distance, 2);
        Vector3 gravitationalForce = direction.normalized * forceMagnitude * -1;
        bodyToRepel.AddForce(gravitationalForce);
    }
}
