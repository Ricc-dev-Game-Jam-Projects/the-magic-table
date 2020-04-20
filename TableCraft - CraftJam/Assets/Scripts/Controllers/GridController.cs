
public class GridController
{
    public Grid grid { get; private set; }

    public void Create(int Size)
    {
        grid = new Grid(Size);
    }

    public void Update(int Size)
    {
        Grid newGrid = new Grid(Size);
        for(int i = 0; i < grid.Size; i++)
        {
            for(int j = 0; j < grid.Size; j++)
            {
                newGrid.Items[i, j] = grid.Items[i, j];
            }
        }
        grid = newGrid;
    }
}

