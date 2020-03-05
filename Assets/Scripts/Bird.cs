using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Bird : MonoBehaviour
{
    //Global Variables
    [SerializeField] private float upForce = 100;
    [SerializeField] public int score;
    [SerializeField] private Text scoreText;
    [SerializeField] private bool isDead;

    [SerializeField] private UnityEvent OnJump, OnDead, OnAddPoint;

    [SerializeField] private PewPew pewpew;

    private Rigidbody2D rigidBody2d;
    private Animator animator;

    //Fungsi untuk mengecek sudah mati apa belum
    public bool IsDead()
    {
        return isDead;
    }

    //Membuat Burung Mati
    public void Dead()
    {
        //Pengecekan jika belum mati dan value OnDead tidak sama dengan Null
        if (!isDead && OnDead != null)
        {
            //Memanggil semua event pada OnDead
            OnDead.Invoke();
        }

        //Mengeset variable Dead menjadi True
        isDead = true;

    }

    void Jump()
    {
        //Mengecek rigidbody null atau tidak
        if (rigidBody2d)
        {
            //menghentikan kecepatan burung ketika jatuh
            rigidBody2d.velocity = Vector2.zero;

            //Menambahkan gaya ke arah sumbu y agar burung meloncat
            rigidBody2d.AddForce(new Vector2(0, upForce));
        }

        //Pengecekan Null variable
        if (OnJump != null)
        {
            //Menjalankan semua event OnJump event
            OnJump.Invoke();
        }
    }

    void Shoot()
    {

        //menduplikasi game object pipeUp dan menempatkan posisinya sama dengan game object ini tetapi dirotasi 180 derajat
        PewPew newPewpew = Instantiate(pewpew, transform.position, Quaternion.identity);

        //Mengaktifkan game object newPipeUp
        newPewpew.gameObject.SetActive(true);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //menghentikan Animasi Burung ketika bersentukan dengan object lain
        animator.enabled = false;
    }

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
        if (!isDead && Input.GetMouseButtonDown(0))
        {
            //Burung meloncat
            Jump();
        }

        //Melakukan pengecekan jika belum mati dan klik kanan pada mouse
        if (!isDead && Input.GetMouseButtonDown(1))
        {
            if (gameObject.scene.name == "Main")
            {
                //Burung nembak (ciecie)
                Shoot();
            }
            
        }
    }

}
