using System;
using UnityEngine;

public enum DialogueType
{
    Neutral = 0,
    Like = 1,
    Dislike = 2,
}

[Serializable]
public class Dialogue
{
    [TextArea(3, 240)]
    public string playerQuote;

    public Dialogue[] variants;

    [TextArea(3, 240)]
    public string[] sentences;

    public DialogueType type;
}
