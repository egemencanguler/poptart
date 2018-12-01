using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandButton : MonoBehaviour
{
    public Command.Direction direction;
    public Sprite rightArrowSprite;
    public Sprite waitSprite;
    public Image buttonIcon;
    public event System.Action<Command.Direction> DirectionChanged;

    void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            int d = (int) direction;
            d = (d + 1) % Command.NumberOfDirections;
            direction = (Command.Direction) d;
            if (DirectionChanged != null)
            {
                DirectionChanged(direction);
            }
            UpdateState();
        });
        direction = Command.Direction.None;
        UpdateState();
    }

    void UpdateState()
    {
        if (direction == 0)
        {
            buttonIcon.sprite = waitSprite;
        }
        else
        {
            buttonIcon.sprite = rightArrowSprite;
            float angle = 0;
            switch (direction)
            {
                    case Command.Direction.Left:
                        angle = 180;
                        break;
                    case Command.Direction.Right:
                        angle = 0;
                        break;
                    case Command.Direction.Up:
                        angle = 90;
                        break;
                    case Command.Direction.Down:
                        angle = -90;
                        break;
            }
            buttonIcon.transform.rotation = Quaternion.Euler(0,0,angle);
        }
    }
}