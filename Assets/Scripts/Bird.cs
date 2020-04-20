using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Bird : MonoBehaviour
{
    //Global Variables
    public int score; // Score dibuat public agar bisa diakses oleh Pipe sebagai syarat mulai gerakan naik turun

    [SerializeField] private Text scoreText;
    [SerializeField] private PewPew pewpew; // Object PewPew yang ditembakkan dari Bird
    [SerializeField] private float upForce = 100;
    [SerializeField] private bool isDead;
    [SerializeField] private UnityEvent OnJump, OnDead, OnAddPoint;

    private Rigidbody2D rigidBody2d;
    private Animator animator;

    //Fungsi untuk mengecek sudah mati apa belum
    public bool IsDead()
    {
        return isDead;
    }

    //Membuat Bird Mati
    public void Dead()
    {
        //Pengecekan jika belum mati dan value OnDead tidak sama dengan Null
        if (!isDead && OnDead != null)
        {
            rigidBody2d.transform.rotation = Quaternion.Euler(0f, 0f, 180f);

            //Memanggil semua event pada OnDead
            OnDead.Invoke();
        }

        //Mengeset variable Dead menjadi True
        isDead = true;

    }

    // Fungsi membuat Bird melompat
    void Jump()
    {
        //Mengecek rigidbody null atau tidak
        if (rigidBody2d)
        {
            //menghentikan kecepatan Bird ketika jatuh
            rigidBody2d.velocity = Vector2.zero;

            //Menambahkan gaya ke arah sumbu y agar Bird meloncat
            rigidBody2d.AddForce(new Vector2(0, upForce));
            rigidBody2d.transform.rotation = Quaternion.Euler(0f, 0f, 30f);
        }

        //Pengecekan Null variable
        if (OnJump != null)
        {
            //Menjalankan semua event OnJump event
            OnJump.Invoke();
        }
    }

    //Fungsi untuk Bird menembakkan PewPew
    void Shoot()
    {

        //menduplikasi game object PewPew
        PewPew newPewpew = Instantiate(pewpew, transform.position, Quaternion.identity);

        //Mengaktifkan game object PewPew
        newPewpew.gameObject.SetActive(true);

    }

    //Fungsi deteksi tabrakan Bird dengan objek lain
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //menghentikan Animasi Bird ketika bersentukan dengan object lain
        animator.enabled = false;
    }

    // Fungsi menambahkan score saat melalui Point
    public void AddScore(int value)
    {
        //Menambahkan Score value
        score += value;

        //Mengubah nilai text pada score text
        scoreText.text = score.ToString();

        //Pengecekan Null Value
        if (OnAddPoint != null)
        {
            //Memanggil semua event pada OnAddPoint
            OnAddPoint.Invoke();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //Mendapatkan component ketika game baru berjalan
        rigidBody2d = GetComponent<Rigidbody2D>();

        //Mendapatkan komponen animator pada game object   
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Melakukan pengecekan jika belum mati dan klik kiri pada mouse
        if (!isDead)
        {
            //Bird meloncat
            if (Input.GetMouseButtonDown(0))
            {
                //Bird meloncat
                Jump();
            } else if (Input.GetMouseButtonUp(0))
            {
                rigidBody2d.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
        } 

        //Melakukan pengecekan jika belum mati dan klik kanan pada mouse untuk nembak
        if (!isDead && Input.GetMouseButtonDown(1))
        {
            // Mengecek apakah scene yang aktif adalah Main, sebab pada scene Opening seharusnya
            // tidak bisa menembakkan PewPew
            if (gameObject.scene.name == "Main")
            {
                //Bird nembak (ciecie)
                //Bird bisa nembak tanpa batas
                Shoot();
            }
            
        }

        if (rigidBody2d.velocity.y < -3f)
        {
            rigidBody2d.transform.rotation = Quaternion.Euler(0f, 0f, -30f); ;
        }
    }

}
