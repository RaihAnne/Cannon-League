using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class RaihConsole : MonoBehaviour
{
    [SerializeField] private Text ConsoleText;
    [SerializeField] private Scrollbar ConsoleScrollBar;

    private float initialTextHeight;
    private bool IsFirstLog = false;

    private void Awake()
    {
        initialTextHeight = ConsoleText.rectTransform.rect.height;
        Application.logMessageReceived += LogEvent;
    }


    private void LogEvent(string condition, string stackTrace, LogType type)
    {
        AddStringToConsole(condition);
    }

    public void AddStringToConsole(string message)
    {
        ScrollToBottom();
        ConsoleText.text += ">" + message + "\n";
        AddNewLineHeight();
    }

    private void ScrollToBottom()
    {
        ConsoleScrollBar.value = 0;
    }

    private void AddNewLineHeight()
    {
        if (IsFirstLog)
        {
            IsFirstLog = false;
            return;
        }

        var newSize = ConsoleText.rectTransform.sizeDelta;
        newSize.y += initialTextHeight;
        ConsoleText.rectTransform.sizeDelta = newSize;
    }

}
