using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projekt1.Shapes;

namespace Projekt1.Relations
{
    static class RelationManager
    {
        public static Stack<Tuple<Relation, SimpleShape>> GetRelationsStack() => new Stack<Tuple<Relation, SimpleShape>>();

        public static void RunRelations(Stack<Tuple<Relation, SimpleShape>> relationsStack)
        {
            int fixesNumber = 0;
            string lastRelationUid = null;

            while (true)
            {
                if (relationsStack.Count == 0)
                    break;

                var relationTuple = relationsStack.Pop();

                if (relationTuple.Item1.Uid == lastRelationUid)
                    continue;

                lastRelationUid = relationTuple.Item1.Uid;

                //Debug.WriteLine($"{relationTuple.Item1.GetType()} - {relationTuple.Item1.Uid}");
                relationTuple.Item1.FixRelation(relationTuple.Item2, relationsStack);
                fixesNumber++;

                if (fixesNumber >= 30)
                {
                    Debug.WriteLine($"{fixesNumber}");
                    Debug.WriteLine("");
                    throw new CannotMoveException();
                }
            }

            //Debug.WriteLine($"{fixesNumber}");
        }
    }
}
