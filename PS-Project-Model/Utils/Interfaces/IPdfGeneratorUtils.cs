using Persistence.Entities;

namespace PS_Project_Model.Utils.Interfaces
{
    public interface IPdfGeneratorUtils
    {
        public string GeneratePdf(Recipe recipe);
    }
}