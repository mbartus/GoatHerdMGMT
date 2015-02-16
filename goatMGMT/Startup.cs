using Owin;

namespace goatMGMT
{
    public partial class Startup 
    {
        public void Configuration(IAppBuilder app) 
        {
            ConfigureAuth(app);
            System.Data.Entity.Database.SetInitializer(new goatMGMT.DAL.DataInitializer());
            goatMGMT.DAL.DataContext c = new goatMGMT.DAL.DataContext();
            c.Database.Initialize(true);
        }
    }
}
