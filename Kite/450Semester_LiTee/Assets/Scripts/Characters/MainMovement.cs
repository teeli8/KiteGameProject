using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMovement : MonoBehaviour
{

    Rigidbody2D rb;
    public int jumpCount;
    public int maxjumpCount;
    public float jumpAbility;

    public static bool inConversation;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpCount = maxjumpCount;
        inConversation = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (inConversation)
        {
            return;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (rb.gravityScale>0)
            {
                jump();
            }
            else
            {
                transform.position += new Vector3(0, 8f * Time.deltaTime, 0);
            }
           
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (rb.gravityScale <= 0)
            {
                transform.position += new Vector3(0, -8f*Time.deltaTime, 0);
            }         
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.rotation = Quaternion.identity;
            transform.position += new Vector3(-8f * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.rotation = new Quaternion(0, 180, 0,0);
            transform.position += new Vector3(8f * Time.deltaTime, 0, 0);
        }

    }

    void jump()
    {
        if (jumpCount > 0)
        {
            rb.AddForce(Vector2.up*jumpAbility);
            jumpCount--;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (rb.gravityScale>0
                && collision.gameObject.layer == LayerMask.NameToLayer("edges"))
        {
            jumpCount = maxjumpCount;
        }
         
    }



}
