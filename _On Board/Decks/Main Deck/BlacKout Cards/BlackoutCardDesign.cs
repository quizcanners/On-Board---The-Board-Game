using System;
using System.Collections;
using System.Collections.Generic;
using PlayerAndEditorGUI;
using TMPro;
using UnityEngine;

namespace DeckRenderer.OnBoard
{
    public class BlackoutCardDesign : CardDesignBase
    {
        [Header("Impostor section")]
        public TextMeshProUGUI SabotagueTypeRequirement;
        public TextMeshProUGUI SabotagueResult;
        public TextMeshProUGUI SabotagueDescription;
    }
}
