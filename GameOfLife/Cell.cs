namespace GameOfLife
{
    public class Cell
    {
        public int x = 0;
        public int y = 0;
        private bool alive = false;
        public int neighbours = 0;

        private UICell uiCell = null;

        public UICell GetUIObject()
        {
            return uiCell;
        }

        public Cell(int x, int y)
        {
            this.x = x;
            this.y = y;

            uiCell = new UICell(this);
        }

        public void Switch()
        {
            alive = !alive;

            uiCell.Update();
        }
        
        public bool IsAlive()
        {
            return alive;
        }
    }
}
