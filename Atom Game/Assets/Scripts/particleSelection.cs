using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleSelection : MonoBehaviour
{
    public ObjectManager objectManager;
    private Particle particle;
    public Camera cam;
    private bool particleInHand = false;
    private GameObject currentParticle;

    //set particle to which ever particle object this script is attatched to
    void Start()
    {
        if (gameObject.name == "Proton")
        {
            particle = Particle.proton;
        }
        else if (gameObject.name == "Electron")
        {
            particle = Particle.electron;
        }
        else if (gameObject.name == "Neutron")
        {
            particle = Particle.neutron;
        }
    }

    void Update()
    {
        //make the particle the player is dragging follow the mouse
        if (particleInHand)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 1;
            Vector3 mousePoint = cam.ScreenToWorldPoint(mousePos);
            currentParticle.transform.position = mousePoint;
        }
    }

    //when the player clicks on the particle in the toolbar instantiate a new object of that type on the cursor
    private void OnMouseDown()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 mousePoint = cam.ScreenToWorldPoint(mousePos);
        mousePos.z = 1;
        currentParticle = objectManager.InstantiateSubParticle(particle, mousePoint);
        particleInHand = true;
    }

    //when the players stops dragging drop the particle
    private void OnMouseUp()
    {
        particleInHand = false;
        currentParticle = null;
    }
}
