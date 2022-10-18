using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace MARDEK.UI
{
    [RequireComponent(typeof(GridLayoutGroup), typeof(InputReader))]
    public class SelectableLayout : MonoBehaviour
    {
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

        GridLayoutGroup layout;
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
        private void Awake()
        {
            layout = GetComponent<GridLayoutGroup>();
        }
        private void OnEnable()
        {
            CacheSelectables();
        }
        private void Update()
        {
            if (selectables.Length != transform.childCount)
            {
                CacheSelectables();
            }
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
                int desiredScrollIndex = Index / layout.constraintCount;
                if (desiredScrollIndex - currentScrollIndex >= numFittingEntries) SetScrollIndex(1 + desiredScrollIndex - numFittingEntries);
                if (desiredScrollIndex - currentScrollIndex < 0) SetScrollIndex(desiredScrollIndex);
            }
        }
        void SetScrollIndex(int newScrollIndex)
        {
            currentScrollIndex = newScrollIndex;
            int numTotalEntries = Selectables.Count / layout.constraintCount;
            if (Selectables.Count % layout.constraintCount != 0) numTotalEntries += 1;

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
            if (layout.constraint == GridLayoutGroup.Constraint.FixedRowCount && layout.constraintCount == 1) return;

            if (layout.constraint == GridLayoutGroup.Constraint.FixedColumnCount && layout.constraintCount != 1)
            {
                if (value > 0) Index -= layout.constraintCount;
                else Index += layout.constraintCount;
            } else {
                if (value > 0) Index--;
                else Index++;
            }

            UpdateSelectionAtIndex();
        }
        void HandleHorizontalInput(float value)
        {
            if (layout.constraint == GridLayoutGroup.Constraint.FixedColumnCount && layout.constraintCount == 1) return;

            if (invertHorizontalInput)
                value = -value;
            if (layout.constraint == GridLayoutGroup.Constraint.FixedRowCount && layout.constraintCount != 1)
            {
                if (value > 0) Index += layout.constraintCount;
                else Index -= layout.constraintCount;
            } else {
                if (value > 0) Index++;
                else Index--;
            }
            
            UpdateSelectionAtIndex();
        }
    }
}