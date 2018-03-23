using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void Routine();

/// <summary>
/// Event management system to trigger events and register callbacks.
/// </summary>

/// <remarks>
/// I took what Thomas showed in the Delegate workshop and wrapped
/// it in a class with a nice interface,
/// and tried to generalize it to work with more than just audio.
/// </remarks>
public class EventManager {

    private static readonly Dictionary<string, Routine> EventTable = new Dictionary<string, Routine>();

    /// <summary>
    /// Register a function to be called when the associated event is triggered.
    /// </summary>
    /// <param name="eventName">The name of the event. Functions are called when Trigger is called with a matching name.</param>
    /// <param name="callback">The function that is called when the event is triggered.</param>
    public static void On(string eventName, Routine callback)
    {
        if (EventTable.ContainsKey(eventName))
        {
            EventTable[eventName] += callback;

        } else
        {
            EventTable.Add(eventName, callback);
        }
    }

    /// <summary>
    /// Trigger an event, calling all functions that are registered to it.
    /// </summary>
    /// <param name="eventName">The name of the event that is triggered.</param>
    public static void Trigger(string eventName)
    {
        if (EventTable.ContainsKey(eventName))
        {
            EventTable[eventName]();
        }
    }

    /// <summary>
    /// Remove all registered events from the event system.
    /// </summary>
    public static void ClearAll()
    {
        EventTable.Clear();
    }
}
