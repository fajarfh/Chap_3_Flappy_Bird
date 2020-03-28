using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PewPew : MonoBehaviour
{

    //Global Variable
    [SerializeField] private float PewPewSpeed = 75f;
    [SerializeField] private Bird bird;
    
    //PewPew menggunakan rigidbody Dynamic dengan coordinate Y dan Z di freeze 
    //Karena hanya bisa jalan kalau seperti itu. namun mungkin seharusnya kinematic
    //tapi tidak bisa jalan.
    private Rigidbody2D rigidBody2d;

    // Jika PewPew menabrak Pipe, Object PewPew hancur
    private void OnTriggerEnter2D(Collider2D collision)
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

        //Menambahkan gaya ke arah sumbu x agar PewPew terlontar saat muncul
        rigidBody2d.AddForce(Vector2.right * PewPewSpeed);

     }

    // Update is called once per frame
    void Update()
    {
        //Jika Bird mati, PewPew yang sudah tertembak akan hilang/hancur
        if (bird.IsDead())
        {
            Destroy(gameObject);
        }
    }
}
