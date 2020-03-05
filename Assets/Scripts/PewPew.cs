using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PewPew : MonoBehaviour
{

    [SerializeField] private float PewPewSpeed;
    [SerializeField] private Bird bird;
    private Rigidbody2D rigidBody2d;

    private void OnCollisionEnter2D(Collision2D collision)
    {

        Pipe pipe = collision.gameObject.GetComponent<Pipe>();
    
        if (pipe)
        {
            //mati
            Destroy(gameObject);
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        //Mendapatkan component ketika game baru berjalan
        rigidBody2d = GetComponent<Rigidbody2D>();

        //Menambahkan gaya ke arah sumbu x agar PewPew terlontar
        rigidBody2d.AddForce(Vector2.right * PewPewSpeed);

     }

    // Update is called once per frame
    void Update()
    {
        if (bird.IsDead())
        {
            Destroy(gameObject);
        }
    }
}
