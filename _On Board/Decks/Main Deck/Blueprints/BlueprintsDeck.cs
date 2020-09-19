using QuizCannersUtilities;
using PlayerAndEditorGUI;
using UnityEngine;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace DeckRenderer
{
    [CreateAssetMenu(fileName = "Blueprints Deck", menuName = "DeckRenderer/Blueprints Deck")]
    public class BlueprintsDeck : DeckGeneric<BlueprintPrototype> {  }

#if UNITY_EDITOR
    [CustomEditor(typeof(BlueprintsDeck))]
    public class BlueprintsDeckDrawer : PEGI_Inspector_SO<BlueprintsDeck> { }
#endif

    [Serializable]
    public class BlueprintPrototype : CardPrototypeBase
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