using System.Collections.Generic;
using Code.Gameplay;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Code.Infrastructure
{
    public class CellMatcher : SerializedMonoBehaviour
    {
        [OdinSerialize] private Cell[,] _cells = new Cell[3, 3];
        [OdinSerialize] private BallTypes[,] _balls = new BallTypes[3, 3];

        private void OnEnable()
        {
            for (int i = 0; i < _cells.GetLength(0); i++)
            {
                for (int j = 0; j < _cells.GetLength(1); j++)
                {
                    if (_cells[i, j] != null)
                        _cells[i, j].BallHooked += OnBallHooked;
                }
            }
        }

        private void OnDisable()
        {
            for (int i = 0; i < _cells.GetLength(0); i++)
            {
                for (int j = 0; j < _cells.GetLength(1); j++)
                {
                    if (_cells[i, j] != null)
                        _cells[i, j].BallHooked -= OnBallHooked;
                }
            }
        }

        private void Start()
        {
            ActivateFirstCellRow();
        }

        private void OnBallHooked(Cell hookedCell, BallTypes ballType)
        {
            (int row, int col) = FindCellCoordinates(hookedCell);

            if (row == -1 || col == -1)
                return;

            _balls[row, col] = ballType;
            ActivateCellAbove(row, col);

            var ballsToRemove = new HashSet<Vector2Int>();
            CheckMatches(row, col, ballType, ballsToRemove);

            if (ballsToRemove.Count > 0)
            {
                ClearMatches(new List<Vector2Int>(ballsToRemove));
            }
        }

        private void ClearMatches(List<Vector2Int> matches)
        {
            foreach (var match in matches)
            {
                if (_cells[match.x, match.y] != null)
                {
                    _cells[match.x, match.y].ReleaseBall();
                    _cells[match.x, match.y].gameObject.SetActive(true);
                }

                _balls[match.x, match.y] = BallTypes.Unknown;
            }
        }

        private void CheckHorizontalMatches(int row, int col, BallTypes type, HashSet<Vector2Int> ballsToRemove)
        {
            var horizontalMatches = new List<Vector2Int>();

            for (int i = col; i >= 0; i--)
            {
                if (_balls[row, i] == type)
                    horizontalMatches.Add(new Vector2Int(row, i));
                else
                    break;
            }

            for (int i = col + 1; i < _balls.GetLength(1); i++)
            {
                if (_balls[row, i] == type)
                    horizontalMatches.Add(new Vector2Int(row, i));
                else
                    break;
            }

            if (horizontalMatches.Count >= 3)
            {
                foreach (var match in horizontalMatches)
                {
                    ballsToRemove.Add(match);
                }
            }

            Debug.Log($"Horizontal - {horizontalMatches.Count}");
        }

        private void CheckMatches(int newRow, int newCol, BallTypes newType, HashSet<Vector2Int> ballsToRemove)
        {
            CheckHorizontalMatches(newRow, newCol, newType, ballsToRemove);
            CheckVerticalMatches(newRow, newCol, newType, ballsToRemove);
            CheckDiagonalMatches(newRow, newCol, newType, ballsToRemove);
        }

        private void CheckVerticalMatches(int row, int col, BallTypes type, HashSet<Vector2Int> ballsToRemove)
        {
            var verticalMatches = new List<Vector2Int>();

            for (int i = row; i >= 0; i--)
            {
                Debug.Log($"{row} - {col} : {i} - {col}");   
                if (_balls[i, col] == type)
                {
                    verticalMatches.Add(new Vector2Int(i, col));
                }
                else
                    break;
            }

            for (int i = row + 1; i < _balls.GetLength(0); i++)
            {
                if (_balls[i, col] == type)
                    verticalMatches.Add(new Vector2Int(i, col));
                else
                    break;
            }

            if (verticalMatches.Count >= 3)
            {
                foreach (var match in verticalMatches)
                {
                    ballsToRemove.Add(match);
                }
            }
            
            Debug.Log($"Vertical - {verticalMatches.Count}");
        }

        private void CheckDiagonalMatches(int row, int col, BallTypes type, HashSet<Vector2Int> ballsToRemove)
        {
            CheckDiagonal(row, col, type, ballsToRemove, 1, 1);
            CheckDiagonal(row, col, type, ballsToRemove, 1, -1);
        }

        private void CheckDiagonal(int row, int col, BallTypes type, HashSet<Vector2Int> ballsToRemove, int rowStep,
            int colStep)
        {
            var diagonalMatches = new List<Vector2Int>();

            for (int i = 0; i >= 0; i--)
            {
                int newRow = row + i * rowStep;
                int newCol = col + i * colStep;

                if (newRow >= 0 && newRow < _balls.GetLength(0) &&
                    newCol >= 0 && newCol < _balls.GetLength(1) &&
                    _balls[newRow, newCol] == type)
                    diagonalMatches.Add(new Vector2Int(newRow, newCol));
                else
                    break;
            }

            for (int i = 1; i < _balls.GetLength(0); i++)
            {
                int newRow = row + i * rowStep;
                int newCol = col + i * colStep;

                if (newRow >= 0 && newRow < _balls.GetLength(0) &&
                    newCol >= 0 && newCol < _balls.GetLength(1) &&
                    _balls[newRow, newCol] == type)
                    diagonalMatches.Add(new Vector2Int(newRow, newCol));
                else
                    break;
            }

            if (diagonalMatches.Count >= 3)
            {
                foreach (var match in diagonalMatches)
                {
                    ballsToRemove.Add(match);
                }
            }
        }

        private (int row, int col) FindCellCoordinates(Cell targetCell)
        {
            for (int i = 0; i < _cells.GetLength(0); i++)
            {
                for (int j = 0; j < _cells.GetLength(1); j++)
                {
                    if (_cells[i, j] == targetCell)
                        return (i, j);
                }
            }

            return (-1, -1);
        }

        private void ActivateCellAbove(int currentRow, int currentCol)
        {
            int rowAbove = currentRow + 1;

            if (rowAbove >= 0 && rowAbove < _cells.GetLength(0))
            {
                Cell cellAbove = _cells[rowAbove, currentCol];

                if (cellAbove != null)
                {
                    cellAbove.gameObject.SetActive(true);
                }
            }
        }

        private void ActivateFirstCellRow()
        {
            for (int i = 0; i < _cells.GetLength(0); i++)
            {
                for (int j = 0; j < _cells.GetLength(1); j++)
                {
                    if (_cells[i, j] != null)
                        _cells[i, j].gameObject.SetActive(false);
                }
            }

            for (int j = 0; j < _cells.GetLength(1); j++)
            {
                if (_cells[0, j] != null)
                    _cells[0, j].gameObject.SetActive(true);
            }
        }
    }
}