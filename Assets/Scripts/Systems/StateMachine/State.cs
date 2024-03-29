public abstract class State
{
    public abstract void Enter();
    public abstract void Tick(float deltaTime);
    public abstract void PhysicsTick(float fixedDeltaTime);
    public abstract void Exit();
}
