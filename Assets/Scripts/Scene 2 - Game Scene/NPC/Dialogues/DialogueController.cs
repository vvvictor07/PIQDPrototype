using UnityEngine;

public class DialogueController : MonoBehaviour
{
    public Dialogue rootDialogue;

    private Dialogue currentDialogue;

    private int currentSentenceIndex;

    private bool endOfDialogueReached;

    public bool active;

    private bool inRange;

    private void Start()
    {
        ReturnToRoot();
    }

    private void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.E))
        {
            active = true;
        }
    }

    public void SelectNextDialogue(Dialogue dialogue)
    {
        currentSentenceIndex = 0;
        currentDialogue = dialogue;
        endOfDialogueReached = currentSentenceIndex >= currentDialogue.sentences.Length - 1;
    }

    public void NextSentence()
    {
        currentSentenceIndex++;
        endOfDialogueReached = currentSentenceIndex >= currentDialogue.sentences.Length - 1;
    }

    public void ReturnToRoot()
    {
        SelectNextDialogue(rootDialogue);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Player.instance.tag))
        {
            inRange = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Player.instance.tag))
        {
            inRange = false;
            active = false;
        }
    }

    public void OnGUI()
    {
        if (!active)
        {
            return;
        }

        GUI.Box(new Rect(610, 510, 500, 300), name);

        GUI.Label(new Rect(620, 540, 180, 20), currentDialogue.sentences[currentSentenceIndex]);

        if (endOfDialogueReached)
        {
            var i = 1;

            if (currentDialogue.variants.Length > 0)
            {
                foreach (var dialogue in currentDialogue.variants)
                {
                    if (GUI.Button(new Rect(620, 540 + 40 * i, 180, 20), dialogue.playerQuote))
                    {
                        SelectNextDialogue(dialogue);
                    }

                    i++;
                }
            }
            else
            {
                if (GUI.Button(new Rect(620, 580, 180, 20), "Return"))
                {
                    ReturnToRoot();
                }
            }

            if (rootDialogue == currentDialogue)
            {
                if (GUI.Button(new Rect(620, 540 + 40 * i, 180, 20), "Bye"))
                {
                    active = false;
                }
            }
        }
        else
        {
            if (GUI.Button(new Rect(620, 580, 180, 20), "Continue"))
            {
                NextSentence();
            }
        }
    }
}
