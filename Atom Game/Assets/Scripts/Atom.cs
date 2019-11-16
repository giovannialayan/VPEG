using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atom : MonoBehaviour
{
    //the charge of the atom, negative or positive, zero for stable
    public int charge;

    //number of sub particles in the Atom
    public int protons;
    public int neutrons;
    public int electrons;

    //changes based on the number of particles contained
    public float diameter;

    //the coefficient that determines how much the diameter scales logarithmically
    protected float dCoefficient = 3f;

    //initializes the number of sub particles, as well as diameter and charge
    public void Init(int protons, int neutrons, int electrons)
    {
        this.protons = protons;
        this.neutrons = neutrons;
        this.electrons = electrons;

        CalcDiameter();

        charge = protons + (-electrons);
    }

    //calculates and sets diameter based on the number of sub particles, increasing logarithmically
    public void CalcDiameter()
    {
        diameter = 1.5f + Mathf.Log(protons + neutrons, dCoefficient);
        gameObject.transform.localScale = new Vector3(diameter, diameter, 0);
    }
}
