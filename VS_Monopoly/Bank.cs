using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VS_Monopoly
{
    internal static class Bank
    {
        public static void Pay(int amount, Player paying)
        {
            if (amount > 0)
            {
                if (paying.balance > amount)
                {
                    paying.balance -= amount;
                }
                else
                {
                    // handle debt
                    throw new ArithmeticException("Not enough money!");
                }
            }
        }
        public static void Pay(int amount, Player recipient, Player paying)
        {
            if (amount > 0)
            {
                if (paying.balance > amount)
                {
                    paying.balance -= amount;
                    recipient.balance += amount;
                }
                else
                {
                    // handle debt
                    throw new ArithmeticException("Not enough money!");
                }
            }
        }
        public static void Receive(int amount, Player recipient)
        {
            recipient.balance += amount;
        }
    }
}
