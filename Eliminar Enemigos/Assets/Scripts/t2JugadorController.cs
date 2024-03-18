using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Rigidbody))]
public class jugador : LivingEntity
{
    
    public Camera mainCamera;
    DisparaBala controladorBalas; 

    public delegate void OnDeathJugador();
    public static event OnDeathJugador OnDeathPlayer;



    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        controladorBalas = GetComponent<DisparaBala>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundplane = new Plane(Vector3.up, Vector3.zero);
        if (groundplane.Raycast(ray, out float rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);
            Debug.DrawLine(ray.origin, point, Color.red);
            transform.LookAt(new UnityEngine.Vector3(point.x, transform.position.y, point.z));
        }

        if (Input.GetMouseButtonDown(0)){
            Debug.Log("Se ha disparado");
            controladorBalas.Dispara();
        }
    }

    void OnDestroy()
    {
        if(OnDeathPlayer != null)
        {
            OnDeathPlayer();
        }
    }

}