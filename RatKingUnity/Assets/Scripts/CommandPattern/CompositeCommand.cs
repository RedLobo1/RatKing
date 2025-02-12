using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CompositeCommand : ICommand
{
    private readonly List<ICommand> _childCommands = new List<ICommand>();

    public void AddToList(ICommand command)
    {
        _childCommands.Add(command);
    }
    public void Execute()
    {
        foreach (var command in _childCommands)
        {
            command.Execute();
            Debug.Log(command.ToString()); 
        }
        //Execute all child commands
    }
    public void Undo()
    {
        Debug.Log("CompositeBack");

        for (int i = _childCommands.Count-1; i >= 0; i--)
        {
            Debug.Log(i);
            _childCommands.ElementAt(i).Undo();
        }
    }
}

