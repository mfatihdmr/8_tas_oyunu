namespace _8_tas_oyunu
{
    public partial class Form1 : Form
    {

        private int size; // Tahtanýn boyutu
        private int[] goalState;
        private Button[] buttons;
        private int moveCount;
        private HashSet<string> visitedStates;


        public Form1()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            // Kullanýcýdan tahtanýn boyutunu al
            string sizeInput = Microsoft.VisualBasic.Interaction.InputBox("Lütfen tahtanýn boyutunu girin (örn: 3):", "Tahta Boyutu");
            size = int.Parse(sizeInput);

            buttons = new Button[size * size];
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i] = new Button();
                buttons[i].Enabled = false;
                buttons[i].Size = new Size(50, 50); // Butonlarýn boyutunu ayarla

                // Butonlarý formun ortasýnda yerleþtir
                int x = this.ClientSize.Width / 2 - (size * buttons[i].Width) / 2 + (i % size) * buttons[i].Width;
                int y = this.ClientSize.Height / 2 - (size * buttons[i].Height) / 2 + (i / size) * buttons[i].Height;
                buttons[i].Location = new Point(x, y);

                this.Controls.Add(buttons[i]); // Butonlarý forma ekle
            }
            StartNewGame();
        }



        private void StartNewGame()
        {
            // Kullanýcýdan hedef durumu ve baþlangýç konumlarýný al
            string goalInput = Microsoft.VisualBasic.Interaction.InputBox("Lütfen hedef durumu girin (örn: 1,2,3,4,5,6,7,8,0):", "Hedef Durum");
            goalState = goalInput.Split(',').Select(int.Parse).ToArray();

            string startInput = Microsoft.VisualBasic.Interaction.InputBox("Lütfen baþlangýç konumlarýný girin (örn: 1,2,3,4,5,6,7,8,0):", "Baþlangýç Konumlarý");
            var numbers = startInput.Split(',').Select(int.Parse).ToArray();

            UpdateGoalStateLabel();

            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].Text = numbers[i] == 0 ? "" : numbers[i].ToString();
                buttons[i].Enabled = true;
            }

            moveCount = 0;
            UpdateMoveCountLabel();

            visitedStates = new HashSet<string>();
            SolveWithAStar();
        }

        private async void SolveWithAStar()
        {
            var currentState = buttons.Select(button => string.IsNullOrEmpty(button.Text) ? 0 : int.Parse(button.Text)).ToArray();
            var emptyIndex = Array.IndexOf(currentState, 0);

            while (!currentState.SequenceEqual(goalState))
            {
                var bestMove = FindBestMove(currentState, emptyIndex);
                if (bestMove != -1)
                {
                    var newState = (int[])currentState.Clone();
                    newState[emptyIndex] = newState[bestMove];
                    newState[bestMove] = 0;

                    visitedStates.Add(string.Join(",", newState));

                    UpdateButtons(newState);
                    currentState = newState;
                    emptyIndex = bestMove;
                    moveCount++;
                    UpdateMoveCountLabel();
                    await Task.Delay(500);
                    Application.DoEvents();
                }
                else
                {
                    MessageBox.Show("Oyun çözülemedi!");
                    break;
                }
            }

            if (currentState.SequenceEqual(goalState))
            {
                MessageBox.Show("Oyun çözüldü!");
            }
        }

        private int FindBestMove(int[] state, int emptyIndex)
        {
            var possibleMoves = new List<int>();
            if (emptyIndex % size > 0) possibleMoves.Add(emptyIndex - 1);
            if (emptyIndex % size < size - 1) possibleMoves.Add(emptyIndex + 1);
            if (emptyIndex >= size) possibleMoves.Add(emptyIndex - size);
            if (emptyIndex < size * (size - 1)) possibleMoves.Add(emptyIndex + size);

            int minCost = int.MaxValue;
            int bestMove = -1;

            foreach (var move in possibleMoves)
            {
                var newState = (int[])state.Clone();
                newState[emptyIndex] = newState[move];
                newState[move] = 0;

                var stateString = string.Join(",", newState);
                if (visitedStates.Contains(stateString))
                {
                    continue;
                }

                var cost = CalculateCost(newState);

                if (cost < minCost)
                {
                    minCost = cost;
                    bestMove = move;
                }
            }

            return bestMove;
        }

        private int CalculateCost(int[] state)
        {
            int cost = 0;
            for (int i = 0; i < state.Length; i++)
            {
                if (state[i] != 0)
                {
                    int goalX = (state[i] - 1) / size;
                    int goalY = (state[i] - 1) % size;
                    int currentX = i / size;
                    int currentY = i % size;
                    cost += Math.Abs(goalX - currentX) + Math.Abs(goalY - currentY);
                }
            }
            return cost;
        }

        private void UpdateButtons(int[] state)
        {
            for (int i = 0; i < state.Length; i++)
            {
                buttons[i].Text = state[i] == 0 ? "" : state[i].ToString();
            }
        }

        private void UpdateMoveCountLabel()
        {
            moveCountLabel.Text = "Hamle Sayýsý: " + moveCount.ToString();
        }

        private void UpdateGoalStateLabel()
        {
            goalStateLabel.Text = "Hedef Durumu: " + string.Join(" ", goalState);
        }
    }
}
