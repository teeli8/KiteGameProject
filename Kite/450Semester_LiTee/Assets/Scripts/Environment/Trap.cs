using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public static Trap instance;

    void Awake()
    {
        instance = this;
    }

    //public bool trapIsTriggered;

    //SpriteRenderer pit;
    //Color color;
    public GameObject item;
    public Transform targetPosition;
    Collider2D[] edges;
    // Start is called before the first frame update
    void Start()
    {
        //      pit = GetComponent<SpriteRenderer>();
        //      color = pit.color;
        //      pit.color = new Color(color.r, color.g, color.b, 0);
        //gameObject.SetActive(false);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        // trapIsTriggered = false;

        edges = GetComponentsInChildren<Collider2D>();
    }

    public void Triggerd()
    {
        if (item == null)
        {
            return;
        }
        item.transform.position = targetPosition.position;
        //gameObject.SetActive(true);
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        foreach (Collider2D c in edges)
        {
            if(c is CircleCollider2D)
            {
                continue;
            }
            c.isTrigger = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("player"))
        {
            Triggerd();
        }
        
    }




}
