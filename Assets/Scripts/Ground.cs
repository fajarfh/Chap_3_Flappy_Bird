using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    //Global variables
    [SerializeField] private Bird bird;
    [SerializeField] private float speed = 1;
    [SerializeField] private Transform nextPos;

    //Method untuk Menempatkan game object pada posisi next ground
    public void SetNextGround(GameObject ground)
    {
        //Pengecekan Null value
        if (ground != null)
        {
            //Menempatkan ground berikutnya pada posisi nextground
            ground.transform.position = nextPos.position;
        }
    }

    //Dipanggil ketika game object bersentuhan dengan game object yang lain
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Membuat Bird mati ketika bersentuhan dengan game object ini
        if (bird != null && !bird.IsDead())
        {
            bird.Dead();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Melakukan pengecekan jika Bird null atau belu mati
        if (bird == null || (bird != null && !bird.IsDead()))
        {
            //Membuat Pipe bergerak kesebelah kiri dengan kecepatan dari variable speed
            transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
        }
    }
}
