﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team20_TextRPG
{
    public partial class DungeonReward
    {
        public string Name { get; set; }
        public int Quantity { get; set; }

        public DungeonReward(string name, int quantity)
        {
            Name = name;
            Quantity = quantity;
        }

        public DungeonReward() { }
    }

    partial class TextRPG_BattleResult
    {
        public List<DungeonReward> dungeonReward = new List<DungeonReward>();

        public static void BattleResult(TextRPG_Player player, List<TextRPG_Monster> monsters, int beforeHp, int beforeLevel, int beforeExp, Stage stage)
        {
            Console.Clear();
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("==========================================================================");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(@"
         ____  ____  ____  ____  ____  ____  _________  ____  ____  ____  ____  ____  ____ 
        ||B ||||A ||||T ||||T ||||L ||||E ||||       ||||R ||||E ||||S ||||U ||||L ||||T ||
        ||__||||__||||__||||__||||__||||__||||_______||||__||||__||||__||||__||||__||||__||
        |/__\||/__\||/__\||/__\||/__\||/__\||/_______\||/__\||/__\||/__\||/__\||/__\||/__\|
");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("==========================================================================");


            //  클리어 실패 처리
            if (player.Hp <= 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("You Lose");
                Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine($"LV. {beforeLevel} | {player.Name} → LV. {player.Level} | {player.Name}");
                Console.WriteLine($"체력: {beforeHp} → {player.Hp}");
                Console.WriteLine();

                Console.WriteLine("0. 다음");
            }

            //  클리어 처리
            else if (player.Hp > 0)
            {
                TextRPG_Manager.Instance.playerInstance.AddItem($"{stage.rewards.itemId}", 1);

                player.CurrentStage++;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Victory!!!");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
                Console.WriteLine($"던전에서 몬스터 {monsters.Count}마리를 잡았습니다");

                Console.WriteLine("[캐릭터 정보]");
                Console.WriteLine($"LV. {beforeLevel} | {player.Name} → LV. {player.Level} | {player.Name}");
                Console.WriteLine($"체력: {beforeHp} → {player.Hp}");
                Console.WriteLine($"경험치: {beforeExp} → {player.Exp}");
                Console.WriteLine();

                Console.WriteLine("[획득 아이템]");
                Console.WriteLine($"{player.Inventory[player.Inventory.Count - 1].Name}");
                Console.WriteLine($"{player.Inventory[player.Inventory.Count - 1].Description}");
                Console.WriteLine("0. 다음");
            }

            
            player.GetMana(10); // 전투 종료시 마나 10 회복

            int input = TextRPG_SceneManager.CheckInput(0, 0);

            if (stage.id == 5)
            {
                TextRPG_EndScene.EndScene();
            }

            else
            {
                TextRPG_StartScene.DisplayStartScene();
            }
        }


        private static void ClearRewardToPlayer(DungeonReward dungeonReward)
        {
            TextRPG_Manager.Instance.playerInstance.AddItem(dungeonReward.Name, dungeonReward.Quantity);
        }
    }
}