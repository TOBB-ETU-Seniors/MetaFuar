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

        var textField = this.panel.transform.Find("Canvas").gameObject.transform.Find("Text");
        var header = textField.transform.Find("Header").GetComponent<TMPro.TextMeshProUGUI>();
        var body = textField.transform.Find("Body").GetComponent<TMPro.TextMeshProUGUI>();
        
        // Veri tabanindan cekecek burada metinleri
        body.text = "Deneme";

        var scroll = this.panel.transform.Find("Canvas").gameObject.transform.Find("Scroll View");
        var toggles = scroll.transform.GetChild(0).gameObject.transform.GetChild(0);
        var toggle1 = toggles.transform.GetChild(0);
        var img = toggle1.transform.Find("RawImage").gameObject;
        // read image and store in a byte array
        WWW www = new WWW("https://cataas.com/cat");

        while (!www.isDone)
            Debug.Log("Image wating...."); ;

        //create a texture and load byte array to it
        // Texture size does not matter 
        GameObject image = img;
        image.GetComponent<RawImage>().texture = www.texture;
        

        Debug.Log("Start is called"); ;
        Debug.Log("Character entered...");
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
