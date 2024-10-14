using System;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tetris
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ImageSource[] tileImages = new ImageSource[]
        {
            new BitmapImage(new Uri("Assets/TetrisAssets/TileEmpty.png", UriKind. Relative)),
            new BitmapImage(new Uri("Assets/TetrisAssets/TileCyan.png", UriKind. Relative)),
            new BitmapImage(new Uri("Assets/TetrisAssets/TileBlue.png", UriKind. Relative)),
            new BitmapImage(new Uri("Assets/TetrisAssets/TileOrange.png", UriKind. Relative)),
            new BitmapImage(new Uri("Assets/TetrisAssets/TileYellow.png", UriKind. Relative)),
            new BitmapImage(new Uri("Assets/TetrisAssets/TileGreen.png", UriKind. Relative)),
            new BitmapImage(new Uri("Assets/TetrisAssets/TilePurple.png", UriKind. Relative)),
            new BitmapImage(new Uri("Assets/TetrisAssets/TileRed.png", UriKind. Relative))
        };

        private readonly ImageSource[] blockImages = new ImageSource[]
        {
            new BitmapImage(new Uri("Assets/TetrisAssets/Block-Empty.png", UriKind. Relative)),
            new BitmapImage(new Uri("Assets/TetrisAssets/Block-I.png", UriKind. Relative)),
            new BitmapImage(new Uri("Assets/TetrisAssets/Block-J.png", UriKind. Relative)),
            new BitmapImage(new Uri("Assets/TetrisAssets/Block-L.png", UriKind. Relative)),
            new BitmapImage(new Uri("Assets/TetrisAssets/Block-O.png", UriKind. Relative)),
            new BitmapImage(new Uri("Assets/TetrisAssets/Block-S.png", UriKind. Relative)),
            new BitmapImage(new Uri("Assets/TetrisAssets/Block-T.png", UriKind. Relative)),
            new BitmapImage(new Uri("Assets/TetrisAssets/Block-Z.png", UriKind. Relative))
        };
        private readonly Image[,] imageControls;
        private readonly int maxDelay = 1000;
        private readonly int minDelay = 100;
        private readonly int delayDecrase = 150;
        private GameState gameState = new GameState();
        private Image[,] SetupGameCanvas(GameGrid grid)
        {
            Image[,] imageControls = new Image[grid.Rows, grid.Columns];
            int cellSize = 25;
            for (int r = 0; r < grid.Rows; r++)
            {
                for (int c = 0; c < grid.Columns; c++)
                {
                    Image imageControl = new Image
                    {
                        Width = cellSize,
                        Height = cellSize
                    };
                    Canvas.SetTop(imageControl, (r - 2) * cellSize);
                    Canvas.SetLeft(imageControl, c * cellSize);
                    GameCanvas.Children.Add(imageControl);
                    imageControls[r, c] = imageControl;
                }
            }
            return imageControls;
        }
        public MainWindow()
        {
            InitializeComponent();
            imageControls = SetupGameCanvas(gameState.GameGrid);
        }

        private void DrawGrid(GameGrid grid)
        {

            for (int r = 0; r < grid.Rows; r++)
            {
                for (int c = 0; c < grid.Columns; c++)
                {
                    int id = grid[r, c];
                    imageControls[r, c].Source = tileImages[id];
                }
            }
        }
        private void DrawBlock(Block block)
        {
            foreach(Position p in block.TilePositions())
            {
                imageControls[p.Row, p.Column].Source = tileImages[block.Id];   
            }
        }
        private void Draw(GameState gameState)
        {
            DrawGrid(gameState.GameGrid);
            DrawBlock(gameState.CurrentBlock);
            DrawNextBlock(gameState.BlockQueue);
            ScoreText.Text = $"Score: {gameState.Score}";

        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (gameState.GameOver) return;
            switch (e.Key)
            {
                case Key.Left:
                    gameState.MoveBlockLeft();
                    break;
                case Key.Right:
                    gameState.MoveBlockRight();
                    break;
                case Key.Down:
                    gameState.MoveBlockDown();
                    break;
                case Key.X:
                    gameState.BlockRotateCw();
                    break;
                case Key.Z:
                    gameState.BlockRotateCounterCw();
                    break;
                default:
                    return;
            }
            Draw(gameState);
        }
       
        private async Task TetrisLoop()
        {
            Draw(gameState);
            while (!gameState.GameOver)
            {
                int delay = Math.Max(minDelay, maxDelay - (gameState.Score * delayDecrase));
                await Task.Delay(delay);
                gameState.MoveBlockDown();
                Draw(gameState);
            }
            GameOverMenu.Visibility = Visibility.Visible;
            FinalScoreText.Text = $"Score: {gameState.Score}";
        }
        private async void GameCanvas_Loaded(object sender, RoutedEventArgs e)
        {
            await TetrisLoop();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            gameState = new GameState();
            GameOverMenu.Visibility = Visibility.Hidden;
            await TetrisLoop();
        }
        private void DrawNextBlock(BlockQueue queue)
        {
            Block next = queue.NextBlock;
            NextImage.Source = blockImages[next.Id];
        }
    }
}