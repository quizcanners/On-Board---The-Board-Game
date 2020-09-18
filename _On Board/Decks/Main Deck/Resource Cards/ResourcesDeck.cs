using QuizCannersUtilities;
using PlayerAndEditorGUI;
using UnityEngine;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace DeckRenderer
{
    [CreateAssetMenu(fileName = "Resource Deck", menuName = "DeckRenderer/Deck")]
    public class ResourcesDeck : DeckGeneric<ResourceCardPrototype> {  }

#if UNITY_EDITOR
    [CustomEditor(typeof(ResourcesDeck))]
    public class ResourcesDeckDrawer : PEGI_Inspector_SO<ResourcesDeck> { }
#endif

    [Serializable]
    public class ResourceCardPrototype : CardPrototypeBase
    {
       
        public string type;
        public string sabotague;

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