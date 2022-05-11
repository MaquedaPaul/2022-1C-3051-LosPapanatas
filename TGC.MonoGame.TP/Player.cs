﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TGC.MonoGame.TP.Geometries;
using TGC.MonoGame.TP.Elements;
using TGC.MonoGame.Samples.Collisions;

namespace TGC.MonoGame.TP
{
    public class Player
    {
        public Vector3 PositionE { get; private set; }
        public Vector3 VectorSpeed { get; set; }
        public Vector3 roundPosition { get; set; }
        private static float Gravity = 0.1f;
        private static float MoveForce = 1f;
        private static float JumpForce = 2f;

        private static Vector3 scale = new Vector3(5, 5, 5);
        private State estado { get; set; }

        public Sphere Body { get; set; }

        public Vector3 Position { get; set; }

        public Player(GraphicsDevice graphics, ContentManager content)
        {
            Body = new Sphere(graphics, content, 1f, 16, Color.Green);
            Body.WorldUpdate(scale, new Vector3(0, 15, 0), Quaternion.Identity);
        }

        public void Draw(Matrix view, Matrix projection)
        {

            Body.Draw(view, projection);
        }

        public bool Intersects(OrientedBoundingBox box)
        {
            var difference = Body.Collider.Center - box.Center;
            var obbSpaceSphere = new BoundingSphere(Vector3.Transform(difference, box.Orientation), Body.Collider.Radius);
            var aabb = new BoundingBox(-box.Extents, box.Extents);
            return aabb.Intersects(obbSpaceSphere);
        }

        public void Update(GameTime gameTime)
        {
            VectorSpeed += Vector3.Down * Gravity;
            var elapsedTime = Convert.ToSingle(gameTime.ElapsedGameTime.TotalSeconds);
            var scaledSpeed = VectorSpeed * elapsedTime;
            Body.WorldUpdate(scale, scaledSpeed, Quaternion.Identity);
            Position = Body.Position;
        }

        public void PhyisicallyInteract(List<Cube> objects)
        {
            foreach (Cube o in objects)
            {
                if (Intersects(o.Collider))
                {
                    VectorSpeed = new Vector3(VectorSpeed.X, -VectorSpeed.Y, VectorSpeed.Z);
                    //aca tenemos que ver como hacer que reaccione a la colision correctamente
                    Body.WorldUpdate(scale, VectorSpeed * 0.02f, Quaternion.Identity);
                }
            }
        }


        public void Move(Vector3 direction)
        {
            VectorSpeed += direction * MoveForce;
        }
        public void Jump()
        {
            VectorSpeed += Vector3.Up * JumpForce;
        }

    }

}

