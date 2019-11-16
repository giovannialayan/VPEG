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

    // Start is called before the first frame update
    void Start()
    {
        protons = new List<GameObject>();
        neutrons = new List<GameObject>();
        electrons = new List<GameObject>();
        atoms = new List<Atom>();

        /*test instantiation (comment me out pls)*/
        InstantiateSP(Particle.proton, new Vector3(0, 0, 0));
        InstantiateSP(Particle.electron, new Vector3(3, 3, 0));
        InstantiateSP(Particle.neutron, new Vector3(-3, -3, 0));
        InstantiateAtom(3, 3, 2, new Vector3(3, 0, 0));
        InstantiateAtom(2, 3, 3, new Vector3(0, 3, 0));
        //*/
    }

    /// <summary>
    /// instantiates a sub particle
    /// </summary>
    /// <param name="particle">Particle to instantiate (sub particle)</param>
    /// <param name="position">position in world space to instantiate prefab at</param>
    public GameObject InstantiateSP(Particle particle, Vector3 position)
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
    public void InstantiateAtom(int protons, int neutrons, int electrons, Vector3 position)
    {
        atoms.Add(Instantiate(atomPrefab, position, Quaternion.identity).GetComponent<Atom>());
        atoms[atoms.Count - 1].Init(protons, neutrons, electrons);
    }
}
