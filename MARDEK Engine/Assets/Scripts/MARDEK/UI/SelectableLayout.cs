using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace MARDEK.UI
{
    public class SelectableLayout : MonoBehaviour
    {
        [SerializeField] bool resetIndexOnEnable = false;
        [SerializeField] GridLayoutGroup.Axis startAxis = GridLayoutGroup.Axis.Vertical;
        [SerializeField] int constraintCount;
        [SerializeField] ScrollRect scrollRect;
        [SerializeField] int numFittingEntries;
        [SerializeField] bool invertHorizontalInput = false;
        int currentScrollIndex = 0;
        int _index = 0;
        int Index
        {
            get
            {
                if (_index < 0 || _index >= Selectables.Count)
                    _index = 0;
                return _index;
            }
            set
            {
                if(_index == 0 && value == -1)
                {
                    _index = Selectables.Count - 1;
                    return;
                }
                _index = value;
                if (constraintCount != 0)
                {
                    var constraintShift = (_index % constraintCount + constraintCount) % constraintCount;
                    if (_index > Selectables.Count)
                        _index = constraintShift;
                    if (_index < 0)
                    {
                        _index = Mathf.FloorToInt((float)Selectables.Count / (float)constraintCount) * constraintCount + constraintShift;
                        if (_index >= Selectables.Count)
                            _index -= constraintCount;
                    }
                }
                if (_index >= Selectables.Count)
                    _index = 0;
                if (_index < 0)
                    _index = Selectables.Count - 1;
            }
        }

        List<Selectable> Selectables
        {
            get
            {
                var _selectables = GetComponentsInChildren<Selectable>(true);
                List<Selectable> returnList = new List<Selectable>();
                foreach (var s in _selectables)
                    if (s && s.IsValid())
                        returnList.Add(s);
                return returnList;
            }
        }
        Selectable currentlySelected = null;

        private void OnValidate()
        {
            var layout = GetComponent<GridLayoutGroup>();
            if(layout != null)
            {
                startAxis = layout.startAxis;
                if (startAxis == GridLayoutGroup.Axis.Horizontal && layout.constraint == GridLayoutGroup.Constraint.FixedColumnCount)
                {
                    constraintCount = layout.constraintCount;
                }
                if (startAxis == GridLayoutGroup.Axis.Vertical && layout.constraint == GridLayoutGroup.Constraint.FixedRowCount)
                {
                    constraintCount = layout.constraintCount;
                }
            }
        }
        private void OnEnable()
        {
            if (resetIndexOnEnable)
                Index = 0;
            foreach (var s in Selectables)
                s.Deselect();
            UpdateSelectionAtIndex();
        }
        private void Update()
        {
            if (currentlySelected == null)
                UpdateSelectionAtIndex(false);
        }
        public void HandleMovementInput(InputAction.CallbackContext ctx)
        {
            if (enabled == false)
                return;
            var prevIndex = Index;

            var value = ctx.ReadValue<Vector2>();
            HandleHorizontalInput(value.x);
            HandleVerticalInput(value.y);

            if(Index != prevIndex)
                UpdateSelectionAtIndex();
        }
        private void HandleHorizontalInput(float x)
        {
            if (x == 0)
                return;

            var amount = x > 0 ? 1 : -1;
            if (invertHorizontalInput)
                amount *= -1;
            
            if(startAxis == GridLayoutGroup.Axis.Horizontal)
                Index += amount;
            else
                Index += amount * constraintCount;
        }
        private void HandleVerticalInput(float x)
        {
            if (x == 0)
                return;

            var amount = x < 0 ? 1 : -1;

            if (startAxis == GridLayoutGroup.Axis.Vertical)
                Index += amount;
            else
                Index += amount * constraintCount;
        }

        public void UpdateSelectionAtIndex(bool playSFX = true)
        {
            if (currentlySelected)
                currentlySelected.Deselect();
            if (Selectables.Count == 0)
            {
                currentlySelected = null;
                return;
            }
            currentlySelected = Selectables[Index];
            currentlySelected.Select(playSFX);
            if (numFittingEntries > 0 && scrollRect != null)
            {
                int desiredScrollIndex = Index / constraintCount;
                if (desiredScrollIndex - currentScrollIndex >= numFittingEntries) SetScrollIndex(1 + desiredScrollIndex - numFittingEntries);
                if (desiredScrollIndex - currentScrollIndex < 0) SetScrollIndex(desiredScrollIndex);
            }
        }
        void SetScrollIndex(int newScrollIndex)
        {
            currentScrollIndex = newScrollIndex;
            int numTotalEntries = Selectables.Count / constraintCount;
            if (Selectables.Count % constraintCount != 0) numTotalEntries += 1;

            int numNonFittingEntries = numTotalEntries - numFittingEntries;
            float scrollAmount = newScrollIndex / (float) numNonFittingEntries;
            
            if (scrollRect.vertical) scrollRect.verticalNormalizedPosition = 1f - scrollAmount;
            else scrollRect.horizontalNormalizedPosition = scrollAmount;
        }
    }
}