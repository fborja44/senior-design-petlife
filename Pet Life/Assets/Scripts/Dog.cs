using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour
{
    public static Dog instance { get; private set; }

    public bool isDirty;
    public bool isHungry;
    public bool isThirsty;
    public bool isTired;
    public bool needsToPotty;
    public bool needsLove;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool hygeine ()
    {
        if (isDirty == true)
        {
            isDirty = false;
        }
        else
        {
            isDirty = true;
        }
    }

    bool water()
    {
        if (isThirsty == true)
        {
            isThirsty = false;
        }
        else
        {
            isThirsty = true;
        }
    }

    bool food()
    {
        if (isHungry == true)
        {
            isHungry = false;
        }
        else
        {
            isHungry = true;
        }
    }

    bool sleepin()
    {
        if (isTired == true)
        {
            isTired = false;
        }
        else
        {
            isTired = true;
        }
    }

    bool potty()
    {
        if (needsToPotty == true)
        {
            needsToPotty = false;
        }
        else
        {
            needsToPotty = true;
        }
    }

    bool love()
    {
        if (needsLove == true)
        {
            needsLove = false;
        }
        else
        {
            needsLove = true;
        }
    }
}

