using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    AudioSource Audiosource;
    Animation animacion;
    // Start is called before the first frame update
    void Start()
    {
        Audiosource = GetComponent<AudioSource>();
        animacion = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            Audiosource.Play();

            animacion.wrapMode = WrapMode.Once;
            animacion.Play();
        }
    }
}
