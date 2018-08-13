namespace QAFood.DAL
{
    /// <summary>
    /// The base repository class. All repositories need  to inherit from this and implement IRepository T 
    /// </summary>
    public abstract class Repository<T>
    {
        public ApplicationDbContext Context { get; set; } = null;

        public Repository(ApplicationDbContext context)
        {
            this.Context = context;
        }

        public void Save()
        {
            this.Context.SaveChanges();
        }

        public async void SaveAsync()
        {
            await this.Context.SaveChangesAsync();
        }

    }
}
