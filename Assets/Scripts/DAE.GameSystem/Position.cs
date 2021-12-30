using DAE.HexesSystem;
using DAE.GameSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PositionEventArgs: EventArgs
{
    public Position Position { get; }

    public PositionEventArgs(Position position)
    {
        Position = position;
    }
}

public class Position : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private UnityEvent OnActivate;

    [SerializeField]
    private UnityEvent OnDeactivate;

    public event EventHandler<PositionEventArgs> Dropped;
    public event EventHandler<PositionEventArgs> Entered;
    public event EventHandler<PositionEventArgs> Exited;

    //[SerializeField]
    //private GameLoop<Piece> _loop;

    public void Deactivated()
        => OnDeactivate.Invoke();

    public void Activated()
        => OnActivate.Invoke();

    #region Event Interface Impl
    public void OnPointerEnter(PointerEventData eventData)
    {
        OnEntered(new PositionEventArgs(this));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnExited(new PositionEventArgs(this));
    }

    public void OnDrop(PointerEventData eventData)
    {
        OnDropped(new PositionEventArgs(this));
    }
    #endregion

    #region Event Trigger Methods

    public virtual void OnEntered(PositionEventArgs eventArgs)
    {
        var handler = Entered;
        handler?.Invoke(this, eventArgs);
    }

    public virtual void OnExited(PositionEventArgs eventArgs)
    {
        var handler = Exited;
        handler?.Invoke(this, eventArgs);
    }

    public void OnDropped(PositionEventArgs eventData)
    {
        var handler = Dropped;
        handler?.Invoke(this, eventData);
    } 
    #endregion
}
