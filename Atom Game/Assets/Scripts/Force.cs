using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Force : MonoBehaviour
{
    //rigid bodies of objects with this script attatched
    public Rigidbody2D rigidBody;

    //the ObjectManager of the current scene
    protected ObjectManager objectManager;

    //the type of particle this object is
    protected Particle particle;

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
}
