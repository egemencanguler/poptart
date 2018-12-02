using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandNumber : MonoBehaviour
{

    public Text commandNumberText;
    public Image backgroundImage;
    int commandIdx = -1;
    
    public void Setup(int commandIdx)
    {
        this.commandIdx = commandIdx;
        commandNumberText.text = (commandIdx + 1) + "";
    }

    void OnEnable()
    {
        CommandPanel.SendCommand += OnExecuteCommand;
    }

    void OnDisable()
    {
        CommandPanel.SendCommand -= OnExecuteCommand;
    }

    void OnExecuteCommand(Command command)
    {
        if (command.commandIdx == commandIdx)
        {
            backgroundImage.color = Color.green;
        }
    }
}
