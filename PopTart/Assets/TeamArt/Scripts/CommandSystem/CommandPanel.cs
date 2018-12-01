using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandPanel : MonoBehaviour
{
    public static event System.Action<Command> SendCommand;
    
    public RectTransform buttonContainer;
    public GameObject commandButtonPrefab;
    public GridLayoutGroup buttonGrid;
    public Text leftCommandText, rightCommandText, upCommandText, downCommandText;
    public Button playButton;
    
    List<CommandButton> commandButtons = new List<CommandButton>();
    CommandNumbers commandNumbers;
    int numberOfPlayers;

    void Awake()
    {
        playButton.onClick.AddListener(OnPlayButtonClicked);
        Setup(5,10,1,2,3,4);
    }

    void Setup(int nPlayer, int nCommand, int nLeft, int nRight, int nUp, int nDown)
    {
        numberOfPlayers = nPlayer;
        commandNumbers = new CommandNumbers(nLeft,nRight,nUp,nDown);
        commandButtons.Clear();
        buttonGrid.constraintCount = nPlayer;
        
        for (int i = 0; i < nCommand; i++)
        {
            for (int pIndex = 0; pIndex < nPlayer; pIndex++)
            {
                GameObject buttonObject = Instantiate(commandButtonPrefab);
                buttonObject.transform.SetParent(buttonContainer,false);
                buttonObject.GetComponent<CommandButton>().DirectionChanged += (direction) =>
                {
                    UpdateState();
                };
                commandButtons.Add(buttonObject.GetComponent<CommandButton>());
            }
        }
        UpdateState();
        
        
    }

    void UpdateState()
    {
        int[] counters = new int[Command.NumberOfDirections];
        foreach (var commandButton in commandButtons)
        {
            counters[(int) commandButton.direction]++;
        }

        leftCommandText.text = counters[(int) Command.Direction.Left] + "/" + commandNumbers.numberOfLeft;
        bool playActive = true;
        if (counters[(int) Command.Direction.Left] > commandNumbers.numberOfLeft)
        {
            leftCommandText.color = Color.red;
            playActive = false;
        }
        else
        {
            leftCommandText.color = Color.white;

        }
        rightCommandText.text = counters[(int) Command.Direction.Right] + "/" + commandNumbers.numberOfRight;
        if (counters[(int) Command.Direction.Right] > commandNumbers.numberOfRight)
        {
            rightCommandText.color = Color.red;
            playActive = false;
        }
        else
        {
            rightCommandText.color = Color.white;

        }
        upCommandText.text = counters[(int) Command.Direction.Up] + "/" + commandNumbers.numberOfUp;
        if (counters[(int) Command.Direction.Up] > commandNumbers.numberOfUp)
        {
            upCommandText.color = Color.red;
            playActive = false;
        }
        else
        {
            upCommandText.color = Color.white;
        }
        downCommandText.text = counters[(int) Command.Direction.Down] + "/" + commandNumbers.numberOfDown;
        if (counters[(int) Command.Direction.Down] > commandNumbers.numberOfDown)
        {
            downCommandText.color = Color.red;
            playActive = false;
        }
        else
        {
            downCommandText.color = Color.white;

        }

        playButton.interactable = playActive;
        if (playActive)
        {
            playButton.image.color = Color.green;
        }
        else
        {
            playButton.image.color = Color.red;
        }
        
        
    }

    void OnPlayButtonClicked()
    {
        Debug.Log("Play");
        StartCoroutine(SendCommands());
    }

    IEnumerator SendCommands()
    {
        playButton.interactable = false;
        playButton.image.color = Color.red;
        
        const float Interval = 0.5f;
        var wait = new WaitForSeconds(Interval);
        Queue<Command> commands = new Queue<Command>();
        for (int i = 0; i < commandButtons.Count; i += numberOfPlayers)
        {
            
            Command.Direction[] dirs = new Command.Direction[]
            {
                commandButtons[i].direction,
                commandButtons[i + 1].direction,
                commandButtons[i + 2].direction,
                commandButtons[i + 3].direction

            };
            
            commands.Enqueue(new Command(dirs));
            
        }
        
        
        while (commands.Count != 0)
        {
            Command command = commands.Dequeue();
            Debug.Log("Command: " + command.ToString());
            if (SendCommand != null)
            {
                SendCommand(command);
            }
            yield return wait;
        }
        

    }
}