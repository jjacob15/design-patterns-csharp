using System;
using System.Collections.Generic;
using System.Text;

namespace designs.Structural
{
    [TestCase]
    public class ChainOfResponsibility : ITest
    {
        public void Run()
        {
            var goblin = new Creature("Goblin", 1, 1);
            Console.WriteLine(goblin); // Name: Goblin, Attack: 1, Defense: 1
            var root = new CreatureModifier(goblin);
            root.Add(new DoubleAttackModifier(goblin));
            root.Add(new DoubleAttackModifier(goblin));
            root.Add(new IncreaseDefenseModifier(goblin));
            // eventually...
            root.Handle();
            Console.WriteLine(goblin); // Name: Goblin, Attack: 4, Defense: 1
        }

        public class Creature
        {
            public string Name;

            public Creature(string name, int attack, int defence)
            {
                Name = name;
                Attack = attack;
                Defense = defence;
            }

            public int Attack, Defense;

        }

        public class CreatureModifier
        {
            protected Creature creature;
            protected CreatureModifier next;
            public CreatureModifier(Creature creature)
            {
                this.creature = creature;
            }
            public void Add(CreatureModifier cm)
            {
                if (next != null) next.Add(cm); //here is where to chain responsibilities.
                else next = cm; 
            }
            public virtual void Handle() => next?.Handle();
        }

        public class DoubleAttackModifier : CreatureModifier
        {
            public DoubleAttackModifier(Creature creature)
            : base(creature) 
            { }
            public override void Handle()
            {
                Console.WriteLine($"Doubling {creature.Name}'s attack");
                creature.Attack *= 2;
                base.Handle(); //need to ensure the next in line is handled. Not calling this would avoid executing the entire chain:
            }
        }

        public class IncreaseDefenseModifier : CreatureModifier
        {
            public IncreaseDefenseModifier(Creature creature)
            : base(creature) { }
            public override void Handle()
            {
                if (creature.Attack <= 2)
                {
                    Console.WriteLine($"Increasing {creature.Name}'s defense");
                    creature.Defense++;
                }
                base.Handle();
            }
        }
    }
}
