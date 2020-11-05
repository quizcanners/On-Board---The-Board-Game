using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PlayerAndEditorGUI;
using QuizCannersUtilities;
using UnityEngine;
using static QuizCannersUtilities.QcUtils;
using DeckRenderer.OnBoard;

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

        public OnBoardRendererAssets assets;

        public List<DeckBase> decks = new List<DeckBase>();

        public Canvas canvas;

        public CardDesignBase cardDesignInstance;
        
        void OnPostRender()
        {
            screenGrabber.OnPostRender();
        }

        void OnEnable()
        {
            instance = this;
        }

        private DeckBase _renderingDeck;

        public bool IsRendering => _renderingDeck != null;

        public void RenderAllTheCards(DeckBase deck)
        {
            StartCoroutine(RenderAllTheCardsCoro(deck));
        }

        public IEnumerator RenderSelectedDecks()
        {
            foreach (var deck in decks)
            {
                if (deck.selectedForRendering)
                    yield return RenderAllTheCardsCoro(deck);
            }
        }

        public void ShowCard(DeckBase deck, CardPrototypeBase card = null)
        {
            if (cardDesignInstance)
                cardDesignInstance.gameObject.DestroyWhatever();

            if (!deck.cardDesignPrefab)
            {
                Debug.LogError("No design prefab in " + deck.name, deck);
                return;
            }

            cardDesignInstance = Instantiate(deck.cardDesignPrefab, canvas.transform);

            if (card != null)
                cardDesignInstance.ActivePrototype = card;
        }

        private IEnumerator RenderAllTheCardsCoro(DeckBase deck)
        {
            ShowCard(deck);

            if (!cardDesignInstance)
                yield break;

            _renderingDeck = deck;

            screenGrabber.folderName = "Deck Renders/{0}".F(deck.name);

            Screen.SetResolution((int)deck.cardResolution.x, (int)deck.cardResolution.y, FullScreenMode.MaximizedWindow);

            foreach (var card in deck)
            {
                cardDesignInstance.ActivePrototype = card;
                screenGrabber.screenShotName = card.NameForPEGI;
                screenGrabber.RenderToTextureManually();
                yield return null;
            }

            _renderingDeck = null;

            yield break;
        }

        public void RenderCard(DeckBase deck, CardPrototypeBase card)
        {
            ShowCard(deck);

            if (!cardDesignInstance)
            {
                Debug.LogError("No card");
                return;
            }

            _renderingDeck = deck;

            screenGrabber.folderName = "Deck Renders/{0}".F(deck.name);

            Screen.SetResolution((int)deck.cardResolution.x, (int)deck.cardResolution.y, FullScreenMode.MaximizedWindow);

            cardDesignInstance.ActivePrototype = card;
            screenGrabber.screenShotName = card.NameForPEGI;
            screenGrabber.RenderToTextureManually();

            _renderingDeck = null;
        }

        #region Inspector

        private int _inspectedStuff = -1;
        private int _inspectedDeck = -1;

        public bool Inspect()
        {
            var changed = pegi.toggleDefaultInspector(this);

            pegi.EditorView.Lock_UnlockClick(this); 

            if (!canvas)
                "Canvas".edit(ref canvas);

            if (icon.Refresh.Click())
            {
                _inspectedStuff = -1;
                _inspectedDeck = -1;
            }

            if (IsRendering)
            {
                "Rendering...{0}".F(_renderingDeck).write();
                if (icon.Close.Click("Cancel"))
                    _renderingDeck = null;
            }

            pegi.nl();

            if (cardDesignInstance && "Clear prefab".Click().nl())
                cardDesignInstance.gameObject.DestroyWhatever();

            "Screen Grabber".enter_Inspect(screenGrabber, ref _inspectedStuff, 0).nl(ref changed); 

            "Decks".enter_List_UObj(ref decks,ref _inspectedDeck, ref _inspectedStuff, 1).nl(ref changed);

            if (_inspectedStuff == 1 && !IsRendering && decks.Any(x=> x.selectedForRendering) 
                && "Render Selected".ClickConfirm(confirmationTag: "rhdDcks", toolTip: "Render all of the selected decks?"))
                StartCoroutine(RenderSelectedDecks());
            

            return changed;
        }
        #endregion
    }
    
#if UNITY_EDITOR
    [CustomEditor(typeof(CardsRenderer))]
    public class CardsRendererDrawer : PEGI_Inspector_Mono<CardsRenderer> { }
#endif

}