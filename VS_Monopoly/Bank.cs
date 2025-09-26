using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VS_Monopoly
{
    internal static class Bank
    {
        public static void Pay(int amount, Player player)
        {
            if (amount > 0)
            {
                if (player.balance > amount)
                {
                    player.balance -= amount;
                }
                else
                {
                    throw new ArithmeticException("Not enough money!");
                }
            }
        }

    }
}
