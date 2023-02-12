using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace TileGame.Core;

public sealed class GameObject
{
    public string Name { get; }
    public Vector2 Position { get; set; }
    public Size Size { get; set; }

    public Rectangle Bounds => new Rectangle((int) Position.X, (int) Position.Y, Size.Width, Size.Height);
    
    private GameObject _parent;
    public List<GameObject> Children { get; set; }

    private List<Component> _comps;

    private bool _isInitialized;

    public GameObject(string name)
    {
        Children = new List<GameObject>();
        _comps = new List<Component>();

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
        
        foreach (var comp in _comps)
            comp.Initialize();
        foreach (var comp in _comps)
            comp.Start();

        _isInitialized = true;
    }

    public bool IsMouseOverGameObject()
    {
        var mouseState = Mouse.GetState();

        return Bounds.Contains(mouseState.Position);
    }

    public void Update(GameTime gameTime)
    {
        for (int i = 0; i < _comps.Count; i++)
            if (_comps[i].Enabled)
                _comps[i].Update(gameTime);
    }

    public void Draw(SpriteBatch sb, GameTime gameTime)
    {
        for (int i = 0; i < _comps.Count; i++)
            if (_comps[i].Visible)
                _comps[i].Render(sb, gameTime);
    }

    public void AddComponent<T>()
        where T : Component, new()
    {
        var obj = new T();
        obj.GameObject = this;
        
        AddComponent(obj);
    }
    
    public void AddComponent(Component comp)
    {
        comp.GameObject = this;
        _comps.Add(comp);

        if (_isInitialized)
        {
            comp.Initialize();
            comp.Start();
        }
    }

    public T GetComponent<T>() where T : Component
    {
        foreach (var comp in _comps)
        {
            if (comp is T matched)
                return matched;
        }
        return null;
    }

    public bool RemoveComponent(Component comp)
    {
        return _comps.Remove(comp);
    }

    public void RemoveComponents<T>() where T : Component
    {
        foreach (var comp in Enumerable.Reverse(_comps))
        {
            if (comp is T matched)
                RemoveComponent(matched);
        }
    }

    public void RemoveAllComponents()
    {
        foreach (var comp in Enumerable.Reverse(_comps))
            RemoveComponent(comp);
    }
}