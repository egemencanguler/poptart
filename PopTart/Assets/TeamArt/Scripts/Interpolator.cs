using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class Interpolator
{
    static Dictionary<int, Vector2> posBefore = new Dictionary<int, Vector2>();
    static Dictionary<int, Vector2> posAfter = new Dictionary<int, Vector2>();

    public static void BeforeCommand()
    {
        posBefore.Clear();
        Unit[] units = GameObject.FindObjectsOfType<Unit>();
        foreach (var unit in units)
        {
            posBefore.Add(unit.gameObject.GetInstanceID(),unit.transform.position);
        }
    }

    public static void AfterCommand()
    {
        posAfter.Clear();
        Unit[] units = GameObject.FindObjectsOfType<Unit>();
        foreach (var unit in units)
        {
            posAfter.Add(unit.gameObject.GetInstanceID(),unit.transform.position);
        }
    }

    public static IEnumerator Interpolate(float duration)
    {
        Unit[] units = GameObject.FindObjectsOfType<Unit>();
        float timer = 0;

        while (timer < duration)
        {
            float nTime = timer / duration;
            foreach (var unit in units)
            {
                if (unit != null)
                {
                    int id = unit.gameObject.GetInstanceID();
                    if (posAfter.ContainsKey(id) && posBefore.ContainsKey(id))
                    {
                        Vector2 pBefore = posBefore[id];
                        Vector2 pAfter = posAfter[id];
                        unit.transform.position = Vector2.Lerp(pBefore, pAfter, nTime);
                    }
                    
                }
            }
            timer += Time.deltaTime;
            yield return null;
        }
        
        
        foreach (var unit in units)
        {
            if (unit != null)
            {
                Vector2 pAfter = posAfter[unit.gameObject.GetInstanceID()];
                unit.transform.position = pAfter;
            }
        }
        
        
    }

}
