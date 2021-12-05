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

public class PositionView : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private UnityEvent OnActivate;

    [SerializeField]
    private UnityEvent OnDeactivate;

    public event EventHandler<PositionEventArgs> Clicked;

    //[SerializeField]
    //private GameLoop<Piece> _loop;

    private Position _model;
    public Position Model
    {
        get
        {
            return _model;
        }
        set
        {
            if (_model != null)
            {
                _model.Activated -= PositionActivated;
                _model.Deactivated -= PositionDeactivated;
            }
            
            _model = value;
            if (_model != null)
            {
                _model.Activated += PositionActivated;
                _model.Deactivated += PositionDeactivated;
            }
        }
    }

    private void PositionDeactivated(object sender, EventArgs e)
        => OnDeactivate.Invoke();

    private void PositionActivated(object sender, EventArgs e)
        => OnActivate.Invoke();

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClicked(new PositionEventArgs(Model));
    }

    public virtual void OnClicked(PositionEventArgs eventArgs)
    {
        var handler = Clicked;
        handler?.Invoke(this, eventArgs);
    }
}
