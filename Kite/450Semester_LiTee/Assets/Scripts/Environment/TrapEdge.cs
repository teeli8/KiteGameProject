using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapEdge : MonoBehaviour
{

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.transform.position.x > gameObject.transform.position.x)
        {
            Trap.instance.Triggerd();
        }
        
    }
}
