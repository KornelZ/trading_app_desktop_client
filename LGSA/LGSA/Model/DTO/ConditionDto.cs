using LGSA.Model.ModelWrappers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LGSA_Server.Model.DTO
{
    public class ConditionDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public ConditionWrapper createCondition()
        {
            ConditionWrapper condition = new ConditionWrapper(new LGSA.Model.dic_condition());
            condition.Id = this.Id;
            condition.Name = this.Name;
            return condition;
        }
    }
}