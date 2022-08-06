using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour {
    public Queue<string> dialogueLines;
    public TextMeshProUGUI dialogueText;
    public Animator dialogueAnimator;

    public void StartDialogue (Dialogue dialogue) {
        dialogueAnimator.SetBool ("IsOpen", true);
        dialogueLines = new Queue<string> ();
        foreach (string sentence in dialogue.sentences) {
            dialogueLines.Enqueue (sentence);
        }
        DisplayNextSentence ();
    }
    private void Start () {
        dialogueLines = new Queue<string> ();
    }

    public void DisplayNextSentence () {
        if (dialogueLines.Count == 0) {
            EndDialogue ();
            return;
        }
        string sentence = dialogueLines.Dequeue ();
        StopAllCoroutines ();
        StartCoroutine (TypeSentence (sentence));
    }
    IEnumerator TypeSentence (string sentence) {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray ()) {
            dialogueText.text += letter;
            yield return null;
        }
    }
    public void EndDialogue () {
        dialogueAnimator.SetBool ("IsOpen", false);
        Debug.Log ("End of conversation.");
    }
}