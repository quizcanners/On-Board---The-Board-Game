using System.Collections.Generic;
using System.Text;
using PlayerAndEditorGUI;
using QuizCannersUtilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using UnityEditor;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.Rendering;

namespace DeckRenderer.DnD
{
    [ExecuteAlways]
    public class DnDSpellCardDesign : CardDesignGeneric<DnDSpellPrototype>
    {

        public TextMeshProUGUI school;
        public TextMeshProUGUI castingTime;
        public TextMeshProUGUI range;
        public Graphic verbal, somatic, material;
        public TextMeshProUGUI component;
        public TextMeshProUGUI duration;
        public RectTransform concentration;
        public RectTransform ritual;



        public RectTransform damageContent;
        public TextMeshProUGUI damageInfo;

        public RectTransform spellScalingContent;
        public TextMeshProUGUI spellScaling;

        public GameObject artContent;
        public Material artMaterial;

        public List<RectTransform> subclassContent;
        public TextMeshProUGUI subclass;


        public Image Bard, Cleric, Druid, Paladin, Ranger, Sorceror, Warlock, Wizard;

        public override void Fill(DnDSpellPrototype spell)
        {
            if (spell != null)
            {
                StopAllCoroutines();

                TrySet(castingTime, spell.castingTime);
                TrySet(range, spell.range);

                TrySetEnabled(verbal, spell.verbal);
                TrySetEnabled(somatic, spell.somatic);
                TrySetEnabled(material, spell.material);
                TrySet(component, spell.components + spell.Cost);

                TrySet(duration, spell.duration);

                TrySetActive(concentration, spell.concentration);
                TrySetActive(ritual, spell.ritual);

                TrySet(Name, spell.NameForPEGI);

                StringBuilder lvl = new StringBuilder(64);

                if (spell.level == 0)
                {
                    lvl.Append(spell.school.ToString()).Append(" Cantrip");
                } else
                {
                    switch (spell.level)
                    {
                        case 1: lvl.Append("1st"); break;
                        case 2: lvl.Append("2nd"); break;
                        case 3: lvl.Append("3rd"); break;
                        default: lvl.Append(spell.level.ToString()+"th"); break;
                    }

                    lvl.Append("-level ").Append(spell.school.ToString());
                }

                TrySet(school, lvl.ToString());
                
                TrySet(Description, spell.description);

                bool showRollsInfo = !spell.attackRoll.IsNullOrEmpty() || !spell.savingThrow.IsNullOrEmpty() || spell.damageType != DamageType.None;

                TrySetActive(damageContent, showRollsInfo);

                if (showRollsInfo) {
                    StringBuilder diceInfo = new StringBuilder(128);

                    diceInfo.Append(spell.savingThrow);
                    if (!spell.savingEffect.IsNullOrEmpty())
                    {
                        diceInfo.Append(" ({0}) ".F(spell.savingEffect));
                    }

                    diceInfo.Append(spell.attackRoll);

                    if (spell.damageType != DamageType.None)
                    {
                        if (spell.savingThrow.IsNullOrEmpty() == false)
                            diceInfo.Append(" | ");

                        if (spell.damageType == DamageType.Custom)
                        {
                            diceInfo.Append(spell.customDamageType);
                        }
                        else
                        {
                            //TrySet(damageIcon, );
                            diceInfo.Append("<sprite name=\"{0}\">".F(spell.damageType.ToString()));
                        }
                        diceInfo.Append(" ").Append(spell.dice).Append(" ");
                    }
                    TrySet(damageInfo, diceInfo.ToString());
                 }
                 
                TrySetActive(spellScalingContent, spell.perHigherSpellLevel.IsNullOrEmpty() == false);
                TrySet(spellScaling, spell.perHigherSpellLevel);


                TrySetActive(artContent, !spell.art.IsNullOrEmpty());
                if (spell.art.IsNullOrEmpty() == false)
                {
                    StartCoroutine(SetTexture(spell.art));
                }

                string specialClass = spell.GetSpecialClass();

                subclassContent.SetActive_List(specialClass.IsNullOrEmpty() == false);
                TrySet(subclass, specialClass);

                TrySetEnabled(Bard, !spell.Bard.IsNullOrEmpty());
                TrySetEnabled(Cleric, !spell.Cleric.IsNullOrEmpty());
                TrySetEnabled(Druid, !spell.Druid.IsNullOrEmpty());
                TrySetEnabled(Paladin, !spell.Paladin.IsNullOrEmpty());
                TrySetEnabled(Ranger, !spell.Ranger.IsNullOrEmpty());
                TrySetEnabled(Sorceror, !spell.Sorceror.IsNullOrEmpty());
                TrySetEnabled(Warlock, !spell.Warlock.IsNullOrEmpty());
                TrySetEnabled(Wizard, !spell.Wizard.IsNullOrEmpty());

            }
            else
            {
                TrySet(Name, "No Prototype");
                TrySet(Description, "No Prototype");
            }
        }

        public IEnumerator SetTexture(string address)
        {
            Debug.Log("Downloading {0}".F(address));

            UnityWebRequest www = UnityWebRequestTexture.GetTexture(address);
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Texture2D wwwTexture = DownloadHandlerTexture.GetContent(www);

               var mipTexture = new Texture2D(wwwTexture.width, wwwTexture.height, wwwTexture.format, mipChain: true);

                //loadedTexture.LoadRawTextureData(www.downloadHandler.data); //wwwTex.GetRawTextureData<byte>());

                //loadedTexture.Apply(true);

                if (SystemInfo.copyTextureSupport != CopyTextureSupport.None)
                {
                    Graphics.CopyTexture(wwwTexture, 0, 0, mipTexture, 0, 0); // copies mip 0
                }
                else
                {
                    mipTexture.LoadImage(www.downloadHandler.data);
                }

                mipTexture.Apply(true); // generates all other mips from mip 0


                if (mipTexture)
                {
                    artMaterial.SetTexture("_FillTex", mipTexture);
                }
                else
                {
                    artContent.SetActive(false);
                }
            }

            yield break;

        }

        public override bool Inspect()
        {
            var changed = pegi.toggleDefaultInspector(this); 
                
            base.Inspect().nl(ref changed);



            return changed;
        }

    }


    [CustomEditor(typeof(DnDSpellCardDesign))]
    public class DnDSpellCardDesignDrawer : PEGI_Inspector_Mono<DnDSpellCardDesign>
    {    }
}
