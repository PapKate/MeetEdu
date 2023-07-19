namespace AppointMate
{
    /// <summary>
    /// Represents a customer point offset log document in the MongoDB
    /// </summary>
    public class CustomerPointOffsetLogEntity : BaseEntity, IDateCreatable, INoteable, IOffsetable
    {
        #region Private Members

        /// <summary>
        /// The member of the <see cref="Note"/> property
        /// </summary>
        private string? mNote;

        #endregion

        #region Public Properties

        /// <summary>
        /// The creation date
        /// </summary>
        public DateTimeOffset DateCreated { get; set; }

        /// <summary>
        /// The customer
        /// </summary>
        public EmbeddedCustomerEntity? Customer { get; set; }

        /// <summary>
        /// The old points of the customer
        /// </summary>
        public uint OldPoints { get; set; }

        /// <summary>
        /// The offset
        /// </summary>
        public int Offset { get; set; }

        /// <summary>
        /// A flag indicating whether the offset was positive or not
        /// </summary>
        public bool IsPositive { get; set; }

        /// <summary>
        /// The new points of the customer
        /// </summary>
        public uint NewPoints { get; set; }

        /// <summary>
        /// The note
        /// </summary>
        public string Note
        {
            get => mNote ?? string.Empty;
            set => mNote = value;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public CustomerPointOffsetLogEntity() : base()
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates and returns a <see cref="CustomerPointOffsetLogEntity"/> from the specified <paramref name="model"/>
        /// </summary>
        /// <param name="customer">The customer</param>
        /// <param name="model">The model</param>
        /// <returns></returns>
        public static CustomerPointOffsetLogEntity FromRequestModel(CustomerEntity customer, CustomerPointOffsetLogRequestModel model)
        {
            var entity = new CustomerPointOffsetLogEntity();

            DI.Mapper.Map(model, entity);
            entity.IsPositive = model.Offset > 0;
            entity.DateCreated = DateTimeOffset.Now;
            entity.Customer = customer.ToEmbeddedEntity();

            return entity;
        }

        /// <summary>
        /// Creates and returns a <see cref="CustomerPointOffsetLogResponseModel"/> from the current <see cref="CustomerPointOffsetLogEntity"/>
        /// </summary>
        /// <returns></returns>
        public CustomerPointOffsetLogResponseModel ToResponseModel()
            => EntityHelpers.ToResponseModel<CustomerPointOffsetLogResponseModel>(this);

        /// <summary>
        /// Returns a string that represents the current object
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"Note: {Note}, Offset: {Offset}";

        #endregion
    }
}
