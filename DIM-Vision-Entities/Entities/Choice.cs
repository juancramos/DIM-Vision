using DIM_Vision_Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DIM_Vision_Entities.Entities
{
    public class Choice
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public ChoiceType CboiceType { get; set; }
        public string Value { get; set; }
        public Guid GrammarId { get; set; }
        public virtual Grammar Grammar { get; set; }
        public Guid? ParentId { get; set; }
        public virtual Choice Parent { get; set; }
        public virtual IList<Choice> EmbeddedChoices { get; set; }

        public Choice()
        {
            Id = Guid.NewGuid();
            EmbeddedChoices = new List<Choice>();
            CboiceType = ChoiceType.None;
        }
    }
}
