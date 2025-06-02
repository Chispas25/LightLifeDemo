using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TutorialDialogueRegistry
{
    private static HashSet<string> shownDialogues = new HashSet<string>();

    public static bool HasBeenShown(string dialogueID)
    {
        return shownDialogues.Contains(dialogueID);
    }

    public static void MarkAsShown(string dialogueID)
    {
        shownDialogues.Add(dialogueID);
    }
}
