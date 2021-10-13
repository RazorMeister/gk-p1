﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projekt1.Relations;

namespace Projekt1.Shapes
{
    class CircleEdge : SimpleShape
    {
        public override ShapeType GetShapeType() => ShapeType.CircleEdge;

        public override Type[] GetPossibleRelationTypes() => new[] {typeof(FixedRadius)};

        public override void Move(int dX, int dY) { }

        public override string ToString() => $"()";
    }
}
