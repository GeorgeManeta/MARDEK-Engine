using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using System.Threading.Tasks;

public class InputReader : MonoBehaviour
{
    static List<InputReader> readers = new List<InputReader>();

    [SerializeField] List<InputActionBind> binds = new List<InputActionBind>();
    List<InputAction> bindedActions = new List<InputAction>();

    async Task BindActions()
    {
        await Task.Yield();
        foreach (InputActionBind bind in binds)
        {
            if (bind.action != null)
            {
                var action = bind.action.action.Clone();
                bind.Bind(action);
                action.Enable();
                bindedActions.Add(action);
            }
            else
                Debug.LogError("bind action shouldn't be null");
        }
    }

    async Task UnbindActions()
    {
        await Task.Yield();
        for(int i = bindedActions.Count-1; i >= 0; i--)
        {
            bindedActions[i].Dispose();
            bindedActions.RemoveAt(i);
        }
    }

    private async void OnEnable()
    {
        readers.Add(this);
        await BindActions();
    }

    private async void OnDisable()
    {
        readers.Remove(this);
        await UnbindActions();
    }

    public static void RefreshInputReaders()
    {
        foreach (var r in readers)
            r.Refresh();
    }

    async void Refresh()
    {
        await UnbindActions();
        await BindActions();
    }

    [System.Serializable]
    class InputActionBind
    {
        [SerializeField] public InputActionProperty action = default;
        [SerializeField] bool bindStarted = false;
        [SerializeField] bool bindPerformed = true;
        [SerializeField] bool bindCanceled = false;
        [SerializeField] UnityEvent<InputAction.CallbackContext> _event = default;

        public void Bind(InputAction action)
        {
            if(bindStarted)
                action.started += _event.Invoke;
            if (bindPerformed)
                action.performed += _event.Invoke;
            if (bindCanceled)
                action.canceled += _event.Invoke;
        }
    }
}
