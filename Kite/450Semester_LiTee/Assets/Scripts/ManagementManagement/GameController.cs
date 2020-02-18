using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    //statics
    public static GameController instance;

    public static int maxHealth = 1;

    enum Item {key = 0, photo = 1, rose = 2};

    public static List<int> itemsCollected = new List<int>();
    //public static List<int> itemsNotCollected = new List<int>();

    public static int numItems;

    
    

    public GameObject[] items;

    public Image[] itemCollectedImages;

    public Camera mainCamera;
    public Camera specialCamera;

    public Image dialogBox;
    public Image icon;
    public Text dialog;


    void Awake()
    {
        instance = this;
        //    itemsNotCollected = new List<int>();
        //    DontDestroyOnLoad(this.gameObject);
        numItems = items.Length;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (Image i in itemCollectedImages)
        {
            i.enabled = false;
        }
        foreach(int j in itemsCollected)
        {
            enableItemImage(j);
            Destroy(items[j]);
        }
        specialCamera.enabled = false;

        switchDialogBox(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //find item in the list
    public int findIndex(GameObject item)
    {
        for(int i = 0; i < items.Length; ++i)
        {
            if (items[i] == null)
            {
                continue;
            }
            if(items[i].name == item.name)
            {
                return i;
            }
        }
        return -1;
    }

    public void switchCam()
    {
        mainCamera.enabled = !mainCamera.enabled;
        specialCamera.enabled = !specialCamera.enabled;
    }

    
    //UI
    public void enableItemImage(int index)
    {
        itemCollectedImages[index].enabled = true;
    }


    public void switchDialogBox(bool state)
    {
        dialogBox.enabled = state;
        dialog.enabled = state;
        foreach(Image i in icon.GetComponentsInChildren<Image>())
        {
            i.enabled = state;
        }
    }



}
