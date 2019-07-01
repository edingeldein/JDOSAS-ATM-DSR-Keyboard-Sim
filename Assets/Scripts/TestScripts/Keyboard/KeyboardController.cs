using System;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardController : MonoBehaviour
{

    private Queue<string> _keyQueue;
    private bool _shifted;

    public bool Queued { get { return _keyQueue.Count > 0; } }
    public string Dequeue {get { return _keyQueue.Dequeue(); } }

    private void Start() 
    {
        _keyQueue = new Queue<string>();    
        _shifted = false;
    }

    public void QueueKeypress(string keypress) => _keyQueue.Enqueue(keypress.Trim());
    public void SetShift(bool shift) => _shifted = shift;
    public bool GetShift() => _shifted;
}