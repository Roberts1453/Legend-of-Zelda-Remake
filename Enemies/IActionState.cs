using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Sprint0Project
{
	
    public interface IActionState
    {
        void AttackVert(Vector2 target);
        void AttackHoriz(Vector2 target);
        void CollideAction();
        void Update(GameTime gameTime);
    }
    internal class WaitingState : IActionState
    {
        ICharacter character;
        public WaitingState(ICharacter character)
        {
            this.character = character;
            character.Speed = 0;
            character.Movement *= 0;

        }
        public void AttackVert(Vector2 target)
        {
            character.State = new AttackVertState(character, target);

        }
        public void AttackHoriz(Vector2 target)
        {
            character.State = new AttackHorizState(character, target);
        }
        public void CollideAction()
        {

        }
        public void Update(GameTime gameTime)
        {
        }
    }
	internal class WanderState : IActionState
    {
        ICharacter character;
		Directions direction;
		int walkDistance;
		int tileDistance = 16;
		int rnd;
        Vector2 lastLocation;
        public static Vector2 upVector = new Vector2(0, -1);
        public static Vector2 downVector = new Vector2(0, 1);
        public static Vector2 leftVector = new Vector2(-1, 0);
        public static Vector2 rightVector = new Vector2(1, 0);
        public WanderState(ICharacter character)
        {
            this.character = character;
            character.Speed = 1;
            character.Movement = new Vector2(1, 0);
            lastLocation = character.Location;
            ChangeDirection();			

        }
        public void AttackVert(Vector2 target)
        {
            character.State = new AttackVertState(character, target);

        }
        public void AttackHoriz(Vector2 target)
        {
            character.State = new AttackHorizState(character, target);
        }
        public void CollideAction()
        {
			ChangeDirection();
        }
		public void ChangeDirection(){
			direction = (Directions) Randomizer.Instance.Next(0, 4);
            character.ChangeDirection(direction);
			switch(direction){
				case Directions.Up:
					character.Movement = upVector;
					break;
				case Directions.Down:
					character.Movement = downVector;
					break;
				case Directions.Left:
					character.Movement = leftVector;
					break;
				case Directions.Right:
					character.Movement = rightVector;
					break;
			}
		}
        public void Update(GameTime gameTime)
        {
            character.Location += character.Movement * character.Speed;
			if (Vector2.Distance(lastLocation, character.Location) >= tileDistance) {
                lastLocation = character.Location;
                rnd = Randomizer.Instance.Next(0, 8);
				if (rnd == 0)
					ChangeDirection();
			}
			
        }
    }
    internal class AttackingState : IActionState
    {
        ICharacter character;
        public AttackingState(ICharacter character, Vector2 target)
        {
            this.character = character;
            character.Speed = 0;

        }
        public void AttackVert(Vector2 target)
        {
            if (character.Location.Y < target.Y)
                character.Movement = new Vector2(0, 1);
            else
                character.Movement = new Vector2(0, -1);
        }
        public void AttackHoriz(Vector2 target)
        {

        }
        public void CollideAction()
        {
            character.State = new ReturningState(character);
        }
        public void Update(GameTime gameTime)
        {
            character.Location += character.Movement * character.Speed;
        }
    }
    internal class AttackVertState : IActionState
    {
        ICharacter character;
        public AttackVertState(ICharacter character, Vector2 target)
        {
            this.character = character;
            character.Speed = 3;
            if (character.Location.Y < target.Y)
                character.Movement = new Vector2(0, 1);
            else
                character.Movement = new Vector2(0, -1);

        }
        public void AttackVert(Vector2 target)
        {

        }
        public void AttackHoriz(Vector2 target)
        {

        }
        public void CollideAction()
        {
			character.Speed = 0;
            character.State = new WanderState(character);
        }
        public void Update(GameTime gameTime)
        {
            character.Location += character.Movement * character.Speed;

        }
    }
    internal class AttackHorizState : IActionState
    {
        ICharacter character;
        public AttackHorizState(ICharacter character, Vector2 target)
        {
            this.character = character;
            character.Speed = 3;
            if (character.Location.X < target.X)
                character.Movement = new Vector2(1, 0);
            else
                character.Movement = new Vector2(-1, 0);

        }
        public void AttackVert(Vector2 target)
        {

        }
        public void AttackHoriz(Vector2 target)
        {

        }
        public void CollideAction()
        {
			character.Speed = 0;
            character.State = new WanderState(character);
        }
        public void Update(GameTime gameTime)
        {
            character.Location = character.Location + character.Movement * character.Speed;

        }
    }
    internal class ReturningState : IActionState
    {
        ICharacter character;
        public ReturningState(ICharacter character)
        {
            this.character = character;
            character.Movement *= -1;
            character.Speed = 1;

        }
        public void AttackVert(Vector2 target)
        {

        }
        public void AttackHoriz(Vector2 target)
        {

        }
        public void CollideAction()
        {

        }
        public void Update(GameTime gameTime)
        {
            character.Location += character.Movement;

        }
    }
    internal class StunState : IActionState
    {
        //save previous state, movement & speed = 0, timer in update, return to prevstate
        ICharacter character;
        IActionState prevState;
        int timer;
        public StunState(ICharacter character, IActionState prevState)
        {
            if (prevState != null)
                if (prevState.GetType() == typeof(StunState))
                    character.State = prevState;
            this.character = character;
            this.prevState = prevState;
            character.Movement *= 0;
            character.Speed = 0;
            timer = 90;

        }
        public void AttackVert(Vector2 target)
        {

        }
        public void AttackHoriz(Vector2 target)
        {

        }
        public void CollideAction()
        {

        }
        public void Update(GameTime gameTime)
        {
            timer--;
            if (timer <= 0)
                character.State = prevState;

        }
    }
}
