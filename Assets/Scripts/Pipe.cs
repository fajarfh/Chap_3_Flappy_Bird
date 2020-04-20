using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{

    //Global variable
    [SerializeField] private Bird bird;
    [SerializeField] private float speed = 1;
    //[SerializeField] private int groovyScore = 5;

    //GLobal variable untuk mengatur properti Dynamic Pipe
    [SerializeField] private float pipeMass = 0.1f;
    [SerializeField] private float pipeGravityScale = 0.1f;

    [HideInInspector] public Boolean isGroove;

    //Variable untuk membantu animasi naik turun
    private float limity;
    private float limiVar;
    private float yspeedUp, yspeedDown;

    //Variable untuk membantu animasi saat ditembak
    private bool isShot;
    private Animator animator;

    //Fungsi animasi naik turun. Fungsi ini sepertinya masih buggy karena 
    //kadang-kadang Pipe stuck dan tidak naik turun tapi bergetar
    //Pipe yang sudah dilewati Bird saat syarat score terpenuhi juga ikut naik turun
    private void UpDownAnim()
    {
        // Saat kena tembak, animasi naik turun tidak akan jalan
        if (!isShot) { 
            
            //Kalau nama Object PipeUp maka animasinya naik turun, kalau PipeDown turun naik
            if (name.Contains("Up"))
            {

                //Menggerakkan Pipe
                transform.Translate(Vector3.up * yspeedUp * Time.deltaTime, Space.World);

                //Menyimpan posisi Pipe saat ini
                float currenty = transform.position.y;

                //Saat Pipe melebihi limity (posisi awal) ATAU limiVar (batas atas posisi)
                //Arah gerak berubah
                if ((currenty >= limiVar) || (currenty <= limity))
                {

                    yspeedUp = -yspeedUp;

                }

            }
            else if (name.Contains("Down"))
            {

                //Menggerakkan Pipe
                transform.Translate(Vector3.down * yspeedDown * Time.deltaTime, Space.World);
                
                //Menyimpan posisi Pipe saat ini
                float currenty = transform.position.y;

                //Saat Pipe melebihi limity (posisi awal) ATAU limiVar (batas bawah posisi)
                //Arah gerak berubah
                if ((currenty <= limiVar) || (currenty >= limity))
                {
                    yspeedDown = -yspeedDown;

                }

            }
        }
    }

    // Fungsi ini dipanggil dari animator Pipe agar Pipe hancur saat animasi tertembak selesai
    private void Destruction()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PewPew pewpew = collision.gameObject.GetComponent<PewPew>();
        if (pewpew) // Pengecekan Null value saat ditembak PewPew
        {

            // Ubah status tertembak
            isShot = true;

            //Mengubah rigidbody2D dari Pipe menjadi Dynamic agar tertarik gravitasi
            Rigidbody2D rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
            rigidbody2D.bodyType = RigidbodyType2D.Dynamic;

            //Tuning mass dan gravity scale di sini agar animasi hancur jadi lebih enak
            rigidbody2D.mass = pipeMass;
            rigidbody2D.gravityScale = pipeGravityScale;

            //Mendorong Pipe searah tembakan PewPew
            //Inginnya rotasi Pipe berubah sesuai posisi kontak tertembaknya
            //Tapi belum tahu caranya
            rigidbody2D.AddForce(Vector2.right * 10);

            //Mematikan collider Pipe saat hancur sehingga Pipe yang hancur tidak bisa melukai Bird
            Collider2D collider = GetComponent<Collider2D>();

            if (collider)
            {
                //Menonaktifkan collider
                collider.enabled = false;

            }

            //Menjalankan animasi Fade Out dari Pipe
            animator.enabled = true;

            print("what");
        }
    }

    //Membuat Bird mati ketika bersentuhan dan menjatuhkannya ke ground jika mengenai di atas collider
    //Juga untuk membuat Pipe hancur saat berentuhan dengan PewPew (ditembak)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Bird bird = collision.gameObject.GetComponent<Bird>();
        //PewPew pewpew = collision.gameObject.GetComponent<PewPew>();

        //Pengecekan Null value
        if (bird)
        {
            //Mendapatkan komponent Collider pada game object
            Collider2D collider = GetComponent<Collider2D>();

            //Melakukan pengecekan Null variabel atau tidak
            if (collider)
            {
                //Menonaktifkan collider
                collider.enabled = false;
                
            }

            //Bird Mati
            bird.Dead();

        } 

    }

    // Start is called before the first frame update
    void Start()
    {
        //Mengubah batas atas atau batas bawah serta kecepatan pada animasi naik turun
        //tergantung apakah PipeUp atau PipeDown
        if (name.Contains("Up"))
        {
            limity = transform.position.y;
            limiVar = limity + speed;
            yspeedUp = speed;

        } else if (name.Contains("Down"))
        {
            limity = transform.position.y;
            limiVar = limity - speed;
            yspeedDown = speed;

        }

        //Mendapatkan komponen animator pada game object   
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        //Melakukan pengecekan jika Bird belum mati
        if (!bird.IsDead())
        {
            //Membuat Pipe bergerak kesebelah kiri dengan kecepatan tertentu
            transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);

            //Mengecek score dan menggerakkan Pipe naik turun setelah mencapai score groovyScore
            //int scoreCheck = bird.score;
            
            if (isGroove)
            {
                UpDownAnim();
            }

        }

    }
}
