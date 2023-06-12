using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subject : MonoBehaviour
{
    readonly List<IObserver> observers = new();
    protected void Attach(IObserver ob)
    {
        observers.Add(ob);
    }

    protected void Detach(IObserver ob)
    {
        observers.Remove(ob);
    }

    protected void NotifyAll()
    {
        foreach( IObserver ob in observers)
        {
            ob.Notify(this);
        }
    }
}
