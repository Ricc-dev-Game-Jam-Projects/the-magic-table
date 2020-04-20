
public class Grid
{
    public Item[,] Items { get; private set; }

    public int Size { 
        get {
            return Items.GetLength(0);
        }
    }
    public Grid(int Size)
    {
        Items = new Item[Size, Size];
    }

    public Item GetItem(int row, int column)
    {
        if(row >= Items.Length)
        {
            return null;
        }
        if(column >= Items.Length)
        {
            return null;
        }

        return Items[row, column];
    }
}
