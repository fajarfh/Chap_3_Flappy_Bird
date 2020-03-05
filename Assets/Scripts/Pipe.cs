using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pipe : MonoBehaviour
{

    //Global variable
    [SerializeField] private Bird bird;
    [SerializeField] private float speed = 1;

    //   [SerializeField] private PipeSpawner pipeSpawn;
    private float limity;
    private float limiVar;
    private float yspeedUp, yspeedDown;
    private bool isShot;
    private Animator animator;


    private void UpDownAnim()
    {
        if (!isShot) { 
            if (name.Contains("Up"))
            {

                transform.Translate(Vector3.up * yspeedUp * Time.deltaTime, Space.World);
                float currenty = transform.position.y;

                if ((currenty >= limiVar) || (currenty <= limity))
                {
                    yspeedUp = -yspeedUp;

                }

            }
            else if (name.Contains("Down"))
            {

                transform.Translate(Vector3.down * yspeedDown * Time.deltaTime, Space.World);
                float currenty = transform.position.y;

                if ((currenty <= limiVar) || (currenty >= limity))
                {
                    yspeedDown = -yspeedDown;

                }

            }
        }
    }

    private void Destruction()
    {
        Destroy(gameObject);
    }

    //Membuat Bird mati ketika bersentuhan dan menjatuhkannya ke ground jika mengenai di atas collider
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Bird bird = collision.gameObject.GetComponent<Bird>();

        PewPew pewpew = collision.gameObject.GetComponent<PewPew>();

       // Rigidbody2D pewpewrig = collision.gameObject.GetComponent<Rigidbody2D>();

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

            //Burung Mati
            bird.Dead();

        } else if (pewpew)
        {
            isShot = true;

          //  ContactPoint2D lastcontactpoint = collision.GetContact(0);
            //Vector2 contactPoint = new Vector2(lastcontactpoint., lastcontactpoint.tangentImpulse);

            Rigidbody2D rigidbody2D = gameObject.GetComponent<Rigidbody2D>();

            rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
            rigidbody2D.mass = 0.1f;
            rigidbody2D.gravityScale = 0.1f;

            //print(pewpewrig.velocity);
            //print(contactPoint);
            // rigidbody2D.AddForce(contactPoint * 10);     
            rigidbody2D.AddForce(Vector2.right * 10);
            //Mendapatkan komponent Collider pada game object
            Collider2D collider = GetComponent<Collider2D>();

            if (collider)
            {
                //Menonaktifkan collider
                collider.enabled = false;

            }

            animator.enabled = true;

        }
    }

    // Start is called before the first frame update
    void Start()
    {
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
        //Melakukan pengecekan jika burung belum mati
        if (!bird.IsDead())
        {
            //Membuat pipa bergerak kesebelah kiri dengan kecepatan tertentu
            transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
            //transform.Translate(Vector3.up * speed * Time.deltaTime, Space.World);

            int scoreCheck = bird.score;
            
            if ( scoreCheck >= 3 )
            {
                UpDownAnim();
            }

        }

    }
}
