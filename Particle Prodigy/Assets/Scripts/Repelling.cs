using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repelling : Force
{
    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (objectManager.physicsEnabled)
        {
            //RepelAllOfRepelling();

            RepelAll();
        }
    }

    /// <summary>
    /// repel another object from this object using newton's law of gravitation
    /// </summary>
    /// <param name="objRepelling">The object to repel.</param>
    private void Repel(Repelling objRepelling)
    {
        //If within max distance?
        Rigidbody2D bodyToRepel = objRepelling.rigidBody;

        Vector3 direction = rigidBody.position - bodyToRepel.position;
        float distance = direction.magnitude;

        float forceMagnitude = (rigidBody.mass * bodyToRepel.mass) / Mathf.Pow(distance, 2);
        Vector3 gravitationalForce = direction.normalized * forceMagnitude * -1;
        bodyToRepel.AddForce(gravitationalForce);
    }

    /// <summary>
    /// repel all objects with this script from this object
    /// </summary>
    public void RepelAllOfRepelling()
    {
        Repelling[] repellings = FindObjectsOfType<Repelling>();
        foreach (Repelling repelling in repellings)
        {
            if (gameObject != repelling.gameObject)
            {
                Repel(repelling);
            }
        }
    }

    /// <summary>
    /// repels all particles based on subatomic forces
    /// </summary>
    private void RepelAll()
    {
        //protons repel
        if (particle == Particle.proton)
        {
            foreach(GameObject proton in objectManager.protons)
            {
                if(proton.gameObject != gameObject)
                {
                    Repel(proton.GetComponent<Repelling>());
                }
            }
        }

        //atom repelling logic
        if (particle == Particle.atom)
        {
            int charge = GetComponent<Atom>().charge;

            //electrons repel from negatively charged atoms
            if (charge < 0)
            {
                RepelEachFrom(objectManager.electrons);
            }

            //atoms of same nonzero charge repel
            foreach (Atom atom in objectManager.atoms)
            {
                if (atom.charge * charge > 0 && gameObject != atom.gameObject)
                {
                    Repel(atom.gameObject.GetComponent<Repelling>());
                }
            }
        }

    }
    /// <summary>
    /// Repel all objects in a given list of GameObjects to this object.
    /// </summary>
    /// <param name="objsRepelling">The list of objects to repel.</param>
    private void RepelEachFrom(List<GameObject> objsRepelling)
    {
        foreach (GameObject objRepelling in objsRepelling)
        {
            Repel(objRepelling.GetComponent<Repelling>());
        }
    }
}
