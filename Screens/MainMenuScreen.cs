using System;
using Microsoft.Xna.Framework;
using MonoGame.Extended.BitmapFonts;
using MonoGame.Extended.Gui;
using MonoGame.Extended.Gui.Controls;
using MonoGame.Extended.ViewportAdapters;
using Screen = MonoGame.Extended.Screens.Screen;

namespace TileGame.Screens;

public class MainMenuScreen : Screen
{
    private readonly Game1 _game;
    private GuiSystem _guiSystem;

    public MainMenuScreen(Game1 game)
    {
        _game = game;
    }
    public override void Update(GameTime gameTime)
    {
        _guiSystem.Update(gameTime);
    }

    public override void Draw(GameTime gameTime)
    {
        _guiSystem.Draw(gameTime);
    }

    public override void LoadContent()
    { 
        var viewportAdapter = new DefaultViewportAdapter(_game.GraphicsDevice);
        
        var guiRenderer = new GuiSpriteBatchRenderer(_game.GraphicsDevice, () => Matrix.Identity);
        var font = _game.Content.Load<BitmapFont>("Sensation");
        BitmapFont.UseKernings = false;
        Skin.CreateDefault(font);

        var startBtn = new Button();
        startBtn.Content = "Start Game";
        startBtn.Clicked += start_clicked;

        var exitBtn = new Button();
        exitBtn.Content = "Exit";
        exitBtn.Clicked += exit_clicked;

        var stack = new StackPanel();
        stack.VerticalAlignment = VerticalAlignment.Centre;
        stack.HorizontalAlignment = HorizontalAlignment.Centre;
        stack.Spacing = 20;
        
        stack.Items.Add(startBtn);
        stack.Items.Add(exitBtn);
        
        var demoScreen = new MonoGame.Extended.Gui.Screen();
        demoScreen.Content = stack;

        _guiSystem = new GuiSystem(viewportAdapter, guiRenderer) { ActiveScreen = demoScreen };
    }

    private void start_clicked(object sender, EventArgs e)
    {
        _game._screenManager.LoadScreen(new GameScreen(_game));
    }

    private void exit_clicked(object sender, EventArgs e)
    {
        Environment.Exit(0);
    }
}