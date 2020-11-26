using UnityEngine;

public class QuestsUi : MonoBehaviour
{
    public void OnGUI()
    {
        GUI.Box(new Rect(20, 210, 250, 600), "Journal");

        var i = 0;
        var y = 0;


        foreach (var quest in Player.instance.quests.ToArray())
        {
            GUI.Label(new Rect(40, 240 + i * 40 + y * 40, 220, 20), quest.title);
            GUI.Label(new Rect(40, 260 + i * 40 + y * 40, 220, 20), quest.description);

            foreach(var req in quest.GetCompletionProgress())
            {
                GUI.Label(new Rect(40, 280 + i * 40 + y * 40, 220, 20), req);
                y++;
            }

            if (GUI.Button(new Rect(40, 260 + i * 40 + y * 40, 220, 20), "Cancel"))
            {
                quest.Cancel();
            }

            i++;
        }
    }
}
