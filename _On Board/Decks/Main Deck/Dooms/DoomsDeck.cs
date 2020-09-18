using QuizCannersUtilities;
using PlayerAndEditorGUI;
using UnityEngine;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace DeckRenderer
{
    [CreateAssetMenu(fileName = "Dooms Deck", menuName = "DeckRenderer/Dooms Deck")]
    public class DoomsDeck : DeckGeneric<DoomsPrototype> {  }

#if UNITY_EDITOR
    [CustomEditor(typeof(DoomsDeck))]
    public class DoomsDeckDrawer : PEGI_Inspector_SO<DoomsDeck> { }
#endif

    [Serializable]
    public class DoomsPrototype : CardPrototypeBase
    {

        [Header("Abilities")]
        public bool useTools;
        public bool infection;
        public bool misfortune;
        public bool ocult;
        public bool madness;

        #region Decoding
        public override void Decode(string key, CfgData token)
        {
            switch (key)
            {
                case "Use Tools": useTools = token.ToBool("Yes"); break;
                case "Infection": infection = token.ToBool("Yes"); break;
                case "Misfortune": misfortune = token.ToBool("Yes"); break;
                case "Ocult": ocult = token.ToBool("Yes"); break;
                case "Madness": madness = token.ToBool("Yes"); break;
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