using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum particle { proton, neutron, electron}

public class ObjectManager : MonoBehaviour
{
    public GameObject protonPrefab;
    public GameObject neutronPrefab;
    public GameObject electronPrefab;

    //data structures to hold all particle objects in the current scene
    public List<GameObject> protons;
    public List<GameObject> neutrons;
    public List<GameObject> electrons;

    // Start is called before the first frame update
    void Start()
    {
        protons = new List<GameObject>();
        neutrons = new List<GameObject>();
        electrons = new List<GameObject>();

        //test instantiation (comment me out)
        InstantiateObject(particle.proton, new Vector3(0, 0, 0));
        InstantiateObject(particle.electron, new Vector3(3, 3, 0));
        InstantiateObject(particle.neutron, new Vector3(-3, -3, 0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// instantiates a particle
    /// </summary>
    /// <param name="particle">type of particle to instantiate</param>
    /// <param name="position">position in world space to instantiate prefab at</param>
    public void InstantiateObject(particle particle, Vector3 position)
    {
        switch (particle)
        {
            case particle.proton:
                protons.Add(Instantiate(protonPrefab, position, Quaternion.identity));
                break;
            case particle.neutron:
                neutrons.Add(Instantiate(neutronPrefab, position, Quaternion.identity));
                break;
            case particle.electron:
                neutrons.Add(Instantiate(electronPrefab, position, Quaternion.identity));
                break;
        }
    }
}
