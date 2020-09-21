using System;
using System.Collections;
using System.Collections.Generic;
using PlayerAndEditorGUI;
using QuizCannersUtilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DeckRenderer.OnBoard
{
    public class BlueprintsCardDesign : CardDesignBase
    {
        public List<Image> dices;

        public override void Fill()
        {
            base.Fill();

            var prot = ActivePrototype as BlueprintPrototype;

            if (prot == null)
            {
                Debug.LogError("No Blueprint Prototype");
                return;
            }

            for (int i = 0; i < dices.Count; i++)
            {
                dices[i].gameObject.SetActive(prot.resourcesNeeded > i);
                var sprite = DeckRederer.assets.diceIcons.TryGet(((int)prot.diceType)-1);
            }
        }
    }
}
