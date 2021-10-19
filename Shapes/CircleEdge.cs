using System;
using System.Collections.Generic;
using Projekt1.Relations;

namespace Projekt1.Shapes
{
    class CircleEdge : SimpleShape
    {
        public override void Move(int dX, int dY, Stack<Tuple<Relation, SimpleShape>> relationsStack, bool addRelationsToFix = true) {}

        public override string ToString() => $"()";

        public override void SavePosition() {}

        public override void BackUpSavedPosition() {}
    }
}
