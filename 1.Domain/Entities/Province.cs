namespace _1.Domain;

public class Province: BaseEntity, IBaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }

    // ใช้ในการทำ relationship กับตารางอื่นๆ
    #region Entity Framework Relationship 
        public ICollection<PointOfInterest> PointOfInterests { get; set; }
    #endregion
}