using DAE.BoardSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DAE.SelectionSystem { 

public class SelectableItemEventArgs<TSelectableItem> : EventArgs
    {
        public TSelectableItem SelectableItem { get; }

        public SelectableItemEventArgs(TSelectableItem selectableItem)
        {
            SelectableItem = selectableItem;
        }
    }
public class SelectionManager<TSelectableItem> 
{
        public event EventHandler<SelectableItemEventArgs<TSelectableItem>> Selected;
        public event EventHandler<SelectableItemEventArgs<TSelectableItem>> Deselected;

        private HashSet<TSelectableItem> _selectableItems = new HashSet<TSelectableItem>();

        public IReadOnlyCollection<TSelectableItem> SelectableItems => _selectableItems;

        public TSelectableItem SelectedItem => _selectableItems.First();

        public bool HasSelection => _selectableItems.Count > 0;
        public bool IsSelected(TSelectableItem selectableItem)
            => _selectableItems.Contains(selectableItem);

        public bool Deselect(TSelectableItem selectableItem)
        {
            //Debug.Log($"Deselected: {selectableItem}");
            //return _selectableItems.Remove(selectableItem);

            if (_selectableItems.Remove(selectableItem))
            {
                OnDeselected(new SelectableItemEventArgs<TSelectableItem>(selectableItem));
                return true;
            }
            return false;
        }

     

        public bool Select(TSelectableItem selectableItem)
        {
            //Debug.Log($"Selected: {selectableItem}");
            //return _selectableItems.Add(selectableItem);

            if(_selectableItems.Add(selectableItem))
            {
                OnSelected(new SelectableItemEventArgs<TSelectableItem>(selectableItem));
                return true;
            }
            return false;
        }

        public bool Toggle(TSelectableItem selectableItem)
        {
            if (_selectableItems.Contains(selectableItem))
                return !Deselect(selectableItem);
            else
                return Select(selectableItem);
        }

        public void DeselectAll()
        {
            //while(SelectableItems.Count > 0)
            //{
            //    Deselect(_selectableItems.First());
            //}
            foreach(var selectableItem in _selectableItems.ToList())
            {
                Deselect(selectableItem);
            }
        }

        protected virtual void OnSelected(SelectableItemEventArgs<TSelectableItem> eventArgs)
        {
            var handler = Selected;
            handler?.Invoke(this, eventArgs);
        }

        protected virtual void OnDeselected(SelectableItemEventArgs<TSelectableItem> eventArgs)
        {
            var handler = Deselected;
            handler?.Invoke(this, eventArgs);
        }
    }
}