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

    public class Sala
    {
        public const string ContentFolderEffects = "Effects/";

        private Effect Effect { get; set; }

        public Cube Piso { get; set; }
        public Cube ParedOeste { get; set; }
        public Cube ParedEste { get; set; }
        public Cube ParedNorteIzq { get; set; }
        public Cube ParedNorteDer { get; set; }
        public Cube Techo { get; set; }

        public Vector3 Posicion;
        public static float Size = 100f;

        public Sala(ContentManager content, GraphicsDevice graphicsDevice, Vector3 posicion)
        {
            Posicion = posicion;

            Effect = content.Load<Effect>(ContentFolderEffects + "BasicShader");

            Piso = new Cube(graphicsDevice, content, posicion,Color.Gray);
            Piso.WorldUpdate(new Vector3(Size, 1f, Size), new Vector3(0, 0, 0) , Quaternion.Identity);
            
            ParedOeste = new Cube(graphicsDevice, content, posicion, Color.Orange);
            ParedOeste.WorldUpdate(new Vector3(Size, Size, 1f), new Vector3(0, Size / 2, Size / 2) , Quaternion.Identity);

            ParedEste = new Cube(graphicsDevice, content, posicion, Color.Orange);
            ParedEste.WorldUpdate(new Vector3(Size, Size, 1f), new Vector3(0, Size / 2, -Size / 2) , Quaternion.Identity);
            
            ParedNorteIzq = new Cube(graphicsDevice, content, posicion, Color.Orange);
            ParedNorteIzq.WorldUpdate(new Vector3(1f, Size, Size * 0.45f), new Vector3(50, Size / 2, Size * 0.275f), Quaternion.Identity);

            ParedNorteDer = new Cube(graphicsDevice, content, posicion, Color.Orange);
            ParedNorteDer.WorldUpdate(new Vector3(1f, Size, Size * 0.45f), new Vector3(50, Size / 2, -Size * 0.275f), Quaternion.Identity);

            Techo = new Cube(graphicsDevice, content, posicion);
            Techo.WorldUpdate(new Vector3(Size, 1f, Size), new Vector3(0, Size, 0) , Quaternion.Identity);
        }

        public virtual void Draw(GameTime gameTime, Matrix view, Matrix projection)
        {

            // Set the View and Projection matrices, needed to draw every 3D model
            Effect.Parameters["View"].SetValue(view);
            Effect.Parameters["Projection"].SetValue(projection);

            Piso.Draw(view, projection);
            ParedOeste.Draw(view, projection);
            ParedEste.Draw(view, projection);
            ParedNorteIzq.Draw(view, projection);
            ParedNorteDer.Draw(view, projection);
            Techo.Draw(view, projection);
        }

        public List<Cube> GetPhyisicalObjects() {
            List<Cube> l = new List<Cube>();
            l.Add(Piso);
            l.Add(ParedEste);
            l.Add(ParedOeste);
            l.Add(ParedNorteDer);
            l.Add(ParedNorteIzq);
            l.Add(Techo);
            return l;
        }
        public virtual void Update(GameTime gameTime) { }
    }
}
