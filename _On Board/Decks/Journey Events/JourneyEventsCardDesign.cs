using System;
using System.Collections;
using System.Collections.Generic;
using PlayerAndEditorGUI;
using TMPro;
using UnityEngine;

namespace DeckRenderer.OnBoard
{
    public class JourneyEventsCardDesign : CardDesignGeneric<JourneyEventPrototype>
    {
        public TextMeshProUGUI penalty;

        public override void Fill(JourneyEventPrototype prot)
        {
            penalty.text = prot.penalty;

        }
    }
}
