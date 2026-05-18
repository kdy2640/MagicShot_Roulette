using MagicShot_Roulette.Datas.Stats;

namespace MagicShot_Roulette.Datas.Battle
{
    public abstract class Character
    {
        private readonly CharacterStat data;

        protected Character(CharacterStat stat)
        {
            data = stat;
        }

        public CharacterStat GetStatData()
        {
            return data;
        }
    }
}
