using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime;
using System.Numerics;

namespace TicTacToe
{
    class Bot:Game
    {
        private string PlayerName { get; set; }
        private int Difficulty { get; set; }
        public Bot(string playerName,int difficulty)
        {
            this.PlayerName = playerName;
            this.Difficulty = difficulty;
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
            int move = 0;



            return move;
        }

        private float CalculateWinChance()
        {
            return 0;
        }
        private float GetWinChance(string[,] SimBoard,int CurrentTurn)
        {
            bool[] MoveToCheck = new bool[9]; // all available moves - index+1 == move
            int cnt = 0;
            foreach (string position in  SimBoard)
            {
                if(position.Equals("O") || position.Equals("X"))
                {
                    MoveToCheck[cnt] = true;
                }
                cnt++;
            }
            //TODO - recurrence maybe??
        }

        private bool isMoveValid(string move, string[,] TestBoard) // Unsure whether its needed
        {
            int moveInt;
            bool isValid = int.TryParse(move, out moveInt);
            if (isValid)
            {
                if (moveInt > 0 && moveInt < 10)
                {
                    for (int i = 0; i < TestBoard.GetLength(0); i++)
                    {
                        for (int j = 0; j < TestBoard.GetLength(1); j++)
                        {
                            if (TestBoard[i, j] == move)
                            {
                                return true;
                            }
                        }


                    }
                }
            }
            return false;
        }
        private void makeMove(string move,string player, string[,] TestBoard) // Unsure whether its needed
        {
            if (isMoveValid(move,TestBoard))
            {
                for (int i = 0; i < TestBoard.GetLength(0); i++)
                {
                    for (int j = 0; j < TestBoard.GetLength(1); j++)
                    {
                        if (TestBoard[i, j] == move)
                        {
                            TestBoard[i, j] = player;
                        }
                    }


                }
            }
        }
    }
}
