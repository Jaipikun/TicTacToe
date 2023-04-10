using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime;
using System.Security.Cryptography;

namespace TicTacToe
{
    class Bot:Game
    {
        private string PlayerName { get; set; }
        private int difficulty { get; set; }
        public Bot(string playerName,int difficulty)
        {
            this.PlayerName = playerName;
            this.difficulty= difficulty;
        }
        public int GetMove()
        {
            return RandomNumberGenerator.GetInt32(9)+1;
        }

        
    }
}
