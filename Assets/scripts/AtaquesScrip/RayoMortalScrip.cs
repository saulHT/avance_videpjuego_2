using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayoMortalScrip : MonoBehaviour
{
     private Rigidbody2D rb;
    public float velocityX = -5f;
    private luffyController luffyController;
    // Start is called before the first frame update
    void Start()
    {
       
          rb = GetComponent<Rigidbody2D>();
        luffyController = FindObjectOfType<luffyController>();
        
        Destroy(gameObject, 3/2);
    }
    
 
    // Update is called once per frame
    void Update()
    {
        
    }
}
