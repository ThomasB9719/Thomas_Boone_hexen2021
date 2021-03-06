using DAE.HexesSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[Serializable]
public class HighLightEvent: UnityEvent<bool> { }
class PieceEventArgs: EventArgs
{
    public Piece Piece { get; }
    public PieceEventArgs(Piece piece) 
        => Piece = piece;
}
class Piece: MonoBehaviour, IPointerClickHandler, IPiece
{
    [SerializeField]
    private HighLightEvent _onHighlight;

    [SerializeField]
    private PieceType _pieceType;

    public bool Highlight
    {
        set
        {
            _onHighlight.Invoke(value);
        }
    }

    public bool Moved { get; set; }

    public PieceType PieceType => _pieceType;

    public event EventHandler<PieceEventArgs> Clicked;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        OnClicked(this, new PieceEventArgs(this));
    }

    public override string ToString()
    {
        return gameObject.name;
    }

    protected virtual void OnClicked(object source, PieceEventArgs e)
    {
        var handle = Clicked;
        handle?.Invoke(this, e);
    }

    public void MoveTo(Vector3 worldPosition)
    {
        transform.position = worldPosition;
    }

    public void Place(Vector3 worldPosition)
    {
        transform.position = worldPosition;
        gameObject.SetActive(true);
    }

    public void Taken()
    {
        gameObject.SetActive(false);
    }
}

