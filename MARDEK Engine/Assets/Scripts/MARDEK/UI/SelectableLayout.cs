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
        [SerializeField] int horizontalConstraint;
        [SerializeField] int verticalConstraint;
        [SerializeField] ScrollRect scrollRect;
        [SerializeField] int numFittingEntries;
        [SerializeField] bool invertHorizontalInput = false;
        int currentScrollIndex = 0;
        int index = 0;
        int Index
        {
            get
            {
                if (Selectables.Count == 0)
                    return 0;
                return (index + Selectables.Count) % Selectables.Count;
            }
            set
            {
                if (Selectables.Count == 0)
                    index = 0;
                else
                    index = (value + Selectables.Count) % Selectables.Count;
            }
        }

        Selectable[] selectables;
        List<Selectable> Selectables
        {
            get
            {
                List<Selectable> returnList = new List<Selectable>();
                foreach (var s in selectables)
                    if (s && s.IsValid())
                        returnList.Add(s);
                return returnList;
            }
        }
        Selectable currentlySelected = null;

        private void CacheSelectables()
        {
            selectables = GetComponentsInChildren<Selectable>(true);
            foreach (var s in Selectables)
                if (s != currentlySelected)
                    s.Deselect();
            UpdateSelectionAtIndex(false);
        }
        private void OnValidate()
        {
            var layout = GetComponent<GridLayoutGroup>();
            if (layout)
            {
                if(layout.constraint == GridLayoutGroup.Constraint.FixedColumnCount)
                {
                    verticalConstraint = layout.constraintCount;
                    horizontalConstraint = 0;
                }
                else if (layout.constraint == GridLayoutGroup.Constraint.FixedRowCount)
                {
                    verticalConstraint = 0;
                    horizontalConstraint = layout.constraintCount;
                }
            }
        }
        private void OnEnable()
        {
            CacheSelectables();
        }
        private void Update()
        {
            if (currentlySelected == null)
                UpdateSelectionAtIndex(false);
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
            if (index > Selectables.Count)
                index = 0;
            currentlySelected = Selectables[Index];
            currentlySelected.Select(playSFX);
            if (numFittingEntries > 0 && scrollRect != null)
            {
                int desiredScrollIndex = Index / verticalConstraint;
                if (desiredScrollIndex - currentScrollIndex >= numFittingEntries) SetScrollIndex(1 + desiredScrollIndex - numFittingEntries);
                if (desiredScrollIndex - currentScrollIndex < 0) SetScrollIndex(desiredScrollIndex);
            }
        }
        void SetScrollIndex(int newScrollIndex)
        {
            currentScrollIndex = newScrollIndex;
            int numTotalEntries = Selectables.Count / verticalConstraint;
            if (Selectables.Count % verticalConstraint != 0) numTotalEntries += 1;

            int numNonFittingEntries = numTotalEntries - numFittingEntries;
            float scrollAmount = newScrollIndex / (float) numNonFittingEntries;
            
            if (scrollRect.vertical) scrollRect.verticalNormalizedPosition = 1f - scrollAmount;
            else scrollRect.horizontalNormalizedPosition = scrollAmount;
        }

        public void HandleMovementInput(InputAction.CallbackContext ctx)
        {
            if (enabled == false)
                return;
            var value = ctx.ReadValue<Vector2>();
            if (value.x == 0)
                HandleVerticalInput(value.y);
            if (value.y == 0)
                HandleHorizontalInput(value.x);
        }
        void HandleVerticalInput(float value)
        {   
            if (value > 0)
                Index -= verticalConstraint;
            else
                Index += verticalConstraint;
            UpdateSelectionAtIndex();
        }
        void HandleHorizontalInput(float value)
        {
            var amount = horizontalConstraint;
            if (invertHorizontalInput)
                amount *= -1;

            if (value > 0)
                Index += amount;
            else
                Index -= amount;            
            UpdateSelectionAtIndex();
        }
    }
}