using Microsoft.Xna.Framework;

namespace Teuria;

public abstract class Shape
{
    public Entity Entity;
    public Component Component;
    private float width;
    private float height;
    private Vector2 position;
    private string groupName;
    internal bool IsInTheWorld;
    public string GroupName { get => groupName; set => groupName = value; }

    internal virtual void Added(Component component) 
    {
        Component = component;
    }

    internal virtual void Added(Entity entity) 
    {
        Entity = entity;
    }

    internal virtual void Removed() 
    {
        Entity = null;
        Component = null;
    }

    public abstract bool Collide(float x, float y, float width, float height, Vector2 offset = default);
    public abstract bool Collide(RectangleShape other, Vector2 offset = default);
    public abstract bool Collide(Rectangle rect, Vector2 offset = default);
    public abstract bool Collide(AABB aabb, Vector2 offset = default);
    public abstract bool Collide(Point value);
    public abstract bool Collide(Vector2 value);

    public abstract AABB BoundingArea { get; }

    
    public float Width 
    {
        get => width;
        set => width = value;
    }

    public float Height 
    {
        get => height;
        set => height = value;
    }
    
    public Vector2 Position 
    {
        get => position;
        set => position = value;
    }

    public float X 
    {
        get => Entity.Position.X + position.X;        
        set => position.Y = value;
    }

    public float Y 
    {
        get => Entity.Position.Y + position.Y;
        set => position.Y = value;
    }

    public float Left 
    {
        get => position.X;
        set => position.X = value;
    }

    public float Right 
    {
        get => position.X + Width;
        set => position.X = value - Width;
    }

    public float Bottom 
    {
        get => position.Y + Height;
        set => position.Y = value - Height;
    }

    public float Top 
    {
        get => position.Y;
        set => position.Y = value;
    }

    public Vector2 TopLeft 
    {
        get => new Vector2(Left, Top);
        set 
        {
            Left = value.X;
            Top = value.Y;
        }
    }

    public Vector2 BottomRight 
    {
        get => new Vector2(Right, Bottom);
        set 
        {
            Right = value.X;
            Bottom = value.Y;
        }
    }

    public float GlobalX 
    { 
        get 
        {
            if (Entity != null)
                return Entity.Position.X + Position.X;
            return Position.X;
        }
    }


    public float GlobalY
    {
        get
        {
            if (Entity != null)
                return Entity.Position.Y + Position.Y;
            return Position.X;
        }
    }

    public Vector2 GlobalPosition
    {
        get
        {
            return new Vector2(GlobalX, GlobalY);
        }
    }

    public float GlobalLeft 
    {
        get 
        { 
            if (Entity != null)
                return Left + Entity.Position.X;
            return Left + Position.X;
        }
    }

    public float GlobalRight
    {
        get
        {
            if (Entity != null)
                return Right + Entity.Position.X;
            return Right + Position.X;
        }
    }

    public float GlobalBottom
    {
        get
        {
            if (Entity != null)
                return Bottom + Entity.Position.Y;
            return Bottom + Position.Y;
        }
    }

    public float GlobalTop
    {
        get
        {
            if (Entity != null)
                return Top + Entity.Position.Y;
            return Top + Position.Y;
        }
    }
}