using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections;

public class MonogameHttpExample  : Game {

    GraphicsDeviceManager graphics;

    BasicHttpServer httpServer;

    Vector2 ballPosition = new Vector2(100,100);

    Texture2D textureBall;
    SpriteBatch spriteBatch;

    private MonogameHttpExample(BasicHttpServer httpServer) {
        this.httpServer = httpServer;
        graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
    }

    protected override void Initialize() {
        spriteBatch = new SpriteBatch(GraphicsDevice);
        textureBall = Content.Load<Texture2D>("ball");
        base.Initialize();
    }
    
    protected override void Update(GameTime gameTime) {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)) {
            Exit();
        }
        float pos = 1000 * gameTime.TotalGameTime.Seconds;
        ballPosition.X += MathF.Sin(pos);
        ballPosition.Y += MathF.Cos(pos * 0.1f);
        httpServer.htmlSend = String.Format(
            @"
<!DOCTYPE html>
<html>
    <head>
        <!-- For mobile devices handy -->
        <meta name='viewport' content='width=device-width, initial-scale=1.0'/> 
    </head>
    <body bgcolor='coral'>
        <h1>statistics:</h1>
        <p>time played: {0}</p>
        <p> X position of the ball: {1}</p>
        <p> Y position of the ball: {2}</p>
    </body>
</html>"
            ,
            gameTime.TotalGameTime,
            ballPosition.X,
            ballPosition.Y
        );

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime) {
        graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
 
        // TODO: Add your drawing code here
        spriteBatch.Begin();
        spriteBatch.Draw(textureBall, ballPosition, Color.White);
        spriteBatch.End();
        base.Draw(gameTime);
    }

    public static void Main(String[] argv) {
        BasicHttpServer server = new BasicHttpServer(port:8080);
        using(var game = new MonogameHttpExample(server)) {
            game.Run();
        }
        server.Stop();
    }

}