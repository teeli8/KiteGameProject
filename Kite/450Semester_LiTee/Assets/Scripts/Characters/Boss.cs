using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    public static bool firstTimeEnteringBoss = true;

    List<string[]> hintToGive;
    public string[] hintIntro;
    public string[] hintBook;
    public string[] hintPhoto;
    public string[] hintKey;
    public string[] hintRose;
    public string[] hintGem;
    public string[] hintHat;
    public string[] hintKite;

    bool[] items;  //helper to find next uncollected item

    public Transform firePoints;
    public GameObject firePrefab;

    public float delay;
    public int fireLeft;

    public Image dialogBox;
    public Image icon;
    public Text dialog;

    void Awake()
    {
        items = new bool[GameController.numItems];
        for(int i = 0; i < items.Length; i++)
        {
            items[i] = false;
        }
        hintToGive = new List<string[]>
        {
            hintBook,
            hintPhoto,
            hintKey,
            hintRose,
            hintGem,
            hintHat,
            hintKite
        };
    }

    // Start is called before the first frame update
    void Start()
    {
        
        fireLeft = GameController.numItems;
        switchDialogBox(false);
        if (firstTimeEnteringBoss)
        {
            firstTimeEnteringBoss = false;
            StartCoroutine(WaitForDialog(hintIntro));
            return;
        }
        int hintIndex = findNextUncollectedIndex();
        if (hintIndex < 0)
        {
            string[] temp = { "You Did It" };
            StartCoroutine(FinalDialog(temp));
        }
        else
        {
            string[] hint = hintToGive[hintIndex];
            StartCoroutine(WaitForDialog(hint));
        }        
    }

    // Update is called once per frame
    void Update()
    {
        if (fireLeft <= 0)
        {
            StopCoroutine("FireTimer");
        }
    }

    int findNextUncollectedIndex()
    {
        if(GameController.itemsCollected.Count == 0)
        {
            return 0;
        }
        foreach(int i in GameController.itemsCollected)
        {
            items[i] = true;
        }
        for(int j = 0; j < items.Length; j++)
        {
            if (items[j] == false) return j;
        }
        return -1;
    }

    IEnumerator FireTimer()
    {
        

        Instantiate(firePrefab, firePoints.position, Quaternion.identity);
        fireLeft--;
        yield return new WaitForSeconds(delay);

        StartCoroutine("FireTimer");
    }

    //UI
    public void switchDialogBox(bool state)
    {
        dialogBox.enabled = state;
        dialog.enabled = state;
        foreach (Image i in icon.GetComponentsInChildren<Image>())
        {
            i.enabled = state;
        }
    }

    IEnumerator WaitForDialog(string[] dialogs)
    {
        
        MainMovement.inConversation = true;
        yield return new WaitForSeconds(0.5f);
        switchDialogBox(true);
        for (int j = 0; j < dialogs.Length; j++)
        {
            StartCoroutine(DisplayDialog(dialogs[j], dialog));
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            StopCoroutine("DisplayDialog");
        }
        switchDialogBox(false);
        MainMovement.inConversation = false;
        StartCoroutine("FireTimer");
    }

    IEnumerator DisplayDialog(string dialogToPrint, Text textbox)
    {
        for (int i = 1; i <= dialogToPrint.Length; i++)
        {
            textbox.text = dialogToPrint.Substring(0, i);
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    IEnumerator FinalDialog(string[] dialogs)
    {
        MainMovement.inConversation = true;
        yield return new WaitForSeconds(0.5f);
        switchDialogBox(true);
        for (int j = 0; j < dialogs.Length; j++)
        {
            StartCoroutine(DisplayDialog(dialogs[j], dialog));
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            StopCoroutine("DisplayDialog");
        }
        switchDialogBox(false);
        MainMovement.inConversation = false;
        SceneManager.LoadScene("Demo");
    }
}
