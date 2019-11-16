using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinChecker : MonoBehaviour
{
    ObjectManager objectManager;

    // Start is called before the first frame update
    void Start()
    {
        objectManager = GameObject.FindObjectOfType<ObjectManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
