using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TestProject1.Move;

namespace TestProject1
{
    internal static class DamageCalculator
    {
        public static int Calculate(Pokemon attacker, Pokemon defender, Move move, double mod)
        {
            double finalDamage;
            double levelComponent = (2.0 * (attacker.Level / 5.0)) + 2.0;

            if (move.moveType == MoveType.Physical)
            {
                double attackDefenseComponent = (double)move.BasePower * ((double)attacker.Attack / defender.Defense) + 2.0;
                finalDamage = ((levelComponent * attackDefenseComponent) / 50.0);
            }
            else // Special
            {
                double specialComponent = (double)move.BasePower * ((double)attacker.SpAttack / defender.SpDefense) + 2.0;
                finalDamage = ((levelComponent * specialComponent) / 50.0);
            }

            finalDamage *= mod;

            // Redondear el resultado
            return (int)Math.Floor(finalDamage);
        }
    }
}
