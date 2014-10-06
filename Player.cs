using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lo_novo
{
    public struct PlayerTraits
    {
        public int Level;
        public int LevelUpAvailable;

        // physical
        public int MainStrength;
        public int MainDexterity;
        public int MainCharisma;
        public int MainIntelligence;
        public int MainSpeed;

        // weapon skill
        public int WeaponBlade; // SPD cap
        public int WeaponBlaster; // DEX cap
        public int WeaponBlunt; // STR cap
        public int WeaponWrestling; // STR cap

        // thinking skillz
        public int SkillBio; // INT cap
        public int SkillEvade; // Max(DEX, SPD) cap
        public int SkillPunt; // DEX
        public int SkillDefend; // STR cap
        public int SkillObservant; // Max(INT, SPD) cap
        public int SkillLore; // INT cap
        public int SkillDeduction; // INT cap
        public int SkillCharm; // CHA cap
        public int SkillIntimidate; // Min(STR, CHA) cap
        public int SkillPersuasion; // Min(INT, CHA) cap
        public int SkillRepair; // Max(DEX, INT)
        public int SkillWeaponize; // Max(CHA, INT)

        // secret, magic attributes
        public int MagicTenacity;
        public int MagicPower;
        public int MagicControl;
        public int MagicLuck;
        public int MagicTaint;

        // personality tracking
        public int PersonalityHonest;
        public int PersonalityAnnoying;
        public int PersonalityAmenable;
        public int PersonalityReckless;
    }

    public class Player : INoun
    {
        public string Name;
        public List<string> Aliases = new List<string>();
        public IIRC IRC;
        public Room Room;
        public List<Thing> Inventory = new List<Thing>();
        public DefaultInventoryResponses DefaultInventoryResponses;

        public PlayerTraits Traits;
        public string Appearance;

        public void Tick() { }

        public override string ToString()
        {
            return Name;
        }
    }
}
