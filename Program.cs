using System;
using System.Collections.Generic;
using System.Linq;

using Ensage;
using SharpDX;
using Ensage.Common.Extensions;
using Ensage.Common;
using System.Windows.Input;
using SharpDX.Direct3D9;


namespace TemplarCombo
{
    class Program
    {
        private static bool activated;
        private static bool toggle = true;
        private static Font txt;
        private static Font not;
        private static Key KeyCombo = Key.D;
        private static bool loaded;
        private static Hero me;
        private static Hero target;
        private const Key ChaseKey = Key.D;
        private static ParticleEffect rangeDisplay;
        static void Main(string[] args)
        {
            Game.OnUpdate += Game_OnUpdate;
            Game.OnWndProc += Game_OnWndProc;
            Console.WriteLine("> TemplarCombo have been injected and ready to use ! ");

            txt = new Font(
   Drawing.Direct3DDevice9,
   new FontDescription
   {
       FaceName = "Tahoma",
       Height = 16,
       OutputPrecision = FontPrecision.Default,
       Quality = FontQuality.Default
   });

            not = new Font(
               Drawing.Direct3DDevice9,
               new FontDescription
               {
                   FaceName = "Tahoma",
                   Height = 24,
                   OutputPrecision = FontPrecision.Default,
                   Quality = FontQuality.Default
               });

            Drawing.OnPreReset += Drawing_OnPreReset;
            Drawing.OnPostReset += Drawing_OnPostReset;
            Drawing.OnEndScene += Drawing_OnEndScene;
            AppDomain.CurrentDomain.DomainUnload += CurrentDomain_DomainUnload;


        }

        public static void Game_OnUpdate(EventArgs args)
        {
            var me = ObjectMgr.LocalHero;
            if (!Game.IsInGame || me.ClassID != ClassID.CDOTA_Unit_Hero_TemplarAssassin)
            {
                return;
            }

            if (activated && toggle && me.CanCast())
            {
                var target = me.ClosestToMouseTarget(1000);
                if (target.IsAlive && !target.IsInvul())
                {
                    var Q = me.Spellbook.SpellQ;
                    var W = me.Spellbook.SpellW;
                    var R = me.Spellbook.SpellR;
                    var D = me.Spellbook.SpellD;
                    var blink = me.FindItem("item_blink");
                    var halberd = me.FindItem("item_heavens_halberd");
                    var abyssal = me.FindItem("item_abyssal_blade");
                    var mjollnir = me.FindItem("item_mjollnir");
                    var maskofmadness = me.FindItem("item_mask_of_madness");
                    var satanic = me.FindItem("item_satanic");
                    var diffusal = me.FindItem("item_diffusal_blade");
                    var wand = me.FindItem("item_magic_wand");
                    var stick = me.FindItem("item_magic_stick");
                    var cheese = me.FindItem("item_cheese");
                    var orchid = me.FindItem("item_orchid");
                    var manta = me.FindItem("item_manta");
                    var phase = me.FindItem("item_phase_boots");

                    if (
                    Q != null &&
                    Q.CanBeCasted() &&
                    me.CanCast() &&
                    !target.IsMagicImmune() &&
                    (Q.CanBeCasted() &&
                    Utils.SleepCheck("Q") &&
                    me.Distance2D(target) <= 1500)
                    )
                    {

                        Q.UseAbility();
                        Utils.Sleep(170 + Game.Ping, "Q");
                    }
                    if (
                    blink != null &&
                    blink.CanBeCasted() &&
                    me.CanCast() &&
                    !target.IsMagicImmune() &&
                    (blink.CanBeCasted() &&
                    Utils.SleepCheck("blink") &&
                    me.Distance2D(target) <= 1500)
                    )
                    {
                        blink.UseAbility(target.Position);
                        Utils.Sleep(250 + Game.Ping, "blink");
                    }
                    if (
                W != null &&
                W.CanBeCasted() &&
                me.CanCast() &&
                !target.IsMagicImmune() &&
                (W.CanBeCasted() &&
                Utils.SleepCheck("W"))
                )
                    {
                        W.UseAbility();
                        me.Attack(target);
                        Utils.Sleep(1000 + Game.Ping, "W");
                    }
                    if (
                 R.CanBeCasted() &&
                 me.CanCast() &&
                 me.Distance2D(target) <= 1000 &&
                 Utils.SleepCheck("R")
                 )
                    {
                        R.UseAbility(target.Position);
                        Utils.Sleep(90 + Game.Ping, "R");
                    }

                    if (
                 abyssal != null &&
                 abyssal.CanBeCasted() &&
                 me.CanCast() &&
                 !target.IsMagicImmune() &&
                 (abyssal.CanBeCasted() &&
                 Utils.SleepCheck("abyssal") &&
                 me.Distance2D(target) <= 400)
                 )
                    {
                        abyssal.UseAbility(target);
                        Utils.Sleep(250 + Game.Ping, "abyssal");
                    }

                    if (
                  halberd != null &&
                  halberd.CanBeCasted() &&
                  me.CanCast() &&
                  !target.IsMagicImmune() &&
                  (abyssal.CanBeCasted() &&
                  Utils.SleepCheck("halberd") &&
                  me.Distance2D(target) <= 700)
                  )
                    {
                        halberd.UseAbility(target);
                        Utils.Sleep(250 + Game.Ping, "halberd");
                    }
                    if (
                  maskofmadness != null &&
                  maskofmadness.CanBeCasted() &&
                  me.CanCast() &&
                  (maskofmadness.CanBeCasted() &&
                  Utils.SleepCheck("maskofmadness") &&
                  me.Distance2D(target) <= 700)
                  )
                    {
                        maskofmadness.UseAbility();
                        Utils.Sleep(250 + Game.Ping, "maskofmadness");
                    }
                    if (
                  mjollnir != null &&
                  mjollnir.CanBeCasted() &&
                  me.CanCast() &&
                  !target.IsMagicImmune() &&
                  (mjollnir.CanBeCasted() &&
                  Utils.SleepCheck("mjollnir") &&
                  me.Distance2D(target) <= 900)
                 )
                    {
                        mjollnir.UseAbility(me);
                        Utils.Sleep(250 + Game.Ping, "mjollnir");
                    }
                    if (
                       (stick != null && stick.CanBeCasted()) ||
                       (wand != null && wand.CanBeCasted()) ||
                       (cheese != null && cheese.CanBeCasted()) &&
                   Utils.SleepCheck("stick") &&
                   me.Distance2D(target) <= 700)
                    {
                        stick.UseAbility();
                        wand.UseAbility();
                        cheese.UseAbility();
                        Utils.Sleep(150 + Game.Ping, "stick");
                    }

                    if (
                    satanic != null &&
                    me.Health / me.MaximumHealth <= 0.3 &&
                    satanic.CanBeCasted() &&
                    me.Distance2D(target) <= 700)
                    {
                        satanic.UseAbility();
                    }


                    var trap = ObjectMgr.GetEntities<Unit>().Where(unit => unit.Name == "npc_dota_templar_assassin_psionic_trap").ToList();
                    var useTrap = target.Distance2D(trap.First());

                    if (
                 D.CanBeCasted() &&
                 me.CanCast() &&
                 useTrap <= 600 &&
                 Utils.SleepCheck("D")
                 )
                    {
                        D.UseAbility();
                        Utils.Sleep(350 + Game.Ping, "D");
                    }

                }
                var range = 1600;
                var canAttack = !Orbwalking.AttackOnCooldown(target) && !target.IsInvul() && !target.IsAttackImmune()
                             && me.CanAttack();
                if (canAttack && me.Distance2D(target) <= 550) Utils.SleepCheck("attack");
                        {
                            me.Attack(target);
                            Utils.Sleep(250, "attack");
                        }
            }
        }





        private static void Game_OnWndProc(WndEventArgs args)
        {
            if (!Game.IsChatOpen)
            {
                if (Game.IsKeyDown(KeyCombo))
                {
                    activated = true;
                }
                else
                {
                    activated = false;
                }



            }
        }




        static void CurrentDomain_DomainUnload(object sender, EventArgs e)
        {
            txt.Dispose();
            not.Dispose();
        }

        static void Drawing_OnEndScene(EventArgs args)
        {
            if (Drawing.Direct3DDevice9 == null || Drawing.Direct3DDevice9.IsDisposed || !Game.IsInGame)
                return;

            var player = ObjectMgr.LocalPlayer;
            var me = ObjectMgr.LocalHero;
            if (player == null || player.Team == Team.Observer || me.ClassID != ClassID.CDOTA_Unit_Hero_TemplarAssassin)
                return;

            if (activated)
            {
                txt.DrawText(null, "TemplarCombo is COMBOING now!", 4, 150, Color.Green);
            }

            if (!activated)
            {
                txt.DrawText(null, "TemplarCombo: Use  [" + KeyCombo + "] for start comboing", 4, 150, Color.Red);
            }


        }

        static void Drawing_OnPostReset(EventArgs args)
        {
            txt.OnResetDevice();
            not.OnResetDevice();
        }

        static void Drawing_OnPreReset(EventArgs args)
        {
            txt.OnLostDevice();
            not.OnLostDevice();
        }
    }
}
