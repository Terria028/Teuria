using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Teuria;

public class Scene 
{
    internal Queue<Node> QueueToFree = new Queue<Node>();
    private List<Node> nodeList = new List<Node>();
    private List<CanvasLayer> layers = new List<CanvasLayer>();
    protected ContentManager Content;
    protected Camera Camera;
    protected SpriteBatch SpriteBatch;
    public event Action OnPause;
    public SceneCanvas MainCanvas;
    private bool paused;
    public bool Paused 
    {
        get => paused;
        set
        {
            paused = value;
            if (paused)
                OnPause?.Invoke();
        }
    }

    public IEnumerable<Node> Entities 
    {
        get => nodeList;
    }

    public Scene(ContentManager content, Camera camera) 
    {
        Content = content;
        Camera = camera;
    }

    public Scene(ContentManager content)
    {
        Content = content;
    }

    public bool HasCanvas(CanvasLayer canvas) 
    {
        return layers.Contains(canvas);
    }

    public bool HasEntity(Entity entity) 
    {
        return nodeList.Contains(entity);
    }

    internal void Activate(SpriteBatch spriteBatch) 
    {
        this.SpriteBatch = spriteBatch;
    }

    internal void AddToQueue(Node entity) 
    {
        QueueToFree.Enqueue(entity);
    }

    public void Add(Node entity, PauseMode pauseMode = PauseMode.Inherit) 
    {
        entity.PauseMode = pauseMode;
        nodeList.Add(entity);
        entity.EnterScene(this, Content);
    }

    public void Add(CanvasLayer layer) 
    {
        layer.Obtain(SpriteBatch, this);
        layers.Add(layer);
    }

    public void Remove(CanvasLayer layer) 
    {
        layers.Remove(layer);
    }

    public void Remove(Node entity) 
    {
        entity.Active = false;
        entity.ExitScene();
        nodeList.Remove(entity);
    }

    public void RemoveAllEntities() 
    {
        for (int i = 0; i < nodeList.Count; i++) 
        {
            nodeList[i].Free();
        }
    }

    public virtual void Initialize() {}
    public virtual void Hierarchy(GraphicsDevice device) 
    {
        foreach(var entity in nodeList) 
        {
            entity.Ready();
        }
    }
    public virtual void ProcessLoop() 
    {
        if (QueueToFree.Count > 0)
            QueueToFree.Dequeue().Free();
        if (Paused) 
        {
            ProcessEntityInPauseMode();
            return;
        }
        foreach(var entity in nodeList) 
        {
            if (!entity.Active) continue;
            entity.Update();
        }
    }

    private void ProcessEntityInPauseMode() 
    {
        foreach(var entity in nodeList) 
        {
            if (!entity.Active) continue;
            if (entity.PauseMode == PauseMode.Single)
                entity.Update();
        }
    }

    public virtual void Render() 
    {
        foreach(var entity in nodeList) 
        {
            if (!entity.Active) continue;
            entity.Draw(SpriteBatch);
        }

        foreach(var layer in layers) 
        {
            layer.Draw();
        }
    }

    public virtual void Exit() 
    {
        foreach(var entity in nodeList) 
        {
            entity.ExitScene();
        }

        foreach(var canvas in layers) 
        {
            canvas.Unload();
        }
    }
}