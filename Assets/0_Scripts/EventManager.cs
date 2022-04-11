using System;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }
    private Dictionary<string, Action<object[]>> _callbacks = new Dictionary<string, Action<object[]>>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            Debug.LogWarning($"Found duplicate of 'EventManager' on {gameObject.name}");
        }
    }

    public void Subscribe(string eventId, Action<object[]> callback)
    {
        if (!_callbacks.ContainsKey(eventId))
            _callbacks.Add(eventId, callback);
        else
        {
            _callbacks[eventId] += callback;
        }
    }

    public void Unsubscribe(string eventId, Action<object[]> callback)
    {
        if (!_callbacks.ContainsKey(eventId)) return; // esto es si no existe basicamente
        _callbacks[eventId] -= callback;
    }

    public void Trigger(string eventId, params object[] parameters)
    {
        if (!_callbacks.ContainsKey(eventId)) return;

        _callbacks[eventId](parameters);
    }

}