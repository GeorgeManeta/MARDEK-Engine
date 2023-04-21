using UnityEngine;
using UnityEngine.InputSystem;
using System;
using TMPro;

public class KeyRebind : MonoBehaviour
{
    [SerializeField] InputActionReference actionReference = null;
    [SerializeField, Range(0, 3)] int compositeBindingIndex = 0;
    InputActionRebindingExtensions.RebindingOperation rebindingOperation;

    [SerializeField] TextMeshProUGUI actionNameText = null;
    [SerializeField] TextMeshProUGUI bindText = null;
    [SerializeField, HideInInspector] Color originalColor;
    Color rebindingColor = Color.red;


    bool isComposite { get { return actionReference.action.bindings[0].isComposite; } }
    InputBinding Binding
    {
        get
        {
            if (isComposite == false)
                return actionReference.action.bindings[0];
            return actionReference.action.bindings[1 + compositeBindingIndex];
        }
    }
    string ActionName
    {
        get
        {
            var name = actionReference.action.name;
            var bindingName = Binding.name;
            if (string.IsNullOrEmpty(bindingName) == false)
                return $"{name} ({bindingName})";
            return name;
        }
    }

    private void OnValidate()
    {
        originalColor = bindText.color;
        actionNameText.text = ActionName;
        gameObject.name = ActionName;
        bindText.text = "-";
        bindText.color = originalColor;
    }

    private void OnEnable()
    {
        UpdateBindText();
    }

    void UpdateBindText()
    {
        var bindPath = Binding.effectivePath;
        var options = InputControlPath.HumanReadableStringOptions.None;
        bindText.text = InputControlPath.ToHumanReadableString(bindPath, options);
        bindText.color = originalColor;
    }

    public void Rebind()
    {
        bindText.color = rebindingColor;

        actionReference.action.actionMap.Disable();
        rebindingOperation = actionReference.action.PerformInteractiveRebinding();

        if(isComposite)
            rebindingOperation.WithTargetBinding(1+compositeBindingIndex);

        rebindingOperation.WithControlsExcluding("Mouse");
        rebindingOperation.OnMatchWaitForAnother(0.1f);
        rebindingOperation.OnComplete(operation => OnEndRebind());
        rebindingOperation.Start();
    }

    private void OnEndRebind()
    {
        actionReference.action.actionMap.Enable();
        rebindingOperation.Dispose();
        UpdateBindText();
        InputReader.RefreshInputReaders();
        SaveRebind();
    }

    void SaveRebind()
    {
        foreach (var binding in actionReference.action.bindings)
        {
            string key = binding.id.ToString();
            string val = binding.overridePath;
            PlayerPrefs.SetString(key, val);
        }        
    }
}