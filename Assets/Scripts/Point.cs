using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]

public class Point : MonoBehaviour
{

    [SerializeField] private Bird bird;
    [SerializeField] private float speed = 1;

    // Fungsi menentukan ukuran Point
    public void SetSize(float size)
    {
        //Mendapatkan komponent BoxCollider2D
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        //Pengecekan Null variable
        if (collider != null)
        {
            //mengubah ukuran collider sesuai dengan paramater
            //ukuran dibuat lebih besar dari lubang dan akan selalu beririsan dengan
            //PipeUp atau PipeDown sebagai teknik agar Bird dapat score saat melalui
            //Celah Pipe meski Pipe bergerak naik-turun
            //Juga dilakukan agar saat PipeDown atau PipeUp ditembak dan hilang, 
            //Point ikut hilang
            collider.size = new Vector2(collider.size.x, size+(4*speed));
        }
    }

    // Deteksi tabrakan ketika yang nabrak keluar area collider
    void OnTriggerExit2D(Collider2D collision)
    {
        //Mendapatkan komponen Bird
        Bird bird = collision.gameObject.GetComponent<Bird>();
        //Menambahkan score jika Bird tidak null dan Bird belum mati;
        if (bird && !bird.IsDead())
        {
            bird.AddScore(1);
        }

        //Jika Pipe hancur/Destroy dan hilang dari area collider Point, hancur pula object Point
        Pipe pipe = collision.gameObject.GetComponent<Pipe>();

        if (pipe)
        {
            Destroy(gameObject);
        }


    }

    // Update is called once per frame
    void Update()
    {
        //Melakukan pengecekan Bird mati atau tidak
        if (!bird.IsDead())
        {
            //menggerakan game object kesebelah kiri dengan kecepatan tertentu
            transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
        }
    }
}
