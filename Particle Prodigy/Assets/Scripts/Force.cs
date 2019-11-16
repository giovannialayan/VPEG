using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Force : MonoBehaviour
{
    //rigid bodies of objects with this script attatched
    public Rigidbody2D rigidBody;

    //The collider of this object.
    public CircleCollider2D circleCollider;

    //the ObjectManager of the current scene
    protected ObjectManager objectManager;

    //the type of particle this object is
    public Particle particle;

    //the scale of the force applied to all attration and repelling
    public float forceCoefficient = 3;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();

        objectManager = FindObjectOfType<ObjectManager>();

        //set Particle based on which List in objectManager it is in
        if (objectManager.protons.Contains(gameObject))
        {
            particle = Particle.proton;
        }
        else if (objectManager.neutrons.Contains(gameObject))
        {
            particle = Particle.neutron;
        }
        else if (objectManager.electrons.Contains(gameObject))
        {
            particle = Particle.electron;
        }
        else
        {
            particle = Particle.atom;
        }
    }

    protected virtual void FixedUpdate()
    {
        //If the collider is enabled, make the collider a trigger when physics isn't enabled
        if (circleCollider.enabled)
        {
            circleCollider.isTrigger = !objectManager.physicsEnabled;
        }
    }
}
