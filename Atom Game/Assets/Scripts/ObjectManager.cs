using System.Collections.Generic;
using UnityEngine;

public enum Particle { proton, neutron, electron, atom }

public class ObjectManager : MonoBehaviour
{
    public GameObject protonPrefab;
    public GameObject neutronPrefab;
    public GameObject electronPrefab;
    public GameObject atomPrefab;

    //data structures to hold all particle objects in the current scene
    public List<GameObject> protons;
    public List<GameObject> neutrons;
    public List<GameObject> electrons;
    public List<Atom> atoms;

    /// <summary>
    /// Can objects attract / repel / etc.?
    /// </summary>
    public bool physicsEnabled;

    // Start is called before the first frame update
    void Start()
    {
        protons = new List<GameObject>();
        neutrons = new List<GameObject>();
        electrons = new List<GameObject>();
        atoms = new List<Atom>();

        physicsEnabled = true;

        /*test instantiation (comment me out pls)*/
        InstantiateSubParticle(Particle.proton, new Vector3(-2, -2, 0));
        InstantiateSubParticle(Particle.proton, new Vector3(2, 2, 0));
        InstantiateSubParticle(Particle.electron, new Vector3(4, 3, 0));
        InstantiateSubParticle(Particle.electron, new Vector3(6, 4, 0));
        InstantiateSubParticle(Particle.neutron, new Vector3(-3, -3, 0));
        InstantiateAtom(3, 3, 2, new Vector3(-5, 3, 0));
        InstantiateAtom(5, 5, 2, new Vector3(-5, 0, 0));
        InstantiateAtom(2, 3, 3, new Vector3(0, 3, 0));
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            physicsEnabled = true;
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
}
