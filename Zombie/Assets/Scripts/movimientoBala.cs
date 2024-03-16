using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimientoBala : MonoBehaviour
{
    public float speed = 120.0f;
    public LayerMask capaDestruir;
    public delegate void OnHitEnemy();
    public static event OnHitEnemy onHitEnemy;
    float damage = 1.0f;

    // Update is called once per frame
    void Update()
    {
        float moveDistancia = Time.deltaTime * speed;
        transform.Translate(Vector3.forward * moveDistancia);
        CheckCollision(moveDistancia);
    }

    void CheckCollision(float movedDistancia)
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, movedDistancia, capaDestruir, QueryTriggerInteraction.Collide)){

            Destroy(gameObject);
            
            if(onHitEnemy != null)
            {
                onHitEnemy();//quiero que lo coja ordas;
            }

            IDamagable damagebleObject = hit.collider.GetComponent<IDamagable>();
            if(damagebleObject != null)
            {
                damagebleObject.TakeHit(damage, hit);
            }

            //if(hit.collider.tag == "Enemigo"){
                //Destroy(hit.collider.gameObject);

            //}
            
        }
    }
}
