﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TGC.MonoGame.TP.Elements;
using TGC.MonoGame.TP.Geometries;

namespace TGC.MonoGame.TP.Niveles
{
    public class Sala0 : Sala
    {
        private Cube ParedSur { get; set; }
        private Cube FirstPlatform { get; set; }

        private StartEnd start;

        private Coin Coin { get; set; }

        private PowerUp powerUp;

        public Sala0(ContentManager content, GraphicsDevice graphicsDevice, Vector3 posicion) : base(content, graphicsDevice, posicion)
        {
     
            Coin = new Coin(graphicsDevice,content,new Vector3(25, 20, 0) + posicion);
            FirstPlatform = new Cube(graphicsDevice, content, posicion, content.Load<Texture2D>("Textures/madera"));
            FirstPlatform.WorldUpdate(new Vector3(10f, 1f, 10f), new Vector3(25, 10, 0) + posicion, Quaternion.Identity);

            // TODO: remover este power up
            //powerUp = new SpeedPU(graphicsDevice, content, new Vector3(0, 10, 0));
            //powerUp = new GladePU(graphicsDevice, content, new Vector3(0, 10, 0));

            ParedSur = new Cube(graphicsDevice, content, posicion, content.Load<Texture2D>("Textures/madera"));
            ParedSur.WorldUpdate(new Vector3(1f, Size, Size), new Vector3(-Size / 2, Size / 2, 0) + posicion,Quaternion.Identity);

            start = new StartEnd(graphicsDevice, content, Color.Yellow);
            start.WorldUpdate(new Vector3(10, 100, 10), Vector3.Zero, Quaternion.Identity);
        }

        public override void Draw(GameTime gameTime, Matrix view, Matrix projection)
        {
            base.Draw(gameTime, view, projection);
            FirstPlatform.Draw( view, projection);
            Coin.Draw( view, projection);
            ParedSur.Draw(view, projection);
        }

        public override void DrawTranslucent(GameTime gameTime, Matrix view, Matrix projection)
        {
            //powerUp.Draw(view, projection);
            base.DrawTranslucent(gameTime, view, projection);
            start.Draw(view, projection, (float)gameTime.TotalGameTime.TotalSeconds);
        }

        public override void Update(GameTime gameTime)
        {
            Coin.Update(gameTime);
            //powerUp.Update(gameTime);
            base.Update(gameTime);
        }

        public override List<TP.Elements.Object> GetPhysicalObjects()
        {
            List<TP.Elements.Object> l = base.GetPhysicalObjects();
            l.Add(ParedSur);
            l.Add(FirstPlatform);
            return l;
        }
        public override List<TP.Elements.LogicalObject> GetLogicalObjects()
        {
            List<TP.Elements.LogicalObject> logicalObjects = base.GetLogicalObjects();
            logicalObjects.Add(Coin);
            //logicalObjects.Add(powerUp);
            return logicalObjects;
        }
    }
}
