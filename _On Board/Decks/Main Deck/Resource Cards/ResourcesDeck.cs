using QuizCannersUtilities;
using PlayerAndEditorGUI;
using UnityEngine;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace DeckRenderer
{
    [CreateAssetMenu(fileName = "Resource Deck", menuName = "DeckRenderer/OnBoard/Deck/Resources")]
    public class ResourcesDeck : DeckGeneric<ResourceCardPrototype> {  }

#if UNITY_EDITOR
    [CustomEditor(typeof(ResourcesDeck))]
    public class ResourcesDeckDrawer : PEGI_Inspector_SO<ResourcesDeck> { }
#endif

    [Serializable]
    public class ResourceCardPrototype : CardPrototypeBase
    {
       
        public string sabotague;
        public string sabotagueType;

        #region Decoding
        public override void Decode(string key, CfgData token)
        {
            switch (key)
            {
                case "Sabotague": sabotague = token.ToString(); break;
                case "Sabotague Type": sabotagueType = token.ToString(); break; 
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