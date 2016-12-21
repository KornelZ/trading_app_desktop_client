using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LGSA_Server.Model.DTO
{
    public class DictionaryDto
    {
        public IEnumerable<ConditionDto> Conditions { get; set; }
        public IEnumerable<GenreDto> Genres { get; set; }
        public IEnumerable<ProductTypeDto> ProductTypes { get; set; }
    }
}