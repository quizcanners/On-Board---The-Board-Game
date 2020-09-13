using System.Collections;
using System.Collections.Generic;
using PlayerAndEditorGUI;
using UnityEngine;
using static QuizCannersUtilities.QcUtils;

#if UNITY_EDITOR
using UnityEditor;
#endif
namespace NodeNotes.BoardGame
{

    [ExecuteAlways]
    public class CardsRenderer : MonoBehaviour, IPEGI
    {

        public ScreenShootTaker screenGrabber = new ScreenShootTaker();

        public bool Inspect()
        {
            var changed = false;

            screenGrabber.Nested_Inspect();

            return changed;
        }

        void OnPostRender() => screenGrabber.OnPostRender();

        void Update()
        {

        }
    }


#if UNITY_EDITOR

    [CustomEditor(typeof(CardsRenderer))]
    public class CardsRendererDrawer : PEGI_Inspector_Mono<CardsRenderer> { }

#endif

}