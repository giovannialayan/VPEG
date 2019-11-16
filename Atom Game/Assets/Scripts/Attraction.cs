using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attraction : MonoBehaviour
{
    //rigid bodies of objects with this script attatched
    public Rigidbody2D rigidBody;

    private void FixedUpdate()
    {
        //attract all objects with this script to this object
        Attraction[] attractions = FindObjectsOfType<Attraction>();
        foreach(Attraction attraction in attractions)
        {
            if (gameObject.name != attraction.gameObject.name)
            {
                Attract(attraction);
            }
        }
    }

    //attract another object to this object using newton's law of gravitation
    private void Attract(Attraction objAttracting)
    {
        Rigidbody2D bodyToAttract = objAttracting.rigidBody;

        Vector3 direction = rigidBody.position - bodyToAttract.position;
        float distance = direction.magnitude;

        float forceMagnitude = (rigidBody.mass * bodyToAttract.mass) / Mathf.Pow(distance, 2);
        Vector3 gravitationalForce = direction.normalized * forceMagnitude;
        bodyToAttract.AddForce(gravitationalForce);
    }
}
