using Godot;
using System;

public class Player : KinematicBody2D
{
    public override void _Ready()
    {
      //var animationTree = GetNode("AnimationTree") as AnimationTree;
      //animationTree.SetActive(true);  
    }
    public const int MAX_SPEED = 9000;
    public const int ACCELERATION = 7000;
    public const int FRICTION = 7000;
  public override void _Process(float delta)
  {
      var animationTree = GetNode("AnimationTree") as AnimationTree;
      //animationTree.SetActive(true);
      AnimationNodeStateMachinePlayback stateMachine = (AnimationNodeStateMachinePlayback)animationTree.Get("parameters/playback");
      var velocity = Vector2.Zero;
      var input_vector = Vector2.Zero;
      input_vector.x = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
      input_vector.y = Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up");
      //input_vector = input_vector.Normalized();
      if(input_vector != Vector2.Zero){
          animationTree.Set("parameters/Idle/blend_position", input_vector);
          animationTree.Set("parameters/Run/blend_position", input_vector);
          stateMachine.Travel("Run");
          velocity = velocity.MoveToward(input_vector*MAX_SPEED, ACCELERATION* delta);
          }
      else{
          stateMachine.Travel("Idle");
      velocity = velocity.MoveToward(Vector2.Zero, FRICTION*delta);}
      velocity = MoveAndSlide(velocity);
  }
}
