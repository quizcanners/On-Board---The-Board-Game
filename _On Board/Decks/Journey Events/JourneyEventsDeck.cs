using QuizCannersUtilities;
using PlayerAndEditorGUI;
using UnityEngine;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace DeckRenderer
{
    [CreateAssetMenu(fileName = "Journey Events Deck", menuName = "DeckRenderer/OnBoard/Deck/Journey Events")]
    public class JourneyEventsDeck : DeckGeneric<JourneyEventPrototype>
    {
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(JourneyEventsDeck))]
    public class JourneyEventsDeckDrawer : PEGI_Inspector_SO<JourneyEventsDeck>
    {
    }
#endif

    [Serializable]
    public class JourneyEventPrototype : CardPrototypeBase
    {
        public string penalty;

        public override void Decode(string key, CfgData token)
        {
            switch (key)
            {
                case "Penalty": penalty = token.ToString(); break;
                default: base.Decode(key, token); break;
            }
        }

    }
}