using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Menambahkan komponen Rigidbody2D dan BoxCollider2D
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]

public class Destroyer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(gameObject.name != "Destroyer Front")
        {
            //Memusnahkan object ketika bersentuhan kalau bukan Destroyer front
            Destroy(collision.gameObject);
        } else
        {
            PewPew pewpew = collision.gameObject.GetComponent<PewPew>();

            if (pewpew)
            {             
                Destroy(collision.gameObject);
            }
        }
    }
}
