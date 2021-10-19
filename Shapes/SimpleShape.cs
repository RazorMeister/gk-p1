using System;
using System.Collections.Generic;
using Projekt1.Relations;

namespace Projekt1.Shapes
{
    abstract class SimpleShape
    {
        protected List<Relation> relations = new List<Relation>();

        public abstract void Move(int dX, int dY, Stack<Tuple<Relation, SimpleShape>> relationsStack, bool addRelationsToFix = true);

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

        public void DestroyRelations() => this.relations.ForEach(relation => relation.Destroy());

        public virtual void AddRelationsToStack(Stack<Tuple<Relation, SimpleShape>> relationsStack, Type exceptType = null)
            => this.relations
                .FindAll(relation => relation.GetType() != exceptType)
                .ForEach(relation => relationsStack.Push(new Tuple<Relation, SimpleShape>(relation, this)));


        /* Destroying */
        public virtual void Destroy() => this.DestroyRelations();
    }
}
