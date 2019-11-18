using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ParticleSelection : MonoBehaviour
{
    public ObjectManager objectManager;
    public SpriteRenderer toolbarRenderer;
    public Camera cam;
    private GameObject currentParticle;
    private CircleCollider2D currentCollider;
    private SpriteRenderer currentRenderer;
    private Particle particle;
    private bool particleInHand = false;
    private bool isStartButton = false;
    private bool isResetButton = false;
    private bool canMakeParticles = true;

    //set particle to which ever particle object this script is attatched to
    void Start()
    {
        cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        objectManager = GameObject.FindObjectOfType<ObjectManager>();

        if (gameObject.name == "ProtonButton")
        {
            particle = Particle.proton;
        }
        else if (gameObject.name == "ElectronButton")
        {
            particle = Particle.electron;
        }
        else if (gameObject.name == "NeutronButton")
        {
            particle = Particle.neutron;
        }
        else if (gameObject.name == "ResetButton")
        {
            isResetButton = true;
        }
        else if(gameObject.name == "StartButton")
        {
            isStartButton = true;
        }
    }

    void Update()
    {
        canMakeParticles = !objectManager.physicsEnabled;

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
        if (isResetButton)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else if (!isStartButton)
        {
            if (canMakeParticles)
            {
                Vector3 mousePos = Input.mousePosition;
                Vector3 mousePoint = cam.ScreenToWorldPoint(mousePos);
                mousePos.z = 1;
                currentParticle = objectManager.InstantiateSubParticle(particle, mousePoint);

                currentCollider = currentParticle.GetComponent<CircleCollider2D>();
                currentCollider.enabled = false;

                currentRenderer = currentParticle.GetComponent<SpriteRenderer>();
                currentRenderer.sortingLayerName = "dragged particles";

                particleInHand = true;
            }
        }
        else
        {
            objectManager.TogglePhysics(true);
        }
    }

    //when the players stops dragging drop the particle
    private void OnMouseUp()
    {
        if (!isResetButton && !isStartButton)
        {
            particleInHand = false;

            if (currentParticle)
            {
                //If current particle is placed back in the toolbar, destroy it.
                if (toolbarRenderer.bounds.Contains((Vector2)currentParticle.transform.position))
                {
                    //Destroy(currentParticle);
                    objectManager.DestroyParticle(currentParticle);
                }
                else
                {
                    currentCollider.enabled = true;
                    currentRenderer.sortingLayerName = "particles";
                }
            }

            currentParticle = null;
            currentCollider = null;
            currentRenderer = null;
        }
    }
}
