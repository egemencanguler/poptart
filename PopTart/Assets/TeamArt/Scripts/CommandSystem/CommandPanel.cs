using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandPanel : MonoBehaviour {
    public static event System.Action<Command> SendCommand;

    public RectTransform buttonContainer;
    public GameObject commandButtonPrefab;
    public GameObject commandNumberPrefab;
    public GameObject dogIconPrefab;

    public GridLayoutGroup buttonGrid;
    public Text leftCommandText, rightCommandText, upCommandText, downCommandText;
    public Button playButton;

    List<CommandButton> commandButtons = new List<CommandButton> ( );
    CommandNumbers commandNumbers;
    int numberOfPlayers;
    int numberOfCommands;

    public AudioClip pause, play;
    public AudioSource audioSource;

    void Awake ( ) {
        playButton.onClick.AddListener (OnPlayButtonClicked);
    }

    public void Setup (int nPlayer, int nCommand, int nLeft, int nRight, int nUp, int nDown) {
        foreach (Transform child in buttonContainer) {
            Destroy (child.gameObject);
        }

        numberOfPlayers = nPlayer;
        numberOfCommands = nCommand;

        commandNumbers = new CommandNumbers (nLeft, nRight, nUp, nDown);
        commandButtons.Clear ( );
        buttonGrid.constraintCount = nPlayer + 1;

        Instantiate (dogIconPrefab).transform.SetParent (buttonContainer, false);

        for (int i = 0; i < numberOfPlayers; i++) {
            GameObject dogIcon = Instantiate (dogIconPrefab);
            dogIcon.transform.SetParent (buttonContainer, false);
            dogIcon.GetComponent<Image> ( ).sprite = Board.Instance.dogSprites[i];
        }

        for (int i = 0; i < nCommand; i++) {
            GameObject commandNumberObject = Instantiate (commandNumberPrefab);
            commandNumberObject.transform.SetParent (buttonContainer, false);
            commandNumberObject.GetComponent<CommandNumber> ( ).Setup (i);
            for (int pIndex = 0; pIndex < nPlayer; pIndex++) {
                GameObject buttonObject = Instantiate (commandButtonPrefab);
                buttonObject.transform.SetParent (buttonContainer, false);
                buttonObject.GetComponent<CommandButton> ( ).DirectionChanged += (direction) => {
                    UpdateState ( );
                };
                commandButtons.Add (buttonObject.GetComponent<CommandButton> ( ));
            }
        }
        UpdateState ( );

    }

    void UpdateState ( ) {
        int[ ] counters = new int[Command.NumberOfDirections];
        foreach (var commandButton in commandButtons) {
            counters[(int) commandButton.direction]++;
        }

        leftCommandText.text = counters[(int) Command.Direction.Left] + "/" + commandNumbers.numberOfLeft;
        bool playActive = true;
        if (counters[(int) Command.Direction.Left] > commandNumbers.numberOfLeft) {
            leftCommandText.color = Color.red;
            playActive = false;
        } else {
            leftCommandText.color = Color.white;

        }
        rightCommandText.text = counters[(int) Command.Direction.Right] + "/" + commandNumbers.numberOfRight;
        if (counters[(int) Command.Direction.Right] > commandNumbers.numberOfRight) {
            rightCommandText.color = Color.red;
            playActive = false;
        } else {
            rightCommandText.color = Color.white;

        }
        upCommandText.text = counters[(int) Command.Direction.Up] + "/" + commandNumbers.numberOfUp;
        if (counters[(int) Command.Direction.Up] > commandNumbers.numberOfUp) {
            upCommandText.color = Color.red;
            playActive = false;
        } else {
            upCommandText.color = Color.white;
        }
        downCommandText.text = counters[(int) Command.Direction.Down] + "/" + commandNumbers.numberOfDown;
        if (counters[(int) Command.Direction.Down] > commandNumbers.numberOfDown) {
            downCommandText.color = Color.red;
            playActive = false;
        } else {
            downCommandText.color = Color.white;

        }

        playButton.interactable = playActive;
        if (playActive) {
            playButton.image.color = Color.green;
        } else {
            playButton.image.color = Color.red;
        }

    }

    void OnPlayButtonClicked ( ) {
        StartCoroutine (SendCommands ( ));
    }

    int turn = 0;

    IEnumerator SendCommands ( ) {

        playButton.interactable = false;
        playButton.image.color = Color.red;

        audioSource.clip = play;
        audioSource.volume = 0.4f;
        audioSource.Play ( );

        const float Interval = 0.5f;
        var wait = new WaitForSeconds (Interval);
        Queue<Command> commands = new Queue<Command> ( );
        for (int i = 0; i < numberOfCommands; i++) {
            int idx = i * numberOfPlayers;
            Command.Direction[ ] dirs = new Command.Direction[numberOfPlayers];
            for (int p = 0; p < dirs.Length; p++) {
                dirs[p] = commandButtons[idx + p].direction;
            }

            commands.Enqueue (new Command (i, dirs));

        }

        while (commands.Count != 0) {
            Command command = commands.Dequeue ( );
            Interpolator.BeforeCommand ( );
            if (SendCommand != null) {

                Debug.Log ("Turn: " + turn++);

                SendCommand (command);
                Board.Instance.Turn ( );
            }
            Interpolator.AfterCommand ( );
            yield return Interpolator.Interpolate (0.5f);
        }

        playButton.interactable = true;
        playButton.image.color = Color.green;

        audioSource.clip = pause;
        audioSource.volume = 1f;
        audioSource.Play ( );

        turn = 0;
    }
}