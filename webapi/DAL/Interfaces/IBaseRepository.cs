namespace DAL.Interfaces
{
    public interface IBaseRepository<TDomainClass, TKeyType>
        where TDomainClass : class
    {
        /// <summary>
        /// Create a record
        /// </summary>
        /// <param name="instance"></param>
        void Create(TDomainClass instance);

        /// <summary>
        /// Create a range of records
        /// </summary>
        /// <param name="instances"></param>
        void CreateRange(IEnumerable<TDomainClass> instances);

        /// <summary>
        /// Update a record
        /// </summary>
        /// <param name="instance"></param>
        void Update(TDomainClass instance);

        /// <summary>
        /// Update a range of records
        /// </summary>
        /// <param name="instances"></param>
        void UpdateRange(IEnumerable<TDomainClass> instances);

        /// <summary>
        /// Delete a record
        /// </summary>
        void Delete(TDomainClass instance);

        /// <summary>
        /// Delete a range of records
        /// </summary>
        /// <param name="instances"></param>
        void DeleteRange(IEnumerable<TDomainClass> instances);

        /// <summary>
        /// Get by id
        /// </summary>
        TDomainClass GetByPrimaryKey(TKeyType id);

        /// <summary>
        /// Get by id async
        /// </summary>
        Task<TDomainClass> GetByIdAsync(TKeyType id);

        /// <summary>
        /// Get all records
        /// </summary>
        /// <returns>List of all records</returns>
        IQueryable<TDomainClass> GetAll();

        /// <summary>
        /// Save changes
        /// </summary>
        public bool SaveChanges();

        /// <summary>
        /// Save changes async
        /// </summary>
        public Task<bool> SaveChangesAsync();
    }
}