using MagicShot_Roulette.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicShot_Roulette.Scenes.Main
{
    internal class HelpScene : BaseScene
    {
        public HelpScene() : base(SceneType.Help)
        {
        }

        public override void Display()
        {
            Console.WriteLine("=== 마탄 룰렛 게임 방법 ===");
            Console.WriteLine();
            Console.WriteLine("권총은 플레이어만 사용합니다.");
            Console.WriteLine("현재 약실의 탄이 실탄인지 가탄인지에 따라 결과가 달라집니다.");
            Console.WriteLine();
            Console.WriteLine("[기본 규칙]");
            Console.WriteLine("가탄으로 나를 쏘면      : 턴 유지");
            Console.WriteLine("가탄으로 적을 쏘면      : 턴 종료, 몬스터 공격");
            Console.WriteLine("실탄으로 나를 쏘면      : 턴 종료, 몬스터 공격");
            Console.WriteLine("실탄으로 적을 쏘면      : 턴 유지");
            Console.WriteLine();
            Console.WriteLine("즉, 가탄일 것 같으면 나를 쏘고,");
            Console.WriteLine("실탄일 것 같으면 적을 쏘는 것이 유리합니다.");
            Console.WriteLine();
            Console.WriteLine("[승리 보상]");
            Console.WriteLine("몬스터에게 승리하면 아래 강화 중 하나가 랜덤으로 적용됩니다.");
            Console.WriteLine("- HP 증가");
            Console.WriteLine("- MP 증가");
            Console.WriteLine("- 인벤토리 슬롯 1칸 증가");
            Console.WriteLine();
            Console.WriteLine("[아이템]");
            Console.WriteLine("밧줄     : 상대의 턴을 1턴 넘깁니다.");
            Console.WriteLine("와인     : 현재 약실을 제거합니다.");
            Console.WriteLine("수정     : 현재 약실을 확인합니다.");
            Console.WriteLine("포션     : HP를 1 회복합니다.");
            Console.WriteLine("마법가루 : 현재 약실이 실탄이면 데미지가 2배가 됩니다.");
            Console.WriteLine("방패     : 다음에 받을 데미지를 1회 무효화합니다.");
            Console.WriteLine("반전 룬  : 현재 약실의 탄을 반전합니다.");
            Console.WriteLine();
            Console.WriteLine("[맵]");
            Console.WriteLine("휴식 : HP, MP, 인벤토리 슬롯 중 하나를 선택해 강화하고 아이템을 뽑을 수 있습니다.");
            Console.WriteLine("미지 : 현재는 아이템 획득 이벤트가 발생합니다.");
            Console.WriteLine();
            Console.WriteLine("현재는 1층과 2층까지 플레이할 수 있습니다.");
            Console.WriteLine("돌아가려면 Space 키를 눌러주세요..."); 
        }

        public override void OnDisable()
        { 
        }

        public override void OnEnable()
        { 
        }

        public override void Update(KeyInfo key)
        {
            if(key == KeyInfo.Space)
            {
                GameManager.GetInstance().Scene.ChangeScene(SceneType.Main);
            }
        }
    }
}
