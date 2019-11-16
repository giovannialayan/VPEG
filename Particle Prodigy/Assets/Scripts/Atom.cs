using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atom : MonoBehaviour
{
    /// <summary>
    /// The charge of the atom, negative or positive, zero for stable
    /// </summary>
    public int charge;

    //number of sub particles in the Atom
    public int protons;
    public int neutrons;
    public int electrons;

    /// <summary>
    /// Changes based on the number of particles contained
    /// </summary>
    public float diameter;

    /// <summary>
    /// the coefficient that determines how much the diameter scales logarithmically
    /// </summary>
    protected float dCoefficient = 3f;

    /// <summary>
    /// initializes the number of sub particles, as well as diameter and charge
    /// </summary>
    /// <param name="protons">The number of protons.</param>
    /// <param name="neutrons">The number of neutrons.</param>
    /// <param name="electrons">The number of electrons.</param>
    public void Init(int protons, int neutrons, int electrons)
    {
        this.protons = protons;
        this.neutrons = neutrons;
        this.electrons = electrons;

        CalcDiameter();

        charge = protons + (-electrons);
    }

    /// <summary>
    /// calculates and sets diameter based on the number of sub particles, increasing logarithmically
    /// </summary>
    public void CalcDiameter()
    {
        diameter = 1.5f + Mathf.Log(protons + neutrons, dCoefficient);
        gameObject.transform.localScale = new Vector3(diameter, diameter, 0);
    }
}
