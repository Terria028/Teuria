using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Teuria;

public abstract class PhysicsComponent : Component
{
    private HashSet<PhysicsComponent> Collided = new HashSet<PhysicsComponent>();
    public bool Collideable;
    private readonly Collider collider;
    public Collider Collider => collider;
    public AABB BoundingArea => collider.BoundingArea;    

    public IPhysicsEntity PhysicsEntity;

    public PhysicsComponent(Collider collider, bool collidable = true) 
    {
        Collideable = collidable;
        this.collider = collider;
    }

    public override void Added(Entity entity) 
    {
        base.Added(entity);
        if (entity is IPhysicsEntity physicsEntity) 
        {
            PhysicsEntity = physicsEntity;
        }
        collider.Entity = entity;
    }

    public override void Removed() 
    {
        if (collider != null)
            collider.Entity = null;
        base.Removed();
    }

    public void Detect(HashSet<PhysicsComponent> components) 
    {
        Collided = components;
    }
    
    public bool CheckWallCollision(IPhysicsEntity entity, Vector2 offset) 
    {
        foreach (var wall in Collided) 
        {
            if (entity.Collider.Equals(wall.Collider)) { continue; }
            if (entity.Collider.Collide(wall.BoundingArea, offset)) 
            {
                return true;
            }
        }
        return false;
    }

    public bool CheckWallCollision(IPhysicsEntity entity, string groupName, Vector2 offset) 
    {
        foreach (var wall in Collided) 
        {
            if (entity.Collider.Equals(wall.Collider)) { continue; }
            if (entity.Collider.Collide(wall.BoundingArea, offset)) 
            {
                if (wall.Collider.GroupName == groupName) return true;
                return false;
            }
        }
        return false;
    }

    public bool CheckWallCollision(IPhysicsEntity entity, string groupName, Vector2 offset, out IPhysicsEntity ent) 
    {
        foreach (var wall in Collided) 
        {
            if (entity.Collider.Equals(wall.Collider)) { continue; }
            if (entity.Collider.Collide(wall.BoundingArea, offset)) 
            {
                if (wall.Collider.GroupName == groupName) 
                {
                    ent = wall.PhysicsEntity;
                    return true;
                }
                continue;
            }
        }
        ent = null;
        return false;
    }


}