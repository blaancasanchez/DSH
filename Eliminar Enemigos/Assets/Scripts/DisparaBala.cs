using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparaBala : MonoBehaviour
{
    public GameObject balaPrefab;
    public Transform puntoDisparo;
    public float firerate = 0.5f;
    float nextFire = 0.0f; 

    Animation animacion;
    // Start is called before the first frame update
    void Start()
    {
        animacion = GetComponent<Animation>();
    }

    // Update is called once per frame
    public void Dispara()
    {

        if(Time.time >= nextFire){
            animacion.wrapMode = WrapMode.Once;
            animacion.Play();

            nextFire =  Time.time + firerate;
            GameObject bala = Instantiate(balaPrefab, puntoDisparo.position, puntoDisparo.rotation);
            //bala.GetComponent<Rigidbody>().AddForce(puntoDisparo.forward * 1000f);
        }
    }
}
