﻿using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public enum Particle { proton, neutron, electron, atom }

public class ObjectManager : MonoBehaviour
{
    public GameObject protonPrefab;
    public GameObject neutronPrefab;
    public GameObject electronPrefab;
    public GameObject atomPrefab;

    public SpriteRenderer overlayRenderer;

    //data structures to hold all particle objects in the current scene
    public List<GameObject> protons;
    public List<GameObject> neutrons;
    public List<GameObject> electrons;
    public List<Atom> atoms;

    /// <summary>
    /// Can objects attract / repel / etc.?
    /// </summary>
    public bool physicsEnabled;

    //loophole for atom collision bug
    public int justAtomAtomCollided = 0;

    // Start is called before the first frame update
    void Start()
    {
        protons = new List<GameObject>();
        neutrons = new List<GameObject>();
        electrons = new List<GameObject>();
        atoms = new List<Atom>();

        physicsEnabled = false;
        overlayRenderer.enabled = true;

        /*test instantiation (comment me out pls)
        InstantiateSubParticle(Particle.proton, new Vector3(-2, -2, 0));
        InstantiateSubParticle(Particle.proton, new Vector3(2, 2, 0));
        InstantiateSubParticle(Particle.electron, new Vector3(4, 3, 0));
        InstantiateSubParticle(Particle.electron, new Vector3(6, 4, 0));
        InstantiateSubParticle(Particle.neutron, new Vector3(-3, -3, 0));
        InstantiateAtom(3, 3, 2, new Vector3(-5, 3, 0));
        InstantiateAtom(5, 5, 2, new Vector3(-5, 0, 0));
        InstantiateAtom(2, 3, 3, new Vector3(0, 3, 0));
        */
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //physicsEnabled = true;
            TogglePhysics(true);
        }
    }

    /// <summary>
    /// instantiates a sub particle
    /// </summary>
    /// <param name="particle">Particle to instantiate (sub particle)</param>
    /// <param name="position">position in world space to instantiate prefab at</param>
    public GameObject InstantiateSubParticle(Particle particle, Vector3 position)
    {
        switch (particle)
        {
            case Particle.proton:
                GameObject newProton = Instantiate(protonPrefab, position, Quaternion.identity);
                protons.Add(newProton);
                return newProton;

            case Particle.neutron:
                GameObject newNeutron = Instantiate(neutronPrefab, position, Quaternion.identity);
                neutrons.Add(newNeutron);
                return newNeutron;

            case Particle.electron:
                GameObject newElectron = Instantiate(electronPrefab, position, Quaternion.identity);
                electrons.Add(newElectron);
                return newElectron;

            default:
                return new GameObject();
        }
    }

    /// <summary>
    /// instantiates an Atom
    /// </summary>
    /// <param name="protons">number of protons in the Atom</param>
    /// <param name="neutrons">number of neutrons in the Atom</param>
    /// <param name="electrons">number of electrons in the Atom</param>
    /// <param name="position">position in world space to instantiate prefab at</param>
    public GameObject InstantiateAtom(int protons, int neutrons, int electrons, Vector3 position)
    {
        GameObject newAtom = Instantiate(atomPrefab, position, Quaternion.identity);
        atoms.Add(newAtom.GetComponent<Atom>());
        atoms[atoms.Count - 1].Init(protons, neutrons, electrons);
        return newAtom;
    }

    /// <summary>
    /// Sets physicsEnabled to the opposite of what it currently is.
    /// </summary>
    public void TogglePhysics()
    {
        physicsEnabled = !physicsEnabled;
        overlayRenderer.enabled = !physicsEnabled;
    }

    /// <summary>
    /// Sets physicsEnabled to a given bool.
    /// </summary>
    /// <param name="turnOn">Whether to turn on physics or not.</param>
    public void TogglePhysics(bool turnOn)
    {
        physicsEnabled = turnOn;
        overlayRenderer.enabled = !turnOn;
    }

    /// <summary>
    /// Removes a given particle from whatever list it should be in, and then destroys it.
    /// </summary>
    /// <param name="particle">The particle to destroy.</param>
    public void DestroyParticle(GameObject particle)
    {
        //Note: List.Remove() returns false if the object is not in the list, and thus,
        //causes no errors if the object is not in the list.
        switch (particle.tag)
        {
            case "proton":
                protons.Remove(particle);
                break;

            case "neutron":
                neutrons.Remove(particle);
                break;

            case "electron":
                electrons.Remove(particle);
                break;

            case "nucleus":
            case "atom":
                //This checks to see if particle has the atom script, and if it does, get it and remove it.
                if (particle.TryGetComponent(out Atom particleAtom))
                {
                    atoms.Remove(particleAtom);
                }
                break;

            default:
                break;
        }

        //No matter what, destroy the particle.
        Destroy(particle);
    }
}
