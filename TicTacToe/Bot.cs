using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace TicTacToe
{
    class Bot:Game
    {
        protected string PlayerName { get; set; }
        protected int Difficulty { get; set; }
        protected int WonScenarios;
        protected int SimulatedScenarios;
        public Bot(string playerName, int difficulty)
        {
            this.PlayerName = playerName;
            this.Difficulty = difficulty;
            this.WonScenarios = 0;
            this.SimulatedScenarios = 0;
        }
        public int GetMove()
        {
            switch (Difficulty)
            {
                case 0:
                    return EasyDifficulty();
                case 1:
                    return MediumDifficulty();
                case 2:
                    return HardDifficulty();
                case 3:
                    return VeryHardDifficulty();
                case 4:
                    return ImpossibleDifficulty();
                default:
                    throw new Exception("Wrong difficulty");
            }
        }

        private int EasyDifficulty() // Random move , no logic behind it :)
        {
            Random rnd = new Random();
            return rnd.Next(1, 10);
        }

        private int MediumDifficulty() // Statistically mediocre move - assign values to the board, choose the value closest to the average unless - next turn loss / win possible 
        {
            int move = 0;
            


            return move;
        }
        private int HardDifficulty() //Statistically best move - assign values to the board, choose the highest value unless - next turn loss / win possible 
        {
            int move = 0;



            return move;
        }

        private int VeryHardDifficulty() //Analitically best move - simulate all possible scenarios - choose most defensive move
        {
            int move = 0;



            return move;
        }

        private int ImpossibleDifficulty() //Analitically best move - simulate all possible scenarios - best probability of winning
        {
            return BestMoveFromArray(CalculateWinChance());
        }
        private int BestMoveFromArray(int[] TestArray) // Choose highest value move from array
        {
            int MoveValue = -1000000000;
            int Move = 0;
            for (int i = 0; i < TestArray.Length; i++)
            {
                if (TestArray[i] > MoveValue)
                {
                    Move = i+1;
                }
            }
            return Move;
        }

        private int[] CalculateWinChance()
        {
            int[] WinsBoard = new int[] {0,0,0,
                                         0,0,0,
                                         0,0,0}; // [i] where i+1 == move on the board. Value == Amount of won scenarios 


            bool[] MoveToCheck = new bool[9]; // all available moves - index+1 == move
            
            int cnt = 0;

            foreach (string position in this.board) //check for possible moves
            {
                if (!position.Equals("O") && !position.Equals("X"))
                {
                    MoveToCheck[cnt] = true;
                }
                cnt++;
            }

            for (int i = 0; i < 9; i++)
            {
                string[,] CurrentBoardCopy = this.board;
                this.WonScenarios = 0;
                this.SimulatedScenarios = 0; //reset values
                if (MoveToCheck[i]) // if move's possible - check winnable scenarios
                {
                    int Column = (int)Math.Floor((decimal)i / 3);
                    Math.DivRem(i, 3, out int Row);
                    CurrentBoardCopy[Column, Row] = this.PlayerName;
                    GetWinChance(CurrentBoardCopy, this.turn + 1, this.PlayerName == "O" ? "X" : "O"); //start recurrence with already simulated single move

                }
                WinsBoard[i] = this.WonScenarios; // assign amount of wins to the simulated move
            }

            return WinsBoard;
        }
        private void GetWinChance( string[,] SimBoard, int CurrentTurn, string CurrentPlayer)
        {
            bool[] MoveToCheck = new bool[9]; // all available moves - index+1 == move
            string[,] SimBoardCopy = SimBoard; // Needed for recurrence (?)
            int cnt = 0;
            int PossibleMovesCount = 0;

            bool GameState = checkState(SimBoard);
            if (IsDraw(SimBoard))
            {
                WonScenarios+= 0;
            }
            if (GameState && PlayerName != CurrentPlayer)
            {
                WonScenarios += 1;
            }
            else if(GameState && PlayerName == CurrentPlayer)
            {
                WonScenarios += -1;
            }

            foreach (string position in  SimBoard)
            {
                if(!position.Equals("O") && !position.Equals("X"))
                {
                    MoveToCheck[cnt] = true;
                    PossibleMovesCount++;
                }
                cnt++;
            }

            

            for(int i = 0; i< 9; i++)
            {
                if (MoveToCheck[i])
                {
                    int Column = (int)Math.Floor((decimal)i / 3);
                    Math.DivRem(i ,3,out int Row);
                    SimBoardCopy[Column,Row] = CurrentPlayer;
                    SimulatedScenarios++;
                    GetWinChance(SimBoardCopy, CurrentTurn + 1, CurrentPlayer == "O" ? "X" : "O");

                }
            }

        }
    }
}
