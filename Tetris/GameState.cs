namespace Tetris;

public class GameState
{
    private Block current;
    public Block CurrentBlock
    {
        get => current; private set
        {
            current = value;
            current.Reset();
            
        }
    }
    public GameGrid GameGrid { get; }
    public BlockQueue BlockQueue { get; }
    public bool GameOver { get;private set; }
    public int Score { get; private set; }
    public GameState()
    {
        GameGrid = new(22, 10);
        BlockQueue = new BlockQueue();
        CurrentBlock = BlockQueue.UpdateGetBlock();
    }
    private bool BlockFits()
    {
        foreach (var p in CurrentBlock.TilePositions())
        {
            if (!GameGrid.IsEmpty(p.Row, p.Column)) return false;
        }
        return true;
    }
    public void BlockRotateCw()
    {
        CurrentBlock.RotateCw();
        if (!BlockFits()) CurrentBlock.RotateCCw();
    }
    public void BlockRotateCounterCw()
    {
        CurrentBlock.RotateCCw();
        if (!BlockFits()) CurrentBlock.RotateCw();
    }
    public void MoveBlockRight()
    {
        CurrentBlock.Move(0, 1);
        if (!BlockFits()) CurrentBlock.Move(0,-1);
    }
    public void MoveBlockLeft()
    {
        CurrentBlock.Move(0, -1);
        if (!BlockFits()) CurrentBlock.Move(0,1);
    }
    private bool IsGameOver()
    {
        return !(GameGrid.IsRowEmpty(0) && GameGrid.IsRowEmpty(1));
    }
    private void PlaceBlock()
    {
        foreach (var p in CurrentBlock.TilePositions())
        {
            GameGrid[p.Row, p.Column] = CurrentBlock.Id;
        }
        Score +=GameGrid.ClearFullRows();
        if (IsGameOver()) GameOver = true;
        else CurrentBlock = BlockQueue.UpdateGetBlock();
    }
    public void MoveBlockDown()
    {
        CurrentBlock.Move(1, 0);
        if (!BlockFits())
        {
            CurrentBlock.Move(-1, 0); 
            PlaceBlock();
        }
    }
}
