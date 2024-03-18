using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hordas : MonoBehaviour
{
    public ValoresEnemigos[] valoresEnemigos;
    public ValoresEnemigos enemigoActual;
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
            Vector3 PosEnemy = new Vector3(691.969971f,138.089996f,135.050003f);
            Instantiate(enemigoActual.tipoEnemigo, PosEnemy, Quaternion.identity);
            enemigosporCrear--;
            tiempoEspera = enemigoActual.tiempoEntreEnemigos;
        } 
        else
        {
            tiempoEspera -= Time.deltaTime;
        }
    }
}
