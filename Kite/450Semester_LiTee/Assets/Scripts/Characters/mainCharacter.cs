using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mainCharacter : MonoBehaviour
{

    public int health;
    public int maxHealth;

    public Image[] hearts;
    public Sprite redHeart;
    public Sprite hollowHeart;

    void Awake()
    {
        maxHealth = GameController.maxHealth;
        health = maxHealth;
    }

    // Start is called before the first frame update
    void Start()
    {
        //initialize health
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < maxHealth)
            {
                hearts[i].enabled = true;
                hearts[i].sprite = redHeart;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
  
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("bullets"))
        {
            GetHurt();
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("items"))
        {
            CollectItem(collision.gameObject);           
        }

    }

    void GetHurt()
    {
        health--;
        updateHealthUI();
        if (health <= 0)
        {
            //die and reset
            SceneManager.LoadScene("Main");
        }
    }

    void CollectItem(GameObject item)
    {
        //update itemlists
        int itemIndex = GameController.instance.findIndex(item);
        GameController.itemsCollected.Add(itemIndex);
        GameController.instance.enableItemImage(itemIndex);
        Destroy(item);

        //update health
        health = ++maxHealth;
        GameController.maxHealth = maxHealth;
        updateHealthUI();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("bullets"))
        {
            health--;
            updateHealthUI();
            if (health <= 0)
            {
                SceneManager.LoadScene("Main");
            }
        }
    }

    void updateHealthUI()
    {
        if(health < maxHealth && health >= 0)
        {
            hearts[health].sprite = hollowHeart;
        }
        else
        {
            for(int i = 0; i < maxHealth; ++i)
            {
                hearts[i].enabled = true;
                hearts[i].sprite = redHeart;
            }
        }
    }
}
