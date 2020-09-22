using QuizCannersUtilities;
using PlayerAndEditorGUI;
using UnityEngine;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace DeckRenderer
{
    [CreateAssetMenu(fileName = "Destination Deck", menuName = "DeckRenderer/OnBoard/Deck/Destinations")]
    public class DestinationsDeck : DeckGeneric<DestinationCardPrototype> {  }

#if UNITY_EDITOR
    [CustomEditor(typeof(DestinationsDeck))]
    public class DestinationDeckDrawer : PEGI_Inspector_SO<DestinationsDeck> { }
#endif

    [Serializable]
    public class DestinationCardPrototype : CardPrototypeBase
    {
        public bool isSafeToLand;

        public override void Decode(string key, CfgData token)
        {
            switch (key)
            {
                case "IsSafe": isSafeToLand = token.ToBool("Yes"); break;
                default: base.Decode(key, token); break;
            }
        }

    }
}