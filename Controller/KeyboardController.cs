using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace Sprint0Project
{

    public class KeyboardController : IController
    {
        private Dictionary<Keys, ICommand> controllerMappings;
        private Dictionary<Keys, ICommand> playMappings;
        private Dictionary<Keys, ICommand> menuMappings;
        private KeyboardState keyboard = Keyboard.GetState();
        private ICommand prevCommand;
        ICommand StandingInPlaceCommand;
        Link link;

        public KeyboardController()
        {
            controllerMappings = new Dictionary<Keys, ICommand>();
            playMappings = new Dictionary<Keys, ICommand>();
            menuMappings = new Dictionary<Keys, ICommand>();
        }
        public void SetKeyCommands(Game1 game)
        {
            StandingInPlaceCommand = new StandingInPlaceCommand(game);
            ICommand WalkingDownCommand = new WalkingDownCommand(game);
            ICommand WalkingLeftCommand = new WalkingLeftCommand(game);
            ICommand WalkingUpCommand = new WalkingUpCommand(game);
            ICommand WalkingRightCommand = new WalkingRightCommand(game);
            ICommand AttackCommand = new AttackCommand(game);
            ICommand QuitGameCommand = new QuitGameCommand(game);
            ICommand DamageLinkCommand = new DamageLinkCommand(game);
            ICommand UseBombCommand = new UseBombCommand(game);
            ICommand UseArrowCommand = new UseArrowCommand(game);
            ICommand UseBoomerangCommand = new UseBoomerangCommand(game);
            ICommand UseEquipedCommand = new UseEquipedCommand(game);
            ICommand CameraUpCommand = new CameraUpCommand(game);
            ICommand CameraDownCommand = new CameraDownCommand(game);
            ICommand CameraLeftCommand = new CameraLeftCommand(game);
            ICommand CameraRightCommand = new CameraRightCommand(game);
            ICommand ResetCommand = new ResetCommand(game);
            ICommand NextRoomCommand = new NextRoomCommand(game);
            ICommand PreviousRoomCommand = new PreviousRoomCommand(game);
            ICommand NextDungeonCommand = new NextDungeonCommand(game);
            ICommand PreviousDungeonCommand = new PreviousDungeonCommand(game);
            ICommand InventoryOpenCloseCommand = new InventoryOpenCloseCommand(game);
            ICommand RewindCommand = new RewindCommand(game);
            ICommand NaviHintCommand = new NaviHintCommand(game);
            ICommand SelectCommand = new SelectCommand(game);
            ICommand SelectLeftCommand = new SelectLeftCommand(game);
            ICommand SelectRightCommand = new SelectRightCommand(game);
            ICommand SelectUpCommand = new SelectUpCommand(game);
            ICommand SelectDownCommand = new SelectDownCommand(game);
            ICommand SaveCommand = new SaveCommand(game);
            ICommand BackCommand = new BackCommand(game);
            ICommand SecondarySelectUpCommand = new SecondarySelectUpCommand(game);
            ICommand SecondarySelectDownCommand = new SecondarySelectDownCommand(game);

            playMappings.Add(Keys.W, WalkingUpCommand);
            playMappings.Add(Keys.A, WalkingLeftCommand);
            playMappings.Add(Keys.S, WalkingDownCommand);
            playMappings.Add(Keys.D, WalkingRightCommand);
            playMappings.Add(Keys.Q, QuitGameCommand);

            playMappings.Add(Keys.Up, WalkingUpCommand);
            playMappings.Add(Keys.Left, WalkingLeftCommand);
            playMappings.Add(Keys.Down, WalkingDownCommand);
            playMappings.Add(Keys.Right, WalkingRightCommand);

            playMappings.Add(Keys.Z, AttackCommand);
            playMappings.Add(Keys.X, UseEquipedCommand);
            playMappings.Add(Keys.D1, UseBombCommand);
            playMappings.Add(Keys.D2, UseArrowCommand);
            playMappings.Add(Keys.D3, UseBoomerangCommand);

            playMappings.Add(Keys.F, DamageLinkCommand);
            playMappings.Add(Keys.T, CameraUpCommand);
            playMappings.Add(Keys.Y, CameraLeftCommand);
            playMappings.Add(Keys.U, CameraDownCommand);
            playMappings.Add(Keys.I, CameraRightCommand);

            playMappings.Add(Keys.G, ResetCommand);
            playMappings.Add(Keys.OemOpenBrackets, PreviousRoomCommand);
            playMappings.Add(Keys.OemCloseBrackets, NextRoomCommand);
            playMappings.Add(Keys.O, PreviousDungeonCommand);
            playMappings.Add(Keys.P, NextDungeonCommand);
            playMappings.Add(Keys.E, InventoryOpenCloseCommand);
            playMappings.Add(Keys.R, RewindCommand);
            playMappings.Add(Keys.Space, NaviHintCommand);

            menuMappings = new Dictionary<Keys, ICommand>() {
                {Keys.E, InventoryOpenCloseCommand },
                {Keys.Space, SelectCommand },
                {Keys.Up, SelectUpCommand },
                {Keys.Down, SelectDownCommand },
                {Keys.Left, SelectLeftCommand },
                {Keys.Right, SelectRightCommand },
                {Keys.Back, BackCommand },
                {Keys.Z, SecondarySelectUpCommand },
                {Keys.X, SecondarySelectDownCommand },
                {Keys.S, SaveCommand },
                {Keys.Q, QuitGameCommand}
            };


        }

        public void Update(Game1 game)
        {
            link = game.link;
            keyboard = Keyboard.GetState();

            if (game.gameState.Peek().GetType() == typeof(MenuState))
                controllerMappings = menuMappings;
            else
                controllerMappings = playMappings;

            if (keyboard.GetPressedKeys().Length == 0)
            {
                prevCommand = null;
            }
            else
            {
                Keys pressedKey = keyboard.GetPressedKeys()[keyboard.GetPressedKeys().Length - 1];
                if (controllerMappings.ContainsKey(pressedKey) && prevCommand != controllerMappings[pressedKey])
                {
                    prevCommand = controllerMappings[pressedKey];
                    controllerMappings[pressedKey].Execute();
                }
            }
            if (!keyboard.GetPressedKeys().Contains(Keys.Up) && !keyboard.GetPressedKeys().Contains(Keys.Down) && !keyboard.GetPressedKeys().Contains(Keys.Left) && !keyboard.GetPressedKeys().Contains(Keys.Right)) {
                //prevCommand = StandingInPlaceCommand;
                link.BeIdle();
            }
        }
    }


}
