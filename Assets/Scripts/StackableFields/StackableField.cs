namespace StackableFields
{
    public class StackableField : StackableFieldBase
    {
        protected override void Start()
        {
            base.Start();
            CalculatePlantPositions();
            GrowFields();
        }
    }
}