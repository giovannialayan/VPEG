using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeOverlay : MonoBehaviour
{
    private Atom atom;
    private TextMesh chargeMesh;
    private string chargeString;

    public Font font;


    // Start is called before the first frame update
    void Start()
    {
        atom = GetComponent<Atom>();
        chargeString = "";

        chargeMesh.font = font;
        chargeMesh.fontSize = 24;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //set chargeMesh to the charge of the Atom
        if (atom.charge >= 0)
        {
            chargeString = "+ ";
        }
        else
        {
            chargeString = "- ";
        }

        chargeString.Insert(2, "" + Mathf.Abs(atom.charge));
    }
}
