using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomObjects;

public class KeyboardController : MonoBehaviour
{

    public bool ShiftEnabled = false;
    private Queue<KeyPressData> keyQueue;

    void Start()
    {
        keyQueue = new Queue<KeyPressData>();
    }

    void Update()
    {
        if (keyQueue.Count == 0) return;
        var key = keyQueue.Dequeue();
        Debug.Log($"({key.KeyType}, {key.Value}, {key.Command})");
    }

    public void KeypressHandler(KeyPressData keyPressed)
    {
        keyQueue.Enqueue(keyPressed);
    }
}
