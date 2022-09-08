using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    private AudioSource audioD;
    private int charsToSound = 2;//each 2 char will play the sound of 1 character typed
    private bool isPlayerInRange;
    [SerializeField] private GameObject canvas;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;

    private bool isDialogueActive;
    private int lineIndex;

    private void Start()
    {
        audioD = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.name == "sign")
        {
            if (isPlayerInRange && Input.GetButtonDown("Fire1"))
            {
                if (!isDialogueActive)
                {
                    StartDialogue();
                }
                //when the entire line is showed, pressing the key you'll see the next line
                else if (dialogueText.text == dialogueLines[lineIndex])
                {
                    NextDialogueLine();
                }
                //if you press the key before the entire line is showed, the courutine stop, showing all the characters of the line
                else
                {
                    StopAllCoroutines();
                    dialogueText.text = dialogueLines[lineIndex];
                }
            }
        }
        else
        {
            if (gameObject.GetComponent<PowerUp>().dialogSignal)
            {
                if (!isDialogueActive)
                {
                    StartDialogue();

                }
                if (Input.GetButtonDown("Fire1"))
                {
                    //when the entire line is showed, pressing the key you'll see the next line
                    if (isDialogueActive && dialogueText.text == dialogueLines[lineIndex])
                    {
                        NextDialogueLine();
                    }
                    else
                    {
                        StopAllCoroutines();
                        dialogueText.text = dialogueLines[lineIndex];
                    }
                }
            }
        }
        
    }

    public void StartDialogue()
    {
        isDialogueActive = true;
        canvas.SetActive(true);
        lineIndex = 0;
        Time.timeScale = 0f; //to stop all the objects when reading a text
        StartCoroutine(ShowLine());
    }

    private void NextDialogueLine()
    {
        lineIndex++;
        if(lineIndex < dialogueLines.Length)
        {
            StartCoroutine(ShowLine());
        }
        else
        {
            isDialogueActive = false;
            canvas.SetActive(false);
            if (gameObject.name != "sign")
            {
                gameObject.GetComponent<PowerUp>().dialogSignal = false;
                Time.timeScale = 1f;//continue objects' moving
                gameObject.SetActive(false);
            }
            Time.timeScale = 1f;//continue objects' moving
        }
    }

    //to see the dialogue char by char
    private IEnumerator ShowLine()
    {
        dialogueText.text = string.Empty;
        int charIndex = 0;
        for (int i = 0; i < dialogueLines[lineIndex].Length; i++)
        {
            char ch = dialogueLines[lineIndex][i];
            dialogueText.text += ch;
            //each charsToSound will play the sound of 1 character typed
            if (charIndex % charsToSound == 0)
            {
                audioD.Play();
            }
            charIndex++;
            yield return new WaitForSecondsRealtime(0.05f);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
}
