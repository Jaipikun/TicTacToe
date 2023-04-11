using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class Game
    {
        protected string[,] board;
        private string player;
        private Bot bot = new Bot("O", 0);
        protected int turn = 0;
        private void changePlayer()
        {
            if (player == "O")
            {
                player = "X";
            }
            else 
            {
                player = "O";
            }
        }
        private void showBoard()
        {
            Console.Clear();
            for (int i = 0; i < board.GetLength(0); i++)
            {
                Console.Write("|  ");
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    Console.Write(board[i, j]+"  |  ");
                }
                Console.Write("\n\n");
            }
        }
        private bool isMoveValid(string move)
        {
            int moveInt;
            bool isValid = int.TryParse(move,out moveInt);
            if (isValid)
            {
                if(moveInt>0 && moveInt < 10)
                {
                    for (int i = 0; i < board.GetLength(0); i++)
                    {
                        for(int j = 0;j< board.GetLength(1); j++)
                        {
                            if (board[i, j] == move)
                            {
                                return true;
                            }
                        }
                        

                    }
                }
            }
            return false;
        }
        private void makeMove(string move)
        {
            if (isMoveValid(move))
            {
                for (int i = 0; i < board.GetLength(0); i++)
                {
                    for (int j = 0; j < board.GetLength(1); j++)
                    {
                        if (board[i, j] == move)
                        {
                            board[i,j] = player;
                            this.turn += 1;
                            changePlayer();
                        }
                    }


                }
            }
        }
        private void resetBoard()
        {
            player = "X";
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    board[i, j] = (board.GetLength(0) * i + j + 1).ToString();
                }
            }
        }
        private bool checkState()
        {
            changePlayer();
            bool state = false;
            int HorCnt;
            int VerCnt;
            for (int i = 0; i < board.GetLength(0); i++)
            {
                VerCnt = 0;
                HorCnt = 0;
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] == this.player)
                    {
                        VerCnt++;
                    }
                    else
                    {
                        VerCnt--;
                    }

                    if (board[j,i] == this.player)
                    {
                        HorCnt++;
                    }
                    else
                    {
                        HorCnt--;
                    }
                }
                if (VerCnt == 3)
                {
                    
                    state = true;
                }
                if (HorCnt == 3)
                {
                    state = true;
                }
            }

            if (board[0,0] == board[1,1] && board[1,1] == board[2, 2])
            {
                state = true;
            }
            if (board[2, 0] == board[1, 1] && board[1, 1] == board[0, 2])
            {
                state = true;
            }
            changePlayer();
            return state;
        }
        private void WinMSG()
        {
            Console.Clear();
            showBoard();
            changePlayer();
            if (IsDraw())
            {
                Console.WriteLine("It's a draw :(");
            }
            else
            {
                Console.WriteLine("The winner is player {0}", this.player);
            }

        }
        

        private bool IsDraw()
        {
            int cnt = 0;
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (isMoveValid(board[i, j]))
                    {
                        cnt++;
                    }
                }
            }
            if (cnt == 0)
            {
                return true;
            }
            return false;
        }

        public Game()
        {
            player = "X";
            board = new string[3, 3];
            for(int i = 0;i<board.GetLength(0);i++)
            {
                for(int j = 0;j < board.GetLength(1); j++)
                {
                    board[i, j] = (board.GetLength(0) * i + j + 1).ToString();
                }
            }
        }
        public void play()
        {
            bool ValidMove = true;
            string move = "";
            bool IsBot = false;
            while (!checkState())
            {
                if(this.player == "O")
                {
                    IsBot = true;
                }
                if (IsDraw())
                {
                    break;
                }
                showBoard();
                Console.WriteLine("Turn of player : {0}", player);
                if (ValidMove && !IsBot)
                {
                    
                    Console.WriteLine("Input your move.");
                    move = Console.ReadLine();
                }
                else
                {
                    move = bot.GetMove().ToString();
                    while (!isMoveValid(move))
                    {
                        move = bot.GetMove().ToString();
                    }
                    IsBot = false;
                }

                if (isMoveValid(move))
                {
                    makeMove(move);
                    ValidMove = true;
                }
                else
                {
                    ValidMove = false;
                    
                    Console.WriteLine("Invalid move. Input your move again.");
                    move = Console.ReadLine();
                }
                
            }
            WinMSG();
            
        }
    }
}
