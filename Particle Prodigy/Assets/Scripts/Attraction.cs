using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attraction : Force
{
    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (objectManager.physicsEnabled)
        {
            //AttractAllOfAttraction();

            AttractAll();
        }
    }

    /// <summary>
    /// attract another object to this object using newton's law of gravitation
    /// </summary>
    /// <param name="objAttracting">The object to attract.</param>
    private void Attract(Attraction objAttracting)
    {
        //If within max distance?
        Rigidbody2D bodyToAttract = objAttracting.rigidBody;

        Vector3 direction = rigidBody.position - bodyToAttract.position;
        float distance = direction.magnitude;

        float forceMagnitude = (rigidBody.mass * bodyToAttract.mass) / Mathf.Pow(distance, 2);
        Vector3 gravitationalForce = direction.normalized * forceMagnitude;
        bodyToAttract.AddForce(gravitationalForce);
    }

    /// <summary>
    /// attract all objects with this script to this object
    /// </summary>
    private void AttractAllOfAttraction()
    {
        Attraction[] attractions = FindObjectsOfType<Attraction>();
        foreach (Attraction attraction in attractions)
        {
            if (gameObject != attraction.gameObject)
            {
                Attract(attraction);
            }
        }
    }

    /// <summary>
    /// attracts all particles based on subatomic forces
    /// </summary>
    private void AttractAll()
    {
        //neutrons and protons attract
        if (particle == Particle.neutron)
        {
            AttractEachTo(objectManager.protons);
        }
        if(particle == Particle.proton)
        {
            AttractEachTo(objectManager.neutrons);
        }

        //atom attraction logic
        if(particle == Particle.atom)
        {
            int charge = GetComponent<Atom>().charge;

            //electrons are attracted to positively charged atoms
            if (charge > 0)
            {
                AttractEachTo(objectManager.electrons);
            }

            //atoms of opposite charge attract
            foreach(Atom atom in objectManager.atoms)
            {
                if(atom.charge * charge < 0)
                {
                    Attract(atom.gameObject.GetComponent<Attraction>());
                }
            }
        }

    }
    /// <summary>
    /// Attract all objects in a given list of GameObjects to this object.
    /// </summary>
    /// <param name="objsAttracting">The list of objects to attract.</param>
    private void AttractEachTo(List<GameObject> objsAttracting)
    {
        foreach (GameObject objAttracting in objsAttracting)
        {
            Attract(objAttracting.GetComponent<Attraction>());
        }
    }
}
