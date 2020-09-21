using System;
using System.Collections;
using System.Collections.Generic;
using PlayerAndEditorGUI;
#if UNITY_EDITOR
    using UnityEditor;
#endif

using UnityEngine;

namespace DeckRenderer.OnBoard
{
    [CreateAssetMenu(fileName = "On Board Assets", menuName = "DeckRenderer/OnBoard/Assets")]
    public class OnBoardRendererAssets : ScriptableObject, IPEGI
    {
        public List<DiceIcon> diceIcons;
        public List<ImpostorPowerIcons> powers;

        [Serializable]
        public struct DiceIcon : IPEGI_ListInspect {
            public Sprite sprite;
            public bool InspectInList(IList list, int ind, ref int edited) =>
                ((DiceRole)ind).ToString().edit(ref sprite);
        }

        [Serializable]
        public struct ImpostorPowerIcons : IPEGI_ListInspect
        {
            public Sprite sprite;
            public bool InspectInList(IList list, int ind, ref int edited) =>
                ((ImpostorPower)ind).ToString().edit(ref sprite);
        }

        public bool Inspect()
        {
            var changes = false;

            "Dice Icons".edit_List(ref diceIcons).nl(ref changes);

            "Powers".edit_List(ref powers).nl(ref changes);

            return changes;
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(OnBoardRendererAssets))]
    public class OnBoardRendererAssetsDrawer : PEGI_Inspector_SO<OnBoardRendererAssets> { }
#endif
}