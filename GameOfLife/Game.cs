using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Controls;

namespace GameOfLife
{
    public class Game
    {
        private List<List<Cell>> cells = new List<List<Cell>>();

        public int Rows
        {
            get { return cells.Count; }
        }

        public int Columns
        {
            get { return cells[0].Count; }
        }

        public void NextGeneration()
        {
            for (int y = 0; y < cells.Count; ++y)
            {
                for (int x = 0; x < cells[0].Count; ++x)
                {
                    // Any dead cell with three live neighbours becomes a live cell.
                    if (cells[y][x].IsAlive() == false && cells[y][x].neighbours == 3)
                        cells[y][x].Switch();

                    // Any live cell with two or three live neighbours survives.
                    else if (cells[y][x].IsAlive() == true && cells[y][x].neighbours != 2 && cells[y][x].neighbours != 3)
                        cells[y][x].Switch();
                }
            }
        }

        private int CountNeighbours(Cell cell)
        {
            int neighbours = 0;

            for (int y_off = -1; y_off <= 1; ++y_off)
            {
                if (cell.y + y_off < 0 || cell.y + y_off >= cells.Count)
                    continue;

                for (int x_off = -1; x_off <= 1; ++x_off)
                {
                    if ((y_off == 0 && x_off == 0) || cell.x + x_off < 0 || cell.x + x_off >= cells[0].Count)
                        continue;

                    if (cells[cell.y + y_off][cell.x + x_off].IsAlive())
                        ++neighbours;
                }
            }

            return neighbours;
        }

        public void UpdateNeighbourCounts()
        {
            for (int y = 0; y < cells.Count; ++y)
            {
                for (int x = 0; x < cells[0].Count; ++x)
                {
                    Cell cell = cells[y][x];
                    int neighbours = CountNeighbours(cell);
                    cell.neighbours = neighbours;
                }
            }
        }

        public void SetCellSize(double size, double padding)
        {
            for (int y = 0; y < cells.Count; ++y)
                for (int x = 0; x < cells[0].Count; ++x)
                    cells[y][x].GetUIObject().SetSize(size, padding);
        }

        public void UpdateUI(Canvas canvas)
        {
            canvas.Children.Clear();

            for (int y = 0; y < cells.Count; ++y)
            {
                for (int x = 0; x < cells[0].Count; ++x)
                {
                    canvas.Children.Add(cells[y][x].GetUIObject().GetShape());
                }
            }
        }

        public void LoadFile(string file)
        {
            TextReader tr = null;
            try {
                tr = new StreamReader(Directory.GetCurrentDirectory() + @"\" + file);
            } catch (Exception) {
                tr = new StreamReader(Config.GetPath() + @"\Patterns\" + file);
            }

            List<string> fileCopy = new List<string>();
            string line = "";

            while ((line = tr.ReadLine()) != null)
                fileCopy.Add(line);

            for (int y = 0; y < fileCopy.Count; ++y)
            {
                cells.Add(new List<Cell>());

                for (int x = 0; x < fileCopy[0].Length; ++x)
                {
                    cells[y].Add(new Cell(x, y));

                    if (fileCopy[y][x] == '1')
                        cells[y][x].Switch();
                }
            }
        }
    }
}
