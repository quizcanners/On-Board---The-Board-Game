using QuizCannersUtilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeckRenderer.OnBoard
{
    public enum DiceRole { Unknown = 0, Civil = 1, Unoperational = 2, Engenear = 3, Ligistics = 4, Security = 5, Research = 6 }

    public enum ImpostorPower { Physical = 0, Infection = 1, Misfortune = 2, Ocult = 3, Madness = 4  }

    public static class OnBoardUtils
    {
        private static CardsRenderer DeckRenderer => CardsRenderer.instance;

        public static Sprite GetSprite(this ImpostorPower role)
        {
            var hld = DeckRenderer.assets.powers.TryGet(((int)role) - 1);
            return hld.sprite;
        }

        public static Sprite GetSprite(this DiceRole role)
        {
            var hld = DeckRenderer.assets.diceIcons.TryGet(((int)role) - 1);
            return hld.sprite;
        }

        public static ImpostorPower FearTypeFrom(string fromName)
        {
            switch (fromName)
            {
                case "Physical": return ImpostorPower.Physical;
                case "Infection": return ImpostorPower.Infection;
                case "Misfortune": return ImpostorPower.Misfortune;
                case "Ocult": return ImpostorPower.Ocult;
                case "Madness": return ImpostorPower.Madness;
                default: Debug.LogError("Couldn't decode {0}".F(fromName));
                    return ImpostorPower.Physical;
            }
        }

        public static DiceRole DiceRoleFrom(string fromName)
        {
            switch (fromName)
            {
                case "Civil": return DiceRole.Civil;
                case "Unoperational": return DiceRole.Unoperational;
                case "Engenear": return DiceRole.Engenear;
                case "Ligistics": return DiceRole.Ligistics;
                case "Security": return DiceRole.Security;
                case "Research": return DiceRole.Research;
                default: 
                    Debug.LogError("{0} not recognized".F(fromName));
                    return DiceRole.Unknown;

            }
        }
    }
}