using System;
using System.Collections.Generic;

namespace lo_novo.Damage
{
    public static class Attack
    {
        public static void Do(Intention i, IDamageModel mdl)
        {
            if (i.AttackType == AttackType.None)
            {
                State.o("You decide to punch.");
                i.AttackType = AttackType.Punch;
            }

            AttackType valid = mdl.GetValidAttacks();
            AttackType atav = i.AttackType & valid;
            var trait = State.Player.Traits;

            if (atav != AttackType.None)
                State.o("You " + i.VerbString + " " + i.ActiveNoun.ToString() + ".");

            if (atav == AttackType.None)
            {
                var s = new List<string>();
                foreach (var at in Enum.GetValues(typeof(AttackType)))
                    if (i.AttackType.HasFlag((AttackType)at) && (AttackType)at != AttackType.None)
                        s.Add(((AttackType)at).ToString());

                if (s.Count == 1)
                    State.o(s[0] + "ing doesn't seem appropriate here.");
                else
                    State.o("Neither " + string.Join(" nor ", s) + " seems appropriate here.");
                    
                // If high SkillWrestling, then try do manhandle
                if (trait[Traits.WeaponWrestling] >= 5 && valid.HasFlag(AttackType.Manhandle))
                    atav = AttackType.Manhandle;
                else
                {
                    // Roll die against player SkillObservant then MagicTenacity then MagicLuck

                    AttackType good = (AttackType) Enum.Parse(typeof(AttackType), valid.ToString());

                    var crit = false;

                    var obsRoll = State.d20(Traits.SkillObservant, 5);
                    if (obsRoll >= 5)
                    {
                        State.o("You decide instead to " + good.ToString() + ".");
                        atav = good;
                    }
                    else
                    {
                        if (obsRoll == -1)
                            crit = true;

                        var tenaRoll = State.d20Hidden(Traits.MagicTenacity);
                        if (tenaRoll >= 10)
                        {
                            State.o("You " + valid.ToString() + " instead.");
                            atav = good;
                        }
                        else
                        {
                            if (tenaRoll == -1)
                                crit = true;

                            var luckRoll = State.d20Hidden(Traits.MagicLuck);
                            if (luckRoll >= 10)
                            {
                                State.o("You find yourself " + good + "ing instead.");
                                atav = good;
                            }
                            else if (luckRoll == -1)
                                crit = true;
                        }
                    }

                    if (crit)
                    {
                        State.o("You hurt yourself in your confusion! (TODO)");
                        return;
                    }
                }
            }

            // Still bad?
            if (atav == AttackType.None)
            {
                State.o("Your attack is ineffectual.");
                return;
            }

            // else...
            // TODO: CRITICAL => FUMBLE == YES
            mdl.HandleAttack(atav, i.Violence, i.PassiveNoun, i.Whimsy >= 5, false, i.Airborne >= 5);
        }
    }
}

