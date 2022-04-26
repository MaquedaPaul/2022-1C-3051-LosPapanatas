﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using TGC.MonoGame.TP.Elements;
using TGC.MonoGame.TP.Geometries;

namespace TGC.MonoGame.TP.Niveles
{
    class Sala4 : Sala
    {
        private Vector3 platformScale = new Vector3(10f, 1, 10f);
        private Vector3 arrowScale = new Vector3(4f, 4f, 4f);

        public CubePrimitive PisoSalida { get; set; }
        public Matrix PisoSalidaWorld { get; set; }
        public List<MovingSphere> Spheres { get; set; }
        public List<Platform> Platforms { get; set; }
        public List<Coin> Coins { get; set; }

        public Sala4(ContentManager content, GraphicsDevice graphicsDevice, Vector3 posicion) : base(content, graphicsDevice, posicion)
        {
            PisoWorld = Matrix.CreateScale(platformScale) * Matrix.CreateTranslation(new Vector3(-45f, 0, 0) + posicion);

            PisoSalida = new CubePrimitive(graphicsDevice);
            PisoSalidaWorld = Matrix.CreateScale(platformScale) * Matrix.CreateTranslation(new Vector3(45f, 0, 0) + posicion);

            Platforms = new List<Platform>();
            Platforms.Add(new Platform(new CubePrimitive(graphicsDevice), new Vector3(-22.5f, 0, 0)));
            Platforms.Add(new Platform(new CubePrimitive(graphicsDevice), new Vector3(0, 0, 0)));
            Platforms.Add(new Platform(new CubePrimitive(graphicsDevice), new Vector3(0, 0, 22.5f)));
            Platforms.Add(new Platform(new CubePrimitive(graphicsDevice), new Vector3(0, 0, -22.5f)));
            Platforms.Add(new Platform(new CubePrimitive(graphicsDevice), new Vector3(22.5f, 0, 0)));

            Spheres = new List<MovingSphere>();
            Spheres.Add(new MovingSphere(new List<Vector3> { new Vector3(-33.75f, 5, -20), new Vector3(-33.75f, 5, 20) }, graphicsDevice, Color.Red, -2, 45f));
            Spheres.Add(new MovingSphere(new List<Vector3> { new Vector3(-12f, 5, 20), new Vector3(-12f, 5, -20) }, graphicsDevice, Color.Red, -2, 45f));
            Spheres.Add(new MovingSphere(new List<Vector3> { new Vector3(12f, 5, -20), new Vector3(12f, 5, 20) }, graphicsDevice, Color.Red, -2, 45f));
            Spheres.Add(new MovingSphere(new List<Vector3> { new Vector3(33.75f, 5, 20), new Vector3(33.75f, 5, -20) }, graphicsDevice, Color.Red, -2, 45f));
            // esferas con movimiento vertical
            Spheres.Add(new MovingSphere(new List<Vector3> { new Vector3(0, 35, 11.25f), new Vector3(0, -35, 11.25f) }, graphicsDevice, Color.Red, -2, 100f));
            Spheres.Add(new MovingSphere(new List<Vector3> { new Vector3(0, -35, -11.25f), new Vector3(0, 35, -11.25f) }, graphicsDevice, Color.Red, -2, 100f));


            Coins = new List<Coin>();
            Coins.Add(new Coin(graphicsDevice, new Vector3(0, 10, 22.5f) + posicion));
            Coins.Add(new Coin(graphicsDevice, new Vector3(0, 10, -22.5f) + posicion));

            foreach (Platform platform in Platforms)
            {
                platform.World = Matrix.CreateScale(platformScale) * Matrix.CreateTranslation(platform.Position + posicion);
            }

            foreach (MovingSphere sphere in Spheres)
            {
                sphere.World = Matrix.CreateScale(arrowScale) * Matrix.CreateTranslation(sphere.Position + Posicion);
            }
        }

        public override void Draw(GameTime gameTime, Matrix view, Matrix projection)
        {
            base.Draw(gameTime, view, projection);
            PisoSalida.Draw(PisoSalidaWorld, view, projection);

            foreach(Platform platform in Platforms)
            {
                platform.Geometric.Draw(platform.World, view, projection);
            }
            
            foreach(MovingSphere sphere in Spheres)
            {
                sphere.Sphere.Draw(sphere.World, view, projection);
            }

            foreach(Coin coin in Coins)
            {
                coin.Draw(view, projection);
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            foreach (MovingSphere sphere in Spheres)
            {
                sphere.Move(gameTime);
                sphere.World = Matrix.CreateScale(arrowScale) * Matrix.CreateTranslation(sphere.Position + Posicion);
            }

            foreach(Coin coin in Coins)
            {
                coin.Update(gameTime);
            }
        }
    }
}
