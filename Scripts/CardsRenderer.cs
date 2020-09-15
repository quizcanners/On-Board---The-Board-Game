using System.Collections;
using System.Collections.Generic;
using PlayerAndEditorGUI;
using QuizCannersUtilities;
using UnityEngine;
using static QuizCannersUtilities.QcUtils;

#if UNITY_EDITOR
using UnityEditor;
#endif
namespace DeckRenderer
{

    [ExecuteAlways]
    public class CardsRenderer : MonoBehaviour, IPEGI
    {
        public static CardsRenderer instance;

        public ScreenShootTaker screenGrabber = new ScreenShootTaker();
        
        public List<CardsRepository> deckRepository = new List<CardsRepository>();

        void OnPostRender()
        {
            screenGrabber.OnPostRender();
        }

        void OnEnable()
        {
            instance = this;
        }

        void Update()
        {
            
        }

        #region Inspector

        private int _inspectedStuff = -1;

        private int _inspectedDeck = -1;

        public bool Inspect()
        {
            var changed = pegi.toggleDefaultInspector(this);

            if (icon.Refresh.Click())
            {
                _inspectedStuff = -1;
                _inspectedDeck = -1;
            }

            pegi.nl();

            "Screen Grabber".enter_Inspect(screenGrabber, ref _inspectedStuff, 0).nl(ref changed); 

            "Decks".enter_List_UObj(ref deckRepository,ref _inspectedDeck, ref _inspectedStuff, 1).nl(ref changed);

            return changed;
        }
        #endregion
    }


#if UNITY_EDITOR

    [CustomEditor(typeof(CardsRenderer))]
    public class CardsRendererDrawer : PEGI_Inspector_Mono<CardsRenderer> { }

#endif

}