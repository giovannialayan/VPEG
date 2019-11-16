using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeOverlay : MonoBehaviour
{
    private Atom atom;
    public GameObject textCanvas;
    private string chargeString;



    // Start is called before the first frame update
    void Start()
    {
        //Instantiate the text canvas prefab and initialize its values
        atom = GetComponent<Atom>();
        chargeString = "";
        textCanvas = Instantiate(textCanvas, gameObject.transform);
        Camera cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        Canvas canvas = textCanvas.GetComponent<Canvas>();
        canvas.worldCamera = cam;
        canvas.sortingLayerName = "particles";
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //set the text to the charge of the Atom
        if (atom.charge >= 0)
        {
            chargeString = "+";
        }
        else
        {
            chargeString = "-";
        }

        chargeString += Mathf.Abs(atom.charge).ToString();

        textCanvas.GetComponentInChildren<Text>().text = chargeString;
    }
}
