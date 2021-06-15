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

    public class GamePadController : IController
    {
        private Dictionary<Buttons, ICommand> controllerMappings;
        private Dictionary<Buttons, ICommand> playMappings;
        private Dictionary<Buttons, ICommand> menuMappings;
        private PlayerIndex p1 = PlayerIndex.One;
        private GamePadState gamepad = GamePad.GetState(PlayerIndex.One);        
        private GamePadCapabilities capabilities = GamePad.GetCapabilities(PlayerIndex.One);
        private ButtonState prevState;
        private Buttons pressed;
        private ICommand prevCommand;
        private bool released = true;
        ICommand StandingInPlaceCommand;
        Link link;
        int timer = 5;

        public GamePadController()
        {
            controllerMappings = new Dictionary<Buttons, ICommand>();
            playMappings = new Dictionary<Buttons, ICommand>();
            menuMappings = new Dictionary<Buttons, ICommand>();
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

            playMappings.Add(Buttons.DPadUp, WalkingUpCommand);
            playMappings.Add(Buttons.DPadLeft, WalkingLeftCommand);
            playMappings.Add(Buttons.DPadDown, WalkingDownCommand);
            playMappings.Add(Buttons.DPadRight, WalkingRightCommand);
            playMappings.Add(Buttons.Back, QuitGameCommand);

            playMappings.Add(Buttons.LeftThumbstickUp, WalkingUpCommand);
            playMappings.Add(Buttons.LeftThumbstickLeft, WalkingLeftCommand);
            playMappings.Add(Buttons.LeftThumbstickDown, WalkingDownCommand);
            playMappings.Add(Buttons.LeftThumbstickRight, WalkingRightCommand);

            playMappings.Add(Buttons.A, AttackCommand);
            playMappings.Add(Buttons.B, UseEquipedCommand);

            playMappings.Add(Buttons.RightThumbstickUp, CameraUpCommand);
            playMappings.Add(Buttons.RightThumbstickLeft, CameraLeftCommand);
            playMappings.Add(Buttons.RightThumbstickDown, CameraDownCommand);
            playMappings.Add(Buttons.RightThumbstickRight, CameraRightCommand);

            playMappings.Add(Buttons.LeftShoulder, PreviousRoomCommand);
            playMappings.Add(Buttons.RightShoulder, NextRoomCommand);
            playMappings.Add(Buttons.LeftTrigger, PreviousDungeonCommand);
            playMappings.Add(Buttons.RightTrigger, NextDungeonCommand);
            playMappings.Add(Buttons.Start, InventoryOpenCloseCommand);
            playMappings.Add(Buttons.Y, RewindCommand);
            playMappings.Add(Buttons.X, NaviHintCommand);


            menuMappings = new Dictionary<Buttons, ICommand>() {
                {Buttons.Start, InventoryOpenCloseCommand },
                {Buttons.A, SelectCommand },
                {Buttons.DPadUp, SelectUpCommand },
                {Buttons.DPadDown, SelectDownCommand },
                {Buttons.DPadLeft, SelectLeftCommand },
                {Buttons.DPadRight, SelectRightCommand },
                {Buttons.Y, SaveCommand }
            };

        }

        public void Update(Game1 gameState)
        {
            link = gameState.link;
            if (Game1.game.gameState.Peek().GetType() == typeof(MenuState))
            {
                controllerMappings = menuMappings;


                if (capabilities.IsConnected)
                {
                    gamepad = GamePad.GetState(p1);
                    if (capabilities.GamePadType == GamePadType.GamePad)
                    {
                        foreach (Buttons button in controllerMappings.Keys)
                        {
                            if (gamepad.IsButtonDown(button) && button != pressed)
                            {
                                controllerMappings[button].Execute();
                                pressed = button;
                            }
                            else if (gamepad.IsButtonUp(button) && button == pressed)
                            {
                                pressed = Buttons.BigButton;
                            }
                        }
                    }
                }
            }
            else
            {
                controllerMappings = playMappings;
                if (capabilities.IsConnected)
                {
                    gamepad = GamePad.GetState(p1);
                    if (capabilities.GamePadType == GamePadType.GamePad)
                    {
                        foreach (Buttons button in controllerMappings.Keys)
                        {
                            if (gamepad.IsButtonDown(button))
                            {
                                controllerMappings[button].Execute();
                            }
                        }
                    }
                }
            }

            /*if (gamepad.IsButtonUp(Buttons.DPadUp) && gamepad.IsButtonUp(Buttons.DPadDown) && gamepad.IsButtonUp(Buttons.DPadLeft) && gamepad.IsButtonUp(Buttons.DPadRight) && gamepad.ThumbSticks.Left.X == 0 && gamepad.ThumbSticks.Left.Y == 0) {
                //prevCommand = StandingInPlaceCommand;
                link.BeIdle();
            }*/
        }
    }


}
