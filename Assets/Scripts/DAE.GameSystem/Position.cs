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

public class Position : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private UnityEvent OnActivate;

    [SerializeField]
    private UnityEvent OnDeactivate;

    public event EventHandler<PositionEventArgs> Clicked;

    //[SerializeField]
    //private GameLoop<Piece> _loop;


    public void Deactivated()
        => OnDeactivate.Invoke();

    public void Activated()
        => OnActivate.Invoke();

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log($"Clicked { gameObject.name}");
        //Activated();
        OnClicked(new PositionEventArgs(this));
    }

    public virtual void OnClicked(PositionEventArgs eventArgs)
    {
        var handler = Clicked;
        handler?.Invoke(this, eventArgs);
    }
}
