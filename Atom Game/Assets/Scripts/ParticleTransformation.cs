using System.Collections;
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
        if (isProton && collType == "neutron")
        {
            GameObject newNucleus = objectManager.InstantiateAtom(1,1,0, gameObject.transform.position);
            newNucleus.tag = "nucleus";
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        else if (isNucleus && collType == "electron")
        {
            objectManager.InstantiateAtom(1, 1, 1, gameObject.transform.position);
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }

    }
}
