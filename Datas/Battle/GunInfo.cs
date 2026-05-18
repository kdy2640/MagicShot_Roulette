using System;

namespace MagicShot_Roulette.Datas.Battle
{
    public enum ShotInfo
    {
        None,
        Fake,
        Real
    }

    public interface IReadOnlyGunInfo
    {
        public abstract int MaxShotCount { get; }
        public abstract int RealShotCount { get; }
        public abstract int FakeShotCount { get; }
        public abstract int NowShotCount { get; }
        public abstract bool CanFire { get; }
    }

    public class GunInfo : IReadOnlyGunInfo
    {
        public const int MAXSHOTCAPACITY = 8;
        public int MaxShotCount { get { return maxShotCount; } }
        public int RealShotCount { get { return realShotCount; } }
        public int FakeShotCount { get { return fakeShotCount; } }
        public int NowShotCount { get { return nowShotCount; } }
        public bool CanFire { get { return nowShotCount < maxShotCount; } }

        public int maxShotCount;
        public int realShotCount;
        public int fakeShotCount;
        public int nowShotCount;
        private bool[] ShotArr;

        private GunInfo() { }

        public ShotInfo GetNewShot()
        {
            ShotInfo shot = PeekCurrentShot();
            if (shot == ShotInfo.None) return ShotInfo.None;

            nowShotCount++;
            if (shot == ShotInfo.Real)
            {
                realShotCount--;
            }
            else
            {
                fakeShotCount--;
            }

            return shot;
        }

        public ShotInfo PeekCurrentShot()
        {
            if (ShotArr == null) return ShotInfo.None;
            if (!CanFire) return ShotInfo.None;
            return ShotArr[nowShotCount] ? ShotInfo.Real : ShotInfo.Fake;
        }

        public ShotInfo RemoveCurrentShot()
        {
            return GetNewShot();
        }

        public bool ReverseCurrentShot()
        {
            ShotInfo shot = PeekCurrentShot();
            if (shot == ShotInfo.None) return false;

            ShotArr[nowShotCount] = !ShotArr[nowShotCount];
            if (shot == ShotInfo.Real)
            {
                realShotCount--;
                fakeShotCount++;
            }
            else
            {
                fakeShotCount--;
                realShotCount++;
            }

            return true;
        }

        private void SetArray()
        {
            ShotArr = new bool[MAXSHOTCAPACITY];
            Random rand = new Random();
            for (int i = 0; i < realShotCount; i++)
            {
                ShotArr[i] = true;
            }
            for (int i = realShotCount; i < maxShotCount; i++)
            {
                ShotArr[i] = false;
            }
            for (int i = maxShotCount - 1; i > 0; i--)
            {
                int k = rand.Next(maxShotCount);
                bool temp = ShotArr[k];
                ShotArr[k] = ShotArr[i];
                ShotArr[i] = temp;
            }
        }

        public static GunInfo GetRandomBattleInfo(int maxShot)
        {
            if (maxShot < 2) return null;
            GunInfo battle = new GunInfo();
            Random rand = new Random();
            battle.maxShotCount = maxShot;
            battle.realShotCount = rand.Next(1, maxShot);
            battle.fakeShotCount = maxShot - battle.realShotCount;
            battle.nowShotCount = 0;
            battle.SetArray();
            return battle;
        }

        public static GunInfo GetRandomBattleInfo()
        {
            Random rand = new Random();
            int maxShot = rand.Next(2, 9);
            GunInfo battle = new GunInfo();
            battle.maxShotCount = maxShot;
            battle.realShotCount = rand.Next(1, maxShot);
            battle.fakeShotCount = maxShot - battle.realShotCount;
            battle.nowShotCount = 0;
            battle.SetArray();
            return battle;
        }
    }
}
