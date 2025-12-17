using System;
using UnityEngine;
using Grid = Snake.Entity.Grid;
using Random = UnityEngine.Random;

namespace Snake.Service
{
    public sealed class GridService : Service
    {
        private readonly byte _height = 21;
        private readonly byte _width = 37;
        
        private readonly Grid _grid = Grid.Instance;

        public static GridService Instance { get; private set; }

        private void Awake()
        {
            Initialize();
        }

        protected override void Initialize()
        {
            if (Instance == null)
            {
                Instance = this;
                _grid.Initialize(_height, _width);
                DontDestroyOnLoad(Instance);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public Vector2Int GetRandomFreeCell()
        {
            Vector2Int[] freeCells = GetFreeCells();
            Vector2Int randomFreeCell = freeCells[Random.Range(0, freeCells.Length)];
            return randomFreeCell;
        }

        private Vector2Int[] GetFreeCells()
        {
            Vector2Int[] freeCells = {};
        
            for (int i = 0; i < _grid.Cells.GetLength(0); i++)
            {
                for (int j = 0; j < _grid.Cells.GetLength(1); j++)
                {
                    Vector2Int cell = new Vector2Int(i, j);
                    if (_grid.IsLiberatedCell(cell))
                    {
                        Array.Resize(ref freeCells, freeCells.Length + 1);
                        freeCells[^1] = cell;
                    }
                }
            }
            return freeCells;
        }
    }
}