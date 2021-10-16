using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projekt1.Relations;

namespace Projekt1.Shapes
{
    abstract class SimpleShape
    {
        public enum ShapeType
        {
            Circle = 1,
            Polygon = 2,
            Edge = 3,
            Vertex = 4,
            CircleEdge = 5,
            CircleCenter = 6
        }

        private List<Relation> relations = new List<Relation>();

        public string Uid { get; private set; }

        protected SimpleShape()
        {
            this.Uid = System.Guid.NewGuid().ToString();
        }

        public abstract ShapeType GetShapeType();

        public abstract void Move(int dX, int dY);

        public abstract override string ToString();


        /* Saving position */
        public abstract void SavePosition();

        public abstract void BackUpSavedPosition();

        /* Relations */
        public void AddRelation(Relation relation) => this.relations.Add(relation);

        public void RemoveRelation(Relation relation) => this.relations.Remove(relation);

        public virtual Relation GetRelationByType(Type relationType) 
            => this.relations.Find(relation => relation.GetType() == relationType);

        public virtual bool HasRelationByType(Type relationType) => this.GetRelationByType(relationType) != null;

        public virtual List<Type> GetAllRelationTypes()
        {
            List<Type> relationTypes = new List<Type>();
            this.relations.ForEach(relation => relationTypes.Add(relation.GetType()));
            return relationTypes;
        }

        public virtual int GetRelationsNumberExcept(Type? relationType) 
            => this.relations.FindAll(relation => relation.GetType() != relationType).Count;

        public void DestroyRelations()
        {
            foreach (var relation in this.relations.ToArray())
                relation.Destroy();
        }

        /* Destroying */
        public virtual void Destroy()
        {
            this.DestroyRelations();
        }
    }
}
