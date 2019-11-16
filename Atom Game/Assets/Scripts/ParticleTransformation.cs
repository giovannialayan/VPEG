﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTransformation : MonoBehaviour
{
    public ObjectManager objectManager;

    private bool isProton = false;
    private bool isNeutron = false;
    private bool isElectron = false;
    private bool isNucleus = false;

    private void Start()
    {
        objectManager = GameObject.FindObjectOfType<ObjectManager>();

        if(gameObject.tag == "proton")
        {
            isProton = true;
        }
        else if(gameObject.tag == "neutron")
        {
            isNeutron = true;
        }
        else if (gameObject.tag == "electron")
        {
            isElectron = true;
        }
        else if(gameObject.tag == "nucleus")
        {
            isNucleus = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string collType = collision.gameObject.tag;

        //create a nucleus if a proton and neutron collide
        if (isProton && collType == "neutron")
        {
            //create new nucleus
            GameObject newNucleus = objectManager.InstantiateAtom(1,1,0, gameObject.transform.position);
            newNucleus.tag = "nucleus";

            //remove proton and neutron from object mananger's lists
            int indexToRemove = objectManager.protons.IndexOf(gameObject);
            objectManager.protons.RemoveAt(indexToRemove);
            indexToRemove = objectManager.neutrons.IndexOf(collision.gameObject);
            objectManager.neutrons.RemoveAt(indexToRemove);

            //destroy proton and neutron
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        //create an atom if a nucleus and an electron collide
        else if (isNucleus && collType == "electron")
        {
            //create new atom
            objectManager.InstantiateAtom(1, 1, 1, gameObject.transform.position);

            //remove nucleus and electron from object manager's lists
            int indexToRemove = objectManager.atoms.IndexOf(gameObject.GetComponent<Atom>());
            objectManager.atoms.RemoveAt(indexToRemove);
            indexToRemove = objectManager.electrons.IndexOf(collision.gameObject);
            objectManager.electrons.RemoveAt(indexToRemove);

            //destroy nucleus and electron
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }

    }
}
