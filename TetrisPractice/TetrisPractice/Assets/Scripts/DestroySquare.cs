using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySquare : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("entro a morir");
        if (collision.gameObject.CompareTag("Rythmic"))
            Destroy(collision.gameObject);
    }
    
}
