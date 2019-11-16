using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinChecker : MonoBehaviour
{
    public ObjectManager objectManager;
    public Text winText;

    // Start is called before the first frame update
    void Start()
    {
        objectManager = GameObject.FindObjectOfType<ObjectManager>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Atom atom in objectManager.atoms)
        {
            if (atom.neutrons == 1 && atom.protons == 1 && atom.electrons == 1)
            {
                winText.text = "nice job";
            }
        }

        
    }
}
