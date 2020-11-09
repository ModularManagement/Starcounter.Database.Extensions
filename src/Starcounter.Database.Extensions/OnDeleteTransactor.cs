
namespace Starcounter.Database.Extensions
{
    public class OnDeleteTransactor : TransactorBase<object>
    {
        class OnDeleteContext : ContextBase
        {
            public OnDeleteContext(IDatabaseContext context) : base(context) { }

            public override void Delete(object obj)
            {
                if (obj is IDeleteAware d)
                {
                    d.OnDelete(this);
                }

                base.Delete(obj);
            }
        }

        public static IDatabaseContext GetContext(object proxy)
        {
            IDatabaseContext db = DbProxy.GetContext(proxy);

            return new OnDeleteContext(db);
        }

        public OnDeleteTransactor(ITransactor transactor) : base(transactor) { }

        protected override IDatabaseContext EnterDatabaseContext(object transactorContext, IDatabaseContext db) => new OnDeleteContext(db);
    }
}