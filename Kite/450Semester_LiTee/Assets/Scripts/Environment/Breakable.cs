using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    int hitLeft;
    SpriteMask[] masks;
    // Start is called before the first frame update
    void Start()
    {
        masks = GetComponentsInChildren<SpriteMask>();
        hitLeft = masks.Length;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (hitLeft >= 0
            && collision.relativeVelocity.magnitude >= 0f)
        {
            if(hitLeft == 0)
            {
                Destroy(gameObject);
                return;
            }
            masks[masks.Length - hitLeft].alphaCutoff = 0.7f;
            hitLeft--;
        }
    }
}