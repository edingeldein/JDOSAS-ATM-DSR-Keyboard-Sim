using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardController : MonoBehaviour
{

    public bool ShiftClicked = false;
    private Queue<string> keyQueue;

    void Start()
    {
        keyQueue = new Queue<string>();
    }

    void Update()
    {
        if (keyQueue.Count == 0) return;
        var key = keyQueue.Dequeue();
        Debug.Log(key);
    }

    public void KeypressHandler(string keyPressed)
    {
        keyQueue.Enqueue(keyPressed);
    }
}
