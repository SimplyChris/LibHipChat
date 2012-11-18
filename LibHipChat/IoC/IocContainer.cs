using StructureMap;

namespace LibHipChat.IoC
{
    public class IocContainer
    {
        public static IContainer Container { get; set; }

        static IocContainer()
        {
            Container = new Container();
        }

        public static void Configure()
        {
           Container.Configure(init=>init.AddRegistry<LibHipChatRegistry>());
        }
        
        public static T GetInstance<T>()
        {
            return Container.GetInstance<T>();
        }
    }
}