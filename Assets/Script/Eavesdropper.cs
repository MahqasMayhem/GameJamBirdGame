using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Eavesdropper : MonoBehaviour
{
    
    public TextHandler textManager;
    public GameObject UI_DialogueBox, UI_DialogueContainer, player;
    public Player playerManager;
    public float chanceMultiplier;

    private Transform currentEavesdrop;
    private Coroutine textPrinter;
    private Text textDisplay;
    private string eavesdropType, currentDialogue, textContent;
    private string characterSet = "ô╚╔╩╦╠░░░░═╬╧╨╤╥!@#▀$%^&™░¢►▀▀▀♫╢↓▀πa╚♀#↨♪▀○◘•↓R╤ÜYç╞┘ñ*┬▓¼⌠î╡♥";
    // Start is called before the first frame update
    void Start()
    {
        if (!player)
        {
            player = GameObject.Find("Player");
        }
        if (!textManager)
        {
            textManager = GameObject.Find("TextManager").GetComponent<TextHandler>();
        }
        if (!UI_DialogueBox)
        {
            UI_DialogueContainer = GameObject.Find("UI_DialogueContainer");
        }
        if (!UI_DialogueBox)
        {
            UI_DialogueBox = GameObject.Find("UI_DialogueBox");
        }
        if (!playerManager)
        {
            playerManager = gameObject.GetComponent<Player>();
        }
        UI_DialogueContainer.SetActive(false);
        string asset = textManager.GetRandomInnocentDialogue();
        textDisplay = UI_DialogueBox.GetComponent<Text>();
    }

    public IEnumerator FetchNextChar(string dialogue) //Generate the next character
    {
        int i = 0;

        Debug.Log("Dialogue print!:" + dialogue);
        while (i < dialogue.Length)
        {
            if (char.IsWhiteSpace(dialogue[i])) //print all spaces
            {
                textContent += dialogue[i];
                
            }
            else if (CalculateChance()) //print correct character
            {
                PrintCharacter(dialogue[i]);
                
            }
            else //print random character
            {
                PrintCharacter(RandomChar());
                
            }
            i++;
            yield return new WaitForSeconds(0.1f);
        }
    }
    private void PrintCharacter(char letter)
    {
        textContent += letter;
        
    }
    private bool CalculateChance()
    {
        
        float chance = playerManager.eavesdropLevel;
        if (Random.value > (Vector3.Distance(gameObject.GetComponent<Transform>().position, currentEavesdrop.position)) / chanceMultiplier)
        {
            
            return true;
        }
        else return false;

    }
    private char RandomChar()
    {
        char value = characterSet[Random.Range(0,characterSet.Length-1)];
        return value;
    }
    // Update is called once per frame
    void Update()
    {
        if (playerManager.eavesdropping)
        {
            textDisplay.text = textContent;
        }
        else if (UI_DialogueContainer.activeSelf == true)
        {
            UI_DialogueContainer.SetActive(false);
        }
    }
    void EnableEavesdrop()
    {
        UI_DialogueContainer.SetActive(true);
        currentEavesdrop = playerManager.currentEavesdrop;
        if (currentEavesdrop.gameObject.CompareTag("NPC"))
        {
            currentDialogue = textManager.GetRandomInnocentDialogue();
            textPrinter = StartCoroutine(FetchNextChar(currentDialogue));
        }
        else if (currentEavesdrop.gameObject.CompareTag("Group"))
        {
            currentDialogue = textManager.GetRandomGroupDialogue();
            textPrinter = StartCoroutine(FetchNextChar(currentDialogue));
        }
        else if (currentEavesdrop.gameObject.CompareTag("Spy"))
        {
            currentDialogue = textManager.GetRandomSpyDialogue();
            textPrinter = StartCoroutine(FetchNextChar(currentDialogue));
        }
        else // If eavesdrop target is not valid, log
        {
            Debug.LogError("Eavesdropper: INVALID TAG '" + currentEavesdrop.gameObject.tag + "'");
            textContent = ("ERROR@Eavesdropper: INVALID TAG '" + currentEavesdrop.gameObject.tag + "'");
        }
        if (currentDialogue.Length < 1)
        {
            Debug.LogError("Warning: Received empty string from Text Manager!");
            textContent = ("ERROR: " + currentEavesdrop.gameObject.tag + "TYPE STRING CANNOT BE EMPTY.");
        }
        
    }
    void DisableEavesdrop()
    {
        textDisplay.text = string.Empty;
        textContent = string.Empty;
        currentEavesdrop = null;
        UI_DialogueContainer.SetActive(false);
        StopCoroutine(textPrinter);
    }
}
