namespace Ransomink
{
    public enum CollisionState
    {
        AIR,
        GROUND,
        WALL,
        LEDGE,
        CEILING
    }

    public enum MovementState
    {
        IDLE,
        WALK,
        RUN
    }

    public enum AirborneState
    {
        NONE,
        JUMP,
        FREE,
        FALL,
        FLY
    }

    public enum WallState
    {
        NONE,
        GRAB,
        SLIDE
    }
}
