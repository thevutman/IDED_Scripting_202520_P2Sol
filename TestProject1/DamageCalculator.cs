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
        public static int Calculate(Pokemon attacker, Pokemon defender, Move move, float mod)
        {
            float finalDamage;
            float levelComponent = (2f * (attacker.Level / 5f)) + 2f;

            if (move.moveType == MoveType.Physical)
            {
                float attackDefenseComponent = (float)move.BasePower * ((float)attacker.Attack / defender.Defense) + 2f;
                finalDamage = ((levelComponent * attackDefenseComponent) / 50f);
            }
            else // Special
            {
                float specialComponent = (float)move.BasePower * ((float)attacker.SpAttack / defender.SpDefense) + 2f;
                finalDamage = ((levelComponent * specialComponent) / 50f);
            }

            finalDamage *= mod;

            // Redondear el resultado
            return (int)Math.Round(finalDamage);
        }
    }
}
