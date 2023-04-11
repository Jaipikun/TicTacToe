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
        private Bot bot;
        protected int turn;
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
        private bool isMoveValid(string move, string[,] TestBoard)
        {
            int moveInt;
            bool isValid = int.TryParse(move,out moveInt);
            if (isValid)
            {
                if(moveInt>0 && moveInt < 10)
                {
                    for (int i = 0; i < TestBoard.GetLength(0); i++)
                    {
                        for(int j = 0;j< TestBoard.GetLength(1); j++)
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
        private void makeMove(string move)
        {
            if (isMoveValid(move,this.board))
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
            this.turn = 0;
            player = "X";
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    board[i, j] = (board.GetLength(0) * i + j + 1).ToString();
                }
            }
        }
        protected bool checkState(string[,] TestBoard)
        {
            changePlayer();
            bool state = false;
            int HorCnt;
            int VerCnt;
            for (int i = 0; i < TestBoard.GetLength(0); i++)
            {
                VerCnt = 0;
                HorCnt = 0;
                for (int j = 0; j < TestBoard.GetLength(1); j++)
                {
                    if (TestBoard[i, j] == this.player)
                    {
                        VerCnt++;
                    }
                    else
                    {
                        VerCnt--;
                    }

                    if (TestBoard[j,i] == this.player)
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

            if (TestBoard[0,0] == TestBoard[1,1] && TestBoard[1,1] == TestBoard[2, 2])
            {
                state = true;
            }
            if (TestBoard[2, 0] == TestBoard[1, 1] && TestBoard[1, 1] == TestBoard[0, 2])
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
            if (IsDraw(this.board))
            {
                Console.WriteLine("It's a draw :(");
            }
            else
            {
                Console.WriteLine("The winner is player {0}", this.player);
            }

        }
        

        protected bool IsDraw(string[,] TestBoard)
        {
            int cnt = 0;
            for (int i = 0; i < TestBoard.GetLength(0); i++)
            {
                for (int j = 0; j < TestBoard.GetLength(1); j++)
                {
                    if (isMoveValid(TestBoard[i, j], TestBoard))
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
            this.turn = 0;
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
            this.bot = new Bot("O", 4);
            bool ValidMove = true;
            string move = "";
            bool IsBot = false;
            
            while (!checkState(this.board))
            {
                bot.board = this.board;
                bot.turn = this.turn;
                if (this.player == "O")
                {
                    IsBot = true;
                }
                if (IsDraw(this.board))
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
                    while (!isMoveValid(move, this.board))
                    {
                        move = bot.GetMove().ToString();
                    }
                    IsBot = false;
                }

                if (isMoveValid(move,this.board))
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
