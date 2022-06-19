﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Teuria;

public class Camera
{
    private Matrix transform = Matrix.Identity;
    private float angle = 0;
    private Vector2 position = Vector2.Zero;
    private Vector2 zoom = Vector2.One;
    private Vector2 origin = Vector2.Zero;
    private Vector2 offset = Vector2.Zero;
    private bool needChanges;
    public Viewport Viewport;


    public Camera(int width, int height) 
    {
        Viewport = new Viewport() { Width = width, Height = height };
    }

    public Camera() : this(TeuriaEngine.ScreenWidth, TeuriaEngine.ScreenHeight) {}


    public void UpdateMatrix() 
    {
        // Position
        var xy = new Vector2((int)Math.Floor(position.X + offset.X), (int)Math.Floor(position.Y + offset.Y));
        var pos = new Vector3(xy, 0);
        // Zoom
        var zooming = new Vector3(zoom, 1);
        // Origin
        var origXy = new Vector2((int)Math.Floor(origin.X), (int)Math.Floor(origin.Y));
        var orig = new Vector3(origXy, 0);

        transform = Matrix.Identity       *
            Matrix.CreateTranslation(pos) * 
            Matrix.CreateRotationZ(angle) *
            Matrix.CreateScale(zooming)   *
            Matrix.CreateTranslation(orig);

        needChanges = false;
    }

    public Matrix Transform 
    { 
        get 
        { 
            if (needChanges)
                UpdateMatrix();
            return transform; 
        }
    }

    public Vector2 Position 
    {
        get => position;
        set 
        {
            needChanges = true;
            position = value;
        }
    }

    public Vector2 Zoom 
    {
        get => zoom;
        set 
        {
            needChanges = true;
            zoom = value;
        }
    }

    public Vector2 Offset 
    {
        get => offset;
        set 
        {
            needChanges = true;
            offset = value;
        }
    }
}

