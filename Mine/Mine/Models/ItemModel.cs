namespace Mine.Models
{
    /// <summary>
    /// Item for the Game
    /// </summary>
    public class ItemModel : BaseModel
    {
        // Add Unique attributes for Item

          
        //the value of the item 
        public int Value { get; set; } = 0;

        public bool Update(ItemModel data)
        {
            //do NOT update the ID, if you do , the record will be orphaned
            //ID=data.ID;

            //update the BASE
            Name = data.Name;
            Description = data.Description;

            //update the extended
            Value = data.Value;

            return true;
        }
    }
}