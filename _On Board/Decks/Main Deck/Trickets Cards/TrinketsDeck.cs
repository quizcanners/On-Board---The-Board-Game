using QuizCannersUtilities;
using PlayerAndEditorGUI;
using UnityEngine;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace DeckRenderer
{
    [CreateAssetMenu(fileName = "Trinkets Deck", menuName = "DeckRenderer/OnBoard/Deck/Trinkets")]
    public class TrinketsDeck : DeckGeneric<TrinketsPrototype> {  }

#if UNITY_EDITOR
    [CustomEditor(typeof(TrinketsDeck))]
    public class TrinketsDeckDrawer : PEGI_Inspector_SO<TrinketsDeck> { }
#endif

    [Serializable]
    public class TrinketsPrototype : CardPrototypeBase
    {

        #region Decoding
        public override void Decode(string key, CfgData token)
        {
            switch (key)
            {
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