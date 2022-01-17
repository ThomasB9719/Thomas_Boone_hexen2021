using DAE.BoardSystem;
using DAE.GameSystem.GameStates;
using DAE.HexesSystem.Moves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DAE.GameSystem.Cards
{
    class BombMove: CardBase
    {
        public GameLoop GameLoop { get; set; }
        public override List<Position> Positions(Board<Position, Piece> board, Grid<Position> grid, Piece piece, Position positionBoard)
        {
            //List<Position> allPositions = new List<Position>();
            //allPositions.Add(positionBoard);

            //if (board.TryGetPieceAt(positionBoard, out var toPiece))
            //    allPositions.Remove(positionBoard);

            var allPositions = new MovementHelper<Position, Piece>(board, grid, piece, positionBoard)
                .NorthEastBomb(positionBoard, 1)
                .EastBomb(positionBoard, 1)
                .SouthEastBomb(positionBoard, 1)
                .SouthWestBomb(positionBoard, 1)
                .WestBomb(positionBoard, 1)
                .NorthWestBomb(positionBoard, 1)
                .Collect();

            allPositions.Add(positionBoard);

            return allPositions;
        }

        public override void Execute(Board<Position, Piece> board, Grid<Position> grid, Piece piece, Position position)
        {
            List<Position> positions = Positions(board, grid, piece, position);

            foreach (Position availablePosition in positions)
            {               
                if (board.TryGetPieceAt(availablePosition, out var toPiece))
                {
                    if(toPiece.PieceType == HexesSystem.PieceType.Player)
                    {
                        Debug.Log("Player tasks");
                        board.Take(toPiece);

                        GameLoop gameLoop = FindObjectOfType<GameLoop>().GetComponent<GameLoop>();
                        gameLoop._gameStateMachine.MoveState(GameState.EndState);
                        gameLoop.EndScreen.gameObject.SetActive(true);
                    }
                    else
                    {
                        board.Take(toPiece);
                    }
                }
                availablePosition.gameObject.SetActive(false);
            }
        }
    }
}
