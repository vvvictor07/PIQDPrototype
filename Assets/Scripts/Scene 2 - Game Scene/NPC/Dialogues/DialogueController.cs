using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    public Dialogue rootDialogue;

    private Dialogue currentDialogue;

    private int currentSentenceIndex;

    private bool endOfDialogueReached;

    private List<QuestBase> activeQuests = new List<QuestBase>();

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
            ReturnToRoot();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Player.instance.tag))
        {
            inRange = false;
            active = false;
            ReturnToRoot();
        }
    }

    public void OnGUI()
    {
        if (!active)
        {
            return;
        }

        GUI.Box(new Rect(610, 510, 500, 300), name);

        var completedQuest = activeQuests.FirstOrDefault(x => x.status == QuestStatus.RequirementsReached);

        if (completedQuest != null)
        {
            GUI.Label(new Rect(620, 540, 450, 20), completedQuest.name + " completed!");

            if (GUI.Button(new Rect(620, 580, 450, 20), "Claim rewards"))
            {
                completedQuest.Complete();
                ReturnToRoot();
            }

            return;
        }

        if (currentDialogue.quest != null)
        {
            if (GUI.Button(new Rect(620, 580, 450, 20), "Deal <Accept>"))
            {
                var newQuest = Instantiate(currentDialogue.quest);
                activeQuests.Add(newQuest);
                newQuest.Accept();
            }

            if (GUI.Button(new Rect(620, 620, 450, 20), "I got better things to do <Decline>"))
            {
                ReturnToRoot();
            }
            return;
        }

        GUI.Label(new Rect(620, 540, 450, 20), currentDialogue.sentences[currentSentenceIndex]);

        if (endOfDialogueReached)
        {
            var i = 1;

            if (currentDialogue.variants.Length > 0)
            {
                foreach (var dialogue in currentDialogue.variants)
                {
                    if (dialogue.quest != null 
                        && (dialogue.quest.status == QuestStatus.Completed 
                        || dialogue.quest.status == QuestStatus.RequirementsReached
                        || dialogue.quest.status == QuestStatus.Accepted))
                    {
                        continue;
                    }

                    if (GUI.Button(new Rect(620, 540 + 40 * i, 450, 20), dialogue.playerQuote))
                    {
                        SelectNextDialogue(dialogue);
                    }

                    i++;
                }
            }
            else
            {
                if (GUI.Button(new Rect(620, 580, 450, 20), "Return"))
                {
                    ReturnToRoot();
                }
            }

            if (rootDialogue == currentDialogue)
            {
                if (GUI.Button(new Rect(620, 540 + 40 * i, 450, 20), "Bye"))
                {
                    active = false;
                }
            }
        }
        else
        {
            if (GUI.Button(new Rect(620, 580, 450, 20), "Continue"))
            {
                NextSentence();
            }
        }
    }
}
