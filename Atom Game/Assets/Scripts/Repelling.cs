using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repelling : MonoBehaviour
{
    //rigid bodies of objects with this script attatched
    public Rigidbody2D rigidBody;

    private void FixedUpdate()
    {
        //attract all objects with this script to this object
        Repelling[] repels = FindObjectsOfType<Repelling>();
        foreach (Repelling repelling in repels)
        {
            if (gameObject.name != repelling.gameObject.name)
            {
                Repel(repelling);
            }
        }
    }

    //attract another object to this object using newton's law of gravitation
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
