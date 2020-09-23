using QuizCannersUtilities;
using PlayerAndEditorGUI;
using UnityEngine;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace DeckRenderer.OnBoard
{
    [CreateAssetMenu(fileName = "Blackout Deck", menuName = "DeckRenderer/OnBoard/Deck/Blackout")]
    public class BlackoutDeck : DeckGeneric<BlackoutPrototype> {  }

#if UNITY_EDITOR
    [CustomEditor(typeof(BlackoutDeck))]
    public class BlackoutDeckDrawer : PEGI_Inspector_SO<BlackoutDeck> { }
#endif

    [Serializable]
    public class BlackoutPrototype : CardPrototypeBase
    {

        public ImpostorPower power;
        public string sabotague;

        #region Decoding
        public override void Decode(string key, CfgData token)
        {
            switch (key)
            {
                case "Sabotague Type": power = OnBoardUtils.FearTypeFrom(token.ToString()); break;
                case "Sabotague": sabotague = token.ToString(); break;
                default: base.Decode(key, token); break;
            }
        }

        #endregion
        
        public override bool Inspect()
        {
            var changed = base.Inspect();

            "Description".editBig(ref description);

            return changed;
        }

    }

}