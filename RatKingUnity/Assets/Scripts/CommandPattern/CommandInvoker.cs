using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static internal class CommandInvoker
{
    static Stack<ICommand> _undoStack = new Stack<ICommand>();

    public static void ExecuteCommand(ICommand command)
    {
        command.Execute();
        _undoStack.Push(command);

    }

    public static void Undo()
    {
        if (_undoStack.Count == 0) return;
        ICommand command = _undoStack.Pop();
        command.Undo();
    }
}
