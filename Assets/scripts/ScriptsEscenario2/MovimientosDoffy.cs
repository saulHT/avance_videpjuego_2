using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using Image = UnityEngine.UI.Image;
using Random = System.Random;
    

public class MovimientosDoffy : MonoBehaviour
{
    public float velocityX = 4;
    
    private Rigidbody2D rb;
  private Animator animator;
    private SpriteRenderer spriteRenderer;
    public GameObject ataquePiso;
    private int LanzarAtaquePiso=2;
  

  public AudioClip[] audioClips;
  private AudioSource audioSource;
 
    private const int animacion_idle = 0;
    private const int animacion_walk = 1;
    private const int animacion_run = 2;
    private const int animacion_ataque_araniaso = 3;
   // private const int animacion_ataque_patada = 5;
    private const int animacion_ataque_piso = 4;
    private const int animacion_ataque_sable =6 ;
    private const int animacion_ataque_puas = 5;

    public const int LUFFYCHOCA = 8;
    public const int PISO = 9;

    private int contadorAtaques = 0;
    public bool EstaAtacando=false;
    public string DirJugador;
     private luffyController luffy;
    private Transform[] transforms;
    
    private float tiempoDetectar=4, cuentaBajo;
    private float tiempoTeleport=3, cuentaBajoTeleport;
    
    
    public  Image barraSaludLuffyImg;
    private float VidaLuffy, VidaActualLuffy;

    public Image barraSaludDoflamingo;

    private float VidaDoflamingo, VidaActualDoflamingo;
    
    // Start is called before the first frame update
    
    // public float velocidad = 4;
    // private static readonly int Run = Animator.StringToHash("run");
    // private static readonly int Walk = Animator.StringToHash("walk");
    // private static readonly int Attack = Animator.StringToHash("attack");
    private bool golpe1=false;
    
   
    void Start()
    { 
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
       spriteRenderer = GetComponent<SpriteRenderer>();
       luffy= luffyController.instance;
       
        ubicarPlayer();
    
    }

    // Update is called once per frame
    void Update()
    {
       
     
        // if (golpe1==true)
        // {
        //     CambiarAnimacion(animacion_ataque_araniaso);
        //     
        // }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = new Vector2(velocityX, rb.velocity.y);
            spriteRenderer.flipX = true;
            CambiarAnimacion(animacion_walk);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity = new Vector2(- velocityX, rb.velocity.y);
            spriteRenderer.flipX = false;
            CambiarAnimacion(animacion_walk);
        }
        // EstaAtacando=true;
        // if(EstaAtacando==true){
        //     CambiarAnimacion(LanzarAtaquePiso);
        //     Invoke("AtaquePiso", 0.01f);
        //    Invoke("isAttack",0.01f);
        //
        //     
        // }
        //
        if(EstaAtacando==false){
            CambiarAnimacion(0);
        }

        contador();
        // if (Input.GetKeyUp(KeyCode.L))
        // {
        //     CambiarAnimacion(animacion_ataque_piso);
        //     var position = new Vector2(luffyController.instance.transform.position.x, luffyController.instance.transform.position.y);
        //     Instantiate(ataquePiso,position, ataquePiso.transform.rotation);
        // }

    }

    

    public void telepor(){
        var initialPosition= UnityEngine.Random.Range(0, transforms.Length);
        transform.position = transforms[initialPosition].position;
         cuentaBajo = tiempoDetectar;
         cuentaBajoTeleport =tiempoTeleport;
    }
    
    public void AtaquePiso()
    {
        var position = new Vector2(luffyController.instance.transform.position.x, luffyController.instance.transform.position.y);
        Instantiate(ataquePiso,position, ataquePiso.transform.rotation);
    }


   
    
    public void ubicarPlayer(){
 
        if (transform.position.x > luffyController.instance.transform.position.x)
        {
          //  transform.localScale= new Vector3(0.6706f,0.6034f,1);
            Debug.Log("esta a la izquierda");
            DirJugador="izquierda";
        
        }else
        {
         //   transform.localScale= new Vector3(-0.6706f,0.6034f,1);
            Debug.Log("esta a la derecha");
            DirJugador="derecha";
             
        }
 
    }

   
    public void AtaqueDoflamingo()
    {
        var distancia=transform.position.x-luffyController.instance.transform.position.x;
        if(distancia>15){
          
            EstaAtacando=true;
            CambiarAnimacion(4);
            if(EstaAtacando==true){
               
                Invoke("AtaquePiso", 0.01f);
                Invoke("isAttack",0.01f); 
            }

        }

        if (distancia <=14 && distancia >10)
        {
            EstaAtacando = true;
            if (EstaAtacando == true)
            {
               CambiarAnimacion(animacion_ataque_sable);
               Invoke("isAttack",1.0f); 
            }
        }

        if (distancia < 5)
        {
            EstaAtacando = true;
            if (EstaAtacando == true)
            {
                CambiarAnimacion(5);
                Invoke("isAttack",1.0f); 
            }
        }
    }

    public void isAttack(){
        EstaAtacando=false;  
    }
    
    
    public void contador()
    {
        cuentaBajo-=Time.deltaTime;
        cuentaBajoTeleport-=Time.deltaTime;
        if (cuentaBajo<=2f)
        {
            AtaqueDoflamingo();
            ubicarPlayer();
            cuentaBajo=tiempoDetectar;
          

        }
        if (cuentaBajoTeleport<=2f)
        {
             
            ubicarPlayer();
            cuentaBajoTeleport=tiempoTeleport;
 
        }
    }
    

    private void CambiarAnimacion(int animacion)
    {
       animator.SetInteger("Estado", animacion);
        
    }

    
    public void DañoRecibeLuffy()
    {
        //vidaFrezer=GetComponent<FrezerController>
        VidaLuffy=0.2f;
        VidaActualLuffy = VidaActualLuffy-VidaLuffy;
        barraSaludLuffyImg.fillAmount = VidaActualLuffy ;
    }
    
    public void DañoRecibeDoflamingo()
    {
        //vidaFrezer=GetComponent<FrezerController>
        VidaDoflamingo=0.2f;
        VidaActualDoflamingo = VidaActualDoflamingo-VidaDoflamingo;
        barraSaludDoflamingo.fillAmount = VidaActualDoflamingo ;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "luffy")
        {
            DañoRecibeLuffy();
        }else if(other.gameObject.tag== "doflamin")
        {
            DañoRecibeDoflamingo();
        }
        
    }
}


    