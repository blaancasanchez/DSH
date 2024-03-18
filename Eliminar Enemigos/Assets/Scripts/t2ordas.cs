using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class t2ordas : MonoBehaviour
{
    public valoresenemigos[] valoresEnemigos;
    public valoresenemigos enemigoActual;
    float tiempoEspera = 0;
    int numOrdaActual = 0;
    int enemigosporCrear = 0;
    int enemigosporMatar = 0;
    // Start is called before the first frame update
    void Start()
    {
        NextOrda();
        LivingEntity.onDeathAnother += EnemigoMuerto;
    }

    void NextOrda()
    {
        numOrdaActual++;
        enemigoActual = valoresEnemigos[numOrdaActual - 1];
        enemigosporCrear = enemigoActual.numeroEnemigos;
        enemigosporMatar = enemigoActual.numeroEnemigos;
    }

    void EnemigoMuerto()
    {
        enemigosporMatar --;
        if(enemigosporMatar  <= 0)
        {
            NextOrda();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(enemigosporCrear > 0 && tiempoEspera <=0)
        {
            Instantiate(enemigoActual.tipoEnemigo, Vector3.zero , Quaternion.identity);
            enemigosporCrear--;
            tiempoEspera = enemigoActual.tiempoEntreEnemigos;
        } 
        else
        {
            tiempoEspera -= Time.deltaTime;
        }
    }
}
