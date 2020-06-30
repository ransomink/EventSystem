using System;
using UnityEngine;

[Serializable]
public class Consumable
{
    public enum EffectType
    {
        NONE, NAUSEA, PAINKILLER
    }

    public string Name         { get; set; }
    public string Desc         { get; set; }
    public Sprite Icon         { get; set; }
    public int HpGain          { get; set; }
    public int SanityGain      { get; set; }
    public int HydrationGain   { get; set; }
    public int NourishmentGain { get; set; }
    public EffectType EffectA  { get; set; }
    public EffectType EffectB  { get; set; }

    public Consumable( string name, string desc, Sprite icon, int hpGain, int sanityGain, int nourishmentGain, int hydrationGain, EffectType effectA, EffectType effectB )
    {
        Name            = name;
        Desc            = desc;
        Icon            = icon;
        HpGain          = hpGain;
        SanityGain      = sanityGain;
        HydrationGain   = hydrationGain;
        NourishmentGain = nourishmentGain;
        EffectA         = effectA;
        EffectB         = effectB;
    }
}
