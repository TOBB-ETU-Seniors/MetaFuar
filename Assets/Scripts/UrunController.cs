using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class UrunController : MonoBehaviour
{

    Vector3 rotation_offset = new Vector3(0, 1, 0);
    public GameObject panel;


    


    // Start is called before the first frame update
    void Start()
    {

        

        this.panel.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.Rotate(rotation_offset, Space.Self);
    }

    void OnTriggerEnter(Collider collider)
    {
        // character triggers when get close
        if (collider.gameObject.tag != "Character")
            return;


        this.panel.SetActive(true);

        /*
         
         Karakterin kapsulune collider ekledik istrigger tick attik
         Rigidbody zaten var

         kapsule yine Character Tag' ini ekledik
         */
        
    }

    void OnTriggerExit(Collider collider)
    {
        // character triggers when get close
        if (collider.gameObject.tag != "Character")
            return;
        Debug.Log("Character exited...");
        this.panel.SetActive(false);


        // remove panel from character


    }



}
