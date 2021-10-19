using System;
using System.Collections.Generic;
using Projekt1.Shapes;

namespace Projekt1.Relations
{
    static class RelationManager
    {
        private const int MAX_FIXES_NUMBER = 30;

        public static Stack<Tuple<Relation, SimpleShape>> GetRelationsStack() => new Stack<Tuple<Relation, SimpleShape>>();

        public static void RunRelations(Stack<Tuple<Relation, SimpleShape>> relationsStack)
        {
            int fixesNumber = 0;
            int lastRelationId = 0;

            while (true)
            {
                if (relationsStack.Count == 0)
                    break;

                // Get latest relations tuple from stack
                var relationTuple = relationsStack.Pop();

                // Check if there is not duplicated relation
                if (relationTuple.Item1.Id == lastRelationId)
                    continue;

                lastRelationId = relationTuple.Item1.Id;

                // Fix relation
                relationTuple.Item1.FixRelation(relationTuple.Item2, relationsStack);

                // Check if this is not infinity loop
                if (++fixesNumber >= RelationManager.MAX_FIXES_NUMBER)
                    throw new CannotMoveException();
            }
        }
    }
}
