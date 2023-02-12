using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace TileGame.Core;

public sealed class GameObject
{

    public string Name { get; }
    public Vector2 Position { get; set; }
    public Size Size { get; set; }
    
    
    private GameObject _parent;
    public List<GameObject> Children { get; set; }

    private List<IComponent> _comps;
    private List<IUpdatable> _updateableComps;
    private List<IRenderable> _renderableComps;

    private List<IComponent> _tempComps;
    private List<IUpdatable> _tempUpdateableComps;
    private List<IRenderable> _tempRenderableComps;

    private bool _isInitialized;

    public GameObject(string name)
    {
        Children = new List<GameObject>();
        _comps = new List<IComponent>();
        _updateableComps = new List<IUpdatable>();
        _renderableComps = new List<IRenderable>();
        _tempComps = new List<IComponent>();
        _tempUpdateableComps = new List<IUpdatable>();
        _tempRenderableComps = new List<IRenderable>();

        Name = name;
    }

    public void SetParent(GameObject parent)
    {
        if (_parent != null)
            _parent.Children.Remove(this);
        _parent = parent;
        if (_parent != null)
            _parent.Children.Add(this);
    }

    public GameObject GetRootParent()
    {
        GameObject current = this;
        GameObject parent;
        do
        {
            parent = current._parent;
            if (parent == null)
                return current;
            current = parent;
        }
        while (parent != null);
        return null;
    }

    public void Destroy()
    {
        RemoveAllComponents();

        foreach (var c in Children)
            c.Destroy();
    }

    public void Initialize()
    {
        if (_isInitialized)
            return;

        _tempComps.Clear();
        _tempComps.AddRange(_comps);

        foreach (var comp in _tempComps)
            comp.Initialize();
        foreach (var comp in _tempComps)
            comp.Start();

        _isInitialized = true;
    }

    public void Update(GameTime gameTime)
    {
        _tempUpdateableComps.Clear();
        _tempUpdateableComps.AddRange(_updateableComps);
        for (int i = 0; i < _tempUpdateableComps.Count; i++)
            if (_tempUpdateableComps[i].Enabled)
                _tempUpdateableComps[i].Update(gameTime);
    }

    public void Draw(SpriteBatch sb, GameTime gameTime)
    {
        _tempRenderableComps.Clear();
        _tempRenderableComps.AddRange(_renderableComps);
        for (int i = 0; i < _tempRenderableComps.Count; i++)
            if (_tempRenderableComps[i].Visible)
                _tempRenderableComps[i].Render(sb, gameTime);
    }

    public void AddComponent<T>()
        where T : IComponent
    {
        AddComponent((T)Activator.CreateInstance(typeof(T), this));
    }
    
    public void AddComponent(IComponent comp)
    {
        _comps.Add(comp);

        if (comp is IUpdatable updateable)
        {
            _updateableComps.Add(updateable);
        }

        if (comp is IRenderable renderable)
        {
            _renderableComps.Add(renderable);
        }

        if (_isInitialized)
        {
            comp.Initialize();
            comp.Start();
        }
    }

    public T GetComponent<T>() where T : class, IComponent
    {
        foreach (var comp in _comps)
        {
            if (comp is T matched)
                return matched;
        }
        return null;
    }

    public bool RemoveComponent(IComponent comp)
    {
        if (_comps.Remove(comp))
        {
            if (comp is IUpdatable updateable)
            {
                _updateableComps.Remove(updateable);
            }

            if (comp is IRenderable renderable)
            {
                _renderableComps.Remove(renderable);
            }
            return true;
        }
        return false;
    }

    public void RemoveComponents<T>() where T : IComponent
    {
        foreach (var comp in Enumerable.Reverse(_comps))
        {
            if (comp is T matched)
                RemoveComponent(matched);
        }
    }

    public void RemoveAllComponents()
    {
        // 왜 뒤집어서 빼야 하느냐 ??
        // 그것은 제 자서전에 나와있습니다 ㅎ
        foreach (var comp in Enumerable.Reverse(_comps))
            RemoveComponent(comp);
    }
}