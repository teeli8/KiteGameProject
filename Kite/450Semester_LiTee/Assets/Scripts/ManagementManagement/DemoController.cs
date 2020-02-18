using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DemoController : MonoBehaviour
{
    public Text dialog;
    string[] dialogs =
    {
        "Success: ",
        "Satisfied with the scale and content of the game.",
        "Get most of the machanics to work. (UI, item collection, NPC, dialogue, etc)",
        "Idea that the Boss gives hints.",
        "Failure: ",
        "Did not use the best techniques at the beginning and that made things hard.",
        "Did not know about tilemap when I began",
        "Did not know how to get free texture from asset store.",
        "Focused too much on the \"message\" part of the game over the \"gameplay part\".",
        "Lessons: ",
        "Research and see if there is a better way before just dive into it.",
        "Put some more thoughts on \"actions\"",
        "Next Steps: ",
        "Build on the actions.",
        "Boss makes fancier moves, more interactive puzzles, more player actions, etc",
        "A version 2.0 !",
        "Keep Exploring !!!",
        "Thank You !!!"
    };

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitForDialog(dialogs));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator WaitForDialog(string[] dialogs)
    {
        for (int j = 0; j < dialogs.Length; j++)
        {
            //dialog.text = dialogs[j];
            StartCoroutine(DisplayDialog(dialogs[j], dialog));
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            StopCoroutine("DisplayDialog");
        }
    }

    IEnumerator DisplayDialog(string dialogToPrint, Text textbox)
    {
        for (int i = 1; i <= dialogToPrint.Length; i++)
        {
            textbox.text = dialogToPrint.Substring(0, i);
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}
