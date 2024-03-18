using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Enemigo : LivingEntity
{
    // Start is called before the first frame update
    UnityEngine.AI.NavMeshAgent pathfinder;
    Transform target;

    float myCollisionradius;
    float TargetCollisionradius;

    float DistanciadeAtaque = 1.3f;
    float NextAttackTime = 0;
    float TimeBetweenAttack = 0.5f;
    bool Atacando = false;
    Material materialInicial;
    Color colorOriginal;

    LivingEntity targetEntity;
    float damage = 1;
    bool muriendo = false;
    bool bFinalPartida = false;

    private Animator animator;

    public override void Start()
    {
        base.Start();
        target =GameObject.FindGameObjectWithTag("Player").transform;
        pathfinder = GetComponent<UnityEngine.AI.NavMeshAgent>();
        materialInicial = GetComponent<Renderer>().material;
        colorOriginal = materialInicial.color;
        animator = GetComponent<Animator>();

        myCollisionradius = GetComponent<CapsuleCollider>().radius;
        TargetCollisionradius = target.GetComponent<CapsuleCollider>().radius;
        targetEntity = target.GetComponent<LivingEntity>();
        JugadorController.OnDeathPlayer += FinalPartida;

        LivingEntity.gonnadie += Semuere;

    }
        void FinalPartida()
    {
        bFinalPartida = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(!bFinalPartida){
            if(!Atacando){
                Vector3 dirtoTarget = (target.position - transform.position).normalized;
                Vector3 TargetPosition = target.position - dirtoTarget * (myCollisionradius + TargetCollisionradius + DistanciadeAtaque);
                Debug.Log(muriendo);
                if(!muriendo)
                {
                    pathfinder.SetDestination(TargetPosition);

                    if(Time.time > NextAttackTime)
                    {
                        NextAttackTime = Time.time + TimeBetweenAttack;
                        float sqrDsttoTarget = (target.position - transform.position).sqrMagnitude;
                        if (sqrDsttoTarget <= Mathf.Pow(myCollisionradius + TargetCollisionradius + DistanciadeAtaque, 2))
                        {
                            Debug.Log("estoy al lado");
                            StartCoroutine(Attack());
                            
                        }
                    }
                }
            } 
        }
    }

    void Semuere()
    {   
        muriendo = true;
        animator.SetBool("Dead", true);
        pathfinder.SetDestination(transform.position);
        //StartCoroutine(EsperarAntesDeContinuar());    
    }

    /*
    // Update is called once per frame
    void Update()
    {
        pathfinder.SetDestination(target.position);
    }*/

        IEnumerator Attack()
        {
            if(!bFinalPartida){
                pathfinder.enabled = false;
                Atacando = true;
                materialInicial.color = Color.red;

                Vector3 originalPosition = transform.position;
                Vector3 dirtoTarget = (target.position - transform.position).normalized;
                Vector3 AttackPosition = target.position - dirtoTarget * (myCollisionradius + TargetCollisionradius);

                float percent = 0;
                float attackSpeed = 0.5f;

                bool hasAppliedDamage = false;
                while(percent <= 1)
                {
                    animator.SetBool("Attacking", true);
                    if(percent >= 0.5f && !hasAppliedDamage)
                    {                    
                        targetEntity.TakeDamage(damage);
                        hasAppliedDamage = true;
                    }
                    percent += Time.deltaTime * attackSpeed;
                    float interpolacion = (-Mathf.Pow(percent,2)+percent)*4;
                    transform.position = Vector3.Lerp(originalPosition, AttackPosition, interpolacion);
                    yield return null;
                    
                }
                pathfinder.enabled = true;
                Atacando = false;
                materialInicial.color = colorOriginal;
                animator.SetBool("Attacking", false);
            }
        }
        /*IEnumerator EsperarAntesDeContinuar()
        {
            Debug.Log("Este es el segundo mensaje.");

            // Esperar 3 segundos
            yield return new WaitForSeconds(4.6f);

            muriendo = false;
            animator.SetBool("Dead", false); 
            

            Debug.Log("Este es el tercer mensaje, después de esperar 3 segundos.");
        }*/
}
