using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleSelection : MonoBehaviour
{
    public ObjectManager objectManager;
    public SpriteRenderer toolbarRenderer;
    public Camera cam;
    private GameObject currentParticle;
    private Particle particle;
    private bool particleInHand = false;

    //set particle to which ever particle object this script is attatched to
    void Start()
    {
        cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();

        if (gameObject.tag == "proton")
        {
            particle = Particle.proton;
        }
        else if (gameObject.tag == "electron")
        {
            particle = Particle.electron;
        }
        else if (gameObject.tag == "neutron")
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
        currentParticle = objectManager.InstantiateSP(particle, mousePoint);
        particleInHand = true;
    }

    //when the players stops dragging drop the particle
    private void OnMouseUp()
    {
        particleInHand = false;

        //If current particle is placed back in the toolbar, destroy it.
        if (toolbarRenderer.bounds.Contains((Vector2)(currentParticle.transform.position)))
        {
            Destroy(currentParticle);
        }

        currentParticle = null;
    }
}
