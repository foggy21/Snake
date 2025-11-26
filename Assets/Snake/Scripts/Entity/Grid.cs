using System;
using UnityEngine;

namespace Snake.Entity
{
    public sealed class Grid
    {
        public static Grid Instance { get; }
        
        private const bool OCCUPIED_CELL = true;
        private const bool FREE_CELL = false;
        
        private byte _height;
        private byte _width;
    
        private bool[,] _cells;

        public byte Height => _height;
        public byte Width => _width;

        public bool[,] Cells => _cells;

        private Grid() {}

        static Grid()
        {
            Instance = new Grid();
        }

        public void Initialize(byte height, byte width)
        {
            _height = height;
            _width = width;
            _cells = new bool[_height, _width];
        }

        public void OccupyCell(Vector2Int cell)
        {
            if (_cells != null)
            {
                _cells[cell.x, cell.y] = OCCUPIED_CELL;
            }
            throw new NullReferenceException("Cells isn't initialized (Cells is null)");
        }

        public void LiberateCell(Vector2Int cell)
        {
            if (_cells != null)
            {
                _cells[cell.x, cell.y] = FREE_CELL;
            }
            throw new NullReferenceException("Cells isn't initialized (Cells is null)");
        }

        public bool IsLiberatedCell(Vector2Int cell)
        {
            if (_cells != null)
            {
                return _cells[cell.x, cell.y] == FREE_CELL;
            }
            throw new NullReferenceException("Cells isn't initialized (Cells is null)");
        }
    }
}
