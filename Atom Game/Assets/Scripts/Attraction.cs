using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attraction : MonoBehaviour
{
    //rigid bodies of objects with this script attatched
    public Rigidbody2D rigidBody;

    //Will objects attract and do all that stuff they usually do?
    public bool physicsEnabled;

    //the ObjectManager of the current scene
    private ObjectManager objectManager;

    //the type of particle this object is
    private Particle particle;

    private void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();

        objectManager = FindObjectOfType<ObjectManager>();

        physicsEnabled = true;

        //set Particle based on which List in objectManager it is in
        if (objectManager.protons.Contains(gameObject))
        {
            particle = Particle.proton;
        }
        else if (objectManager.neutrons.Contains(gameObject))
        {
            particle = Particle.neutron;
        }
        else if(objectManager.electrons.Contains(gameObject))
        {
            particle = Particle.electron;
        }
        else
        {
            particle = Particle.atom;
        }
    }

    private void FixedUpdate()
    {
        //AttractAllOfAttraction();

        //if: is playing.
        //if: within max distance.
        if (physicsEnabled)
        {
            AttractAll();
        }
    }

    /// <summary>
    /// attract another object to this object using newton's law of gravitation
    /// </summary>
    /// <param name="objAttracting">The object to attract.</param>
    private void Attract(Attraction objAttracting)
    {
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
            if (gameObject.name != attraction.name)
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
