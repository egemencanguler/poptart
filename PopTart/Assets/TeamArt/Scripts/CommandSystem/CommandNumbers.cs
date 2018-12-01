using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandNumbers
{
    public readonly int numberOfLeft, numberOfRight, numberOfUp, numberOfDown;
    
    public CommandNumbers(int nLeft, int nRight, int nUp, int nDown)
    {
        numberOfLeft = nLeft;
        numberOfRight = nRight;
        numberOfUp = nUp;
        numberOfDown = nDown;
    }
    
    
}
