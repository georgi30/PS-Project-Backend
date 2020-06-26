namespace PS_Project_Model.Resources.Recipes
{
    public class UpdateRecipeResource
    {
        public string Name { get; set; }
        public string[] Products { get; set; }
        public string Description { get; set; }
        public int WhoUserId { get; set; }
        public short Active { get; set; }
        public short Deleted { get; set; }
    }
}