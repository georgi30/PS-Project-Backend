using System.Collections.Generic;
using Persistence.Entities;

namespace PS_Project_Model.Resources
{
    public class SearchResource
    {
        public string CategoryName { get; set; }
        public List<string> Ingredients { get; set; }
        public bool IncludeAllIngredients { get; set; }
        public string SearchTerm { get; set; }
    }
}