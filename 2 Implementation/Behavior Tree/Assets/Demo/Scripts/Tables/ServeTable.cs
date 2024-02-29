using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServeTable : Table
{
    [SerializeField] private Plate _input;
    
    public override bool Interact(Component item)
    {
        // TODO: Finished a quest when serves
        return Place(item, _input);
    }

    public override Component Interact(out bool success)
    {
        Debug.LogWarning($"You are trying to grab something from {GetType().Name} which is not possible.");
        success = false;
        return null;
    }
}